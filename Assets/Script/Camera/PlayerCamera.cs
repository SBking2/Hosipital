using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("------------ Mouse Para ------------------")]
    public float sensity;
    public Vector2 cameraLimitation;

    private Transform m_CameraBobTransform;
    private Transform m_CameraPivotTransform;
    private Vector3 m_CameraBobStartPos;
    private Quaternion m_CameraBobStartRotation;

    public Animator CameraGroundShakeAnimator;
    public Animator CameraGround2ShakeAnimator;

    private float m_CameraPovitAngle;

    private Vector2 m_MouseDeltaDirect;
    public Vector2 MouseDelta
    {
        get
        {
            return m_MouseDeltaDirect;
        }
    }

    private void Awake()
    {
        m_CameraPivotTransform = this.transform.Find("CameraPivot").transform;
        m_CameraBobTransform = m_CameraPivotTransform.Find("CameraBob").transform;
    }

    private void Start()
    {
        m_CameraBobStartPos = m_CameraBobTransform.localPosition;
        m_CameraBobStartRotation = m_CameraBobTransform.localRotation;
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        HandleCamera(delta);
    }
    private void HandleCamera(float delta)
    {
        Vector3 character_euler = transform.eulerAngles;
        character_euler.y += m_MouseDeltaDirect.x * sensity * delta;
        transform.rotation = Quaternion.Euler(character_euler);

        m_CameraPovitAngle -= m_MouseDeltaDirect.y * sensity * delta;
        m_CameraPovitAngle = Mathf.Clamp(m_CameraPovitAngle, cameraLimitation.x, cameraLimitation.y);

        Vector3 camera_euler = new Vector3(m_CameraPovitAngle, 0.0f, 0.0f);
        m_CameraPivotTransform.localRotation = Quaternion.Euler(camera_euler);
    }

    public void SetInputMouseDirect(Vector2 direct) { m_MouseDeltaDirect = direct; }
}
