using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制相机的上移下移
/// </summary>
public class PlayerCamera
{
    [Header("------------ Mouse Para ------------------")]
    private float m_sensity;
    private Vector2 m_camera_vertical_limit;

    private Transform m_player_transform;
    private Transform m_camera_vertical_handler;    //管理摄像机垂直方向选择的Transform

    private float m_camera_vertical_angle;
    private Vector2 m_mouse_delta;

    public PlayerCamera(Transform player_transform, Transform camera_vertical_transform)
    {
        m_player_transform = player_transform;
        m_camera_vertical_handler = camera_vertical_transform;
        m_sensity = 10.0f;
        m_camera_vertical_limit = new Vector2(-85, 85);
    }

    public void Update(float delta)
    {
        HandleCamera(delta);
    }
    private void HandleCamera(float delta)
    {
        Vector3 character_euler = m_player_transform.eulerAngles;
        character_euler.y += m_mouse_delta.x * m_sensity * delta;
        m_player_transform.rotation = Quaternion.Euler(character_euler);

        m_camera_vertical_angle -= m_mouse_delta.y * m_sensity * delta;
        m_camera_vertical_angle = Mathf.Clamp(m_camera_vertical_angle, m_camera_vertical_limit.x, m_camera_vertical_limit.y);

        Vector3 camera_euler = new Vector3(m_camera_vertical_angle, 0.0f, 0.0f);
        m_camera_vertical_handler.localRotation = Quaternion.Euler(camera_euler);
    }
    public void SetInputMouseDelta(Vector2 delta) { m_mouse_delta = delta; }
}
