using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : Singleton<InputManager>, EeInput.IPlayerActions
{
    public UnityAction<Vector2> onMouseDelta;
    public UnityAction<Vector2> onMoveDelta;
    public UnityAction onJump;

    EeInput m_input_action;

    public void Init()
    {
        m_input_action = new EeInput();
        m_input_action.Player.SetCallbacks(this);
        m_input_action.Enable();
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(onJump != null && context.performed)
        {
            onJump();
        }
    }

    public void OnMouseDelta(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(onMouseDelta != null && context.performed)
        {
            onMouseDelta(context.ReadValue<Vector2>());
        }
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (onMoveDelta != null && context.performed)
        {
            onMoveDelta(context.ReadValue<Vector2>());
        }
    }
}
