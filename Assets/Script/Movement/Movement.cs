using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [Header("Move Property")]
    public float walk_force;
    public float max_walk_speed;
    public float run_force;
    public float max_run_speed;

    [Space(10)]
    [Header("Air Property")]
    public float ground_drag;
    public float jump_force;
    public float air_multiplier;
    public float air_gravity;
    public float ground_gravity;

    [Space(10)]
    [Header("Croch Property")]
    public float crouch_force;
    public float max_crouch_speed;
    public float crouch_scale;
    public float crouch_speed;
    public float crouch_recovery_speed;

    [Space(10)]
    [Header("Ground Check")]
    public float ground_check_radius;
    public float ground_check_length;

    [Space(10)]
    [Header("Ceiling Check")]
    public float ceiling_check_length;

    [Header("Ground Set")]
    //TODO:自己实现重力
    public LayerMask ground_layer;

    private Vector3 m_input_direct;
    private Vector3 m_move_direct;
    private Vector3 m_slope_normal;
    private float m_limit_speed;
    private bool m_IsCrouchSignal;
    private bool m_IsRunSignal;

    public bool IsGround { get; private set; }
    public bool IsSlope { get; private set; }
    public bool IsCroch { get; private set; }

    /// <summary>
    /// 是否头顶天花板
    /// </summary>
    public bool IsCeiling { get; private set; }

    private Rigidbody m_rigidbody;
    [SerializeField] private CapsuleCollider m_collider;
    [SerializeField] private PlayerDebugger m_debugger;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(IsGround)
        {
            m_rigidbody.drag = ground_drag;
        }else
        {
            m_rigidbody.drag = 0.0f;
        }
        GroundCheck();
        SlopeCheck();
        CeilingCheck();
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        HandleInputMove(delta);
        HandleGravity(delta);
        HandleCrouch(delta);
    }

    private void HandleInputMove(float delta)
    {
        float multiplier = IsGround ? 1.0f : air_multiplier;
        float move_force;
        float max_move_speed;
        if(IsCroch)
        {
            move_force = crouch_force;
            max_move_speed = max_crouch_speed;
        }else if(m_IsRunSignal)
        {
            move_force = run_force;
            max_move_speed = max_run_speed;
        }else
        {
            move_force = walk_force;
            max_move_speed = max_walk_speed;
        }

        //起跳速度作为空中的最大速度限制
        if (IsGround) m_limit_speed = max_move_speed;

        //斜坡处理
        if (IsSlope)
            m_move_direct = m_input_direct - Vector3.Dot(m_input_direct, m_slope_normal) * m_slope_normal;
        else
            m_move_direct = m_input_direct;

        m_rigidbody.AddForce(m_move_direct.normalized * move_force * multiplier, ForceMode.Force);

        //速度限制
        Vector3 velocity = new Vector3(m_rigidbody.velocity.x, 0.0f, m_rigidbody.velocity.z);
        if(m_debugger != null)  m_debugger.Refreash(m_rigidbody.velocity.magnitude);
        
        if (velocity.magnitude > m_limit_speed)
        {
            m_rigidbody.velocity = velocity.normalized * m_limit_speed
                + new Vector3(0.0f, m_rigidbody.velocity.y, 0.0f);
        }
    }
    private void HandleGravity(float delta)
    {
        Vector3 force;
        if (!IsGround)
            force = Vector3.down * m_rigidbody.mass * air_gravity;
        else
            force = Vector3.down * m_rigidbody.mass * ground_gravity;

        m_rigidbody.AddForce(force, ForceMode.Force);
    }
    private void HandleCrouch(float delta)
    {
        bool isCroch = m_IsCrouchSignal || (IsCroch && IsCeiling);

        float scale = transform.localScale.y;
        float signal = isCroch ? -1 : 1;
        float accelerate = isCroch ? crouch_speed : crouch_recovery_speed;

        IsCroch = isCroch;

        scale += signal * accelerate;
        scale = Mathf.Clamp(scale, crouch_scale, 1.0f);
        transform.localScale = new Vector3(transform.localScale.x, scale, transform.localScale.z);
    }
    private void GroundCheck()
    {
        float y = transform.position.y - m_collider.height * transform.localScale.y / 2;
        Vector3 check_pos = transform.position;
        check_pos.y = y + 0.1f;

        Vector3[] points;
        GetThreePoint(check_pos, ground_check_radius, out points);

        for(int i = 0; i < 3; i++)
        {
            Ray ray = new Ray(points[i], Vector3.down);
            if(Physics.Raycast(ray, ground_check_length, ground_layer))
            {
                IsGround = true;
                return;
            }
        }
        IsGround = false;
    }
    private void CeilingCheck()
    {
        float y = transform.position.y + m_collider.height * transform.localScale.y / 2;
        Vector3 check_pos = transform.position;
        check_pos.y = y - 0.1f;

        Vector3[] points;
        GetThreePoint(check_pos, ground_check_radius, out points);

        for (int i = 0; i < 3; i++)
        {
            Ray ray = new Ray(points[i], Vector3.up);
            if (Physics.Raycast(ray, ceiling_check_length, ground_layer))
            {
                IsCeiling = true;
                return;
            }
        }
        IsCeiling = false;
    }
    private void SlopeCheck()
    {
        float y = transform.position.y - m_collider.height * transform.localScale.y / 2;
        Vector3 check_pos = transform.position;
        check_pos.y = y;
        Ray ray = new Ray(check_pos, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, ground_check_radius + 0.1f, ground_layer))
        {
            IsSlope = true;
            m_slope_normal = hit.normal;
        }
        else
            IsSlope = false;
    }
    public void Jump()
    {
        if(IsGround)
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, 0.0f, m_rigidbody.velocity.z);
            m_rigidbody.AddForce(transform.up * jump_force, ForceMode.Impulse);
        }
    }
    public void SetMoveDirect(Vector3 direct)
    {
        m_input_direct = direct;
    }
    public void SetCrouch(bool value) { m_IsCrouchSignal = value; }
    public void SetRun(bool value) { m_IsRunSignal = value; }

    private void GetThreePoint(Vector3 pos,float radius,out Vector3[] points)
    {
        points = new Vector3[3];
        float side = radius / Mathf.Sqrt(2);
        points[0] = pos + transform.rotation * new Vector3(0.0f, 0.0f, radius);
        points[1] = pos + transform.rotation * new Vector3(-side, 0.0f, -side);
        points[2] = pos + transform.rotation * new Vector3(side, 0.0f, -side);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        float y = transform.position.y - m_collider.height * transform.localScale.y / 2 ;
        Vector3 pos = transform.position;
        pos.y = y + 0.1f;

        //绘制Slope Check
        Gizmos.DrawRay(pos, Vector3.down * (ground_check_radius + 0.1f));

        Gizmos.color = Color.red;

        //绘制Ground Check
        Vector3[] points;
        GetThreePoint(pos, ground_check_radius, out points);
        for (int i = 0; i < 3; i++)
        {
            Gizmos.DrawRay(points[i], Vector3.down * ground_check_length);
        }

        //绘制Ceiling Check
        y = transform.position.y + m_collider.height * transform.localScale.y / 2;
        pos.y = y - 0.1f;
        GetThreePoint(pos, ground_check_radius, out points);
        for (int i = 0; i < 3; i++)
        {
            Gizmos.DrawRay(points[i], Vector3.up * ceiling_check_length);
        }

        //绘制move direct
        Ray move_ray = new Ray(transform.position, m_move_direct);
        Gizmos.DrawRay(move_ray);
    }
#endif
}
