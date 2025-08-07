using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float move_speed;
    public float ground_check_radius;
    public float ground_drag;
    public float jump_force;
    public float air_multiplier;
    public LayerMask ground_layer;

    private Rigidbody m_rigidbody;
    private Vector3 m_move_direct;

    [SerializeField] private PlayerDebugger m_debugger;

    public bool IsGround { get; private set; }

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
        m_debugger.Refreash(m_rigidbody.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        HandlerInputMove(delta);
    }

    private void HandlerInputMove(float delta)
    {
        if(IsGround)
            m_rigidbody.AddForce(m_move_direct.normalized * move_speed, ForceMode.Force);
        else
            m_rigidbody.AddForce(m_move_direct.normalized * move_speed * air_multiplier, ForceMode.Force);

        Vector3 velocity = new Vector3(m_rigidbody.velocity.x, 0.0f, m_rigidbody.velocity.z);
        if(velocity.magnitude > move_speed)
        {
            m_rigidbody.velocity = velocity.normalized * move_speed 
                + new Vector3(0.0f, m_rigidbody.velocity.y, 0.0f);
        }
    }

    public void SetMoveDirect(Vector3 direct)
    {
        m_move_direct = direct;
    }

    private void GroundCheck()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, ground_check_radius, ground_layer);
        if(collider.Length > 0 )
            IsGround = true;
        else
            IsGround = false;
    }

    public void Jump()
    {
        if(IsGround)
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, 0.0f, m_rigidbody.velocity.z);
            m_rigidbody.AddForce(transform.up * jump_force, ForceMode.Impulse);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //ªÊ÷∆Ground Check
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ground_check_radius);
    }
#endif
}
