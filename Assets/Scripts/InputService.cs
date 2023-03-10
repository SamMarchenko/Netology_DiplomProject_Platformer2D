using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService
{
    private Vector2 _direction;
    private PlayerControls _controls;
    public Action<Vector2> OnMove;
    public Action OnJump;

    public InputService()
    {
        _controls = new PlayerControls();
        _controls.Player.Enable();
        _controls.Player.Move.performed += MoveOnperformed;
        _controls.Player.Jump.performed +=  JumpOnperformed;
        //todo: где отписываться не в монобехе?
    }
    
    private void JumpOnperformed(InputAction.CallbackContext obj)
    {
        OnJump?.Invoke();
    }
    
    private void MoveOnperformed(InputAction.CallbackContext obj)
    {
        _direction = obj.ReadValue<Vector2>();
        OnMove?.Invoke(_direction);
    }
    
    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    private void OnDestroy()
    {
        _controls.Player.Move.performed -= MoveOnperformed;
        _controls.Player.Jump.performed -= JumpOnperformed;
        _controls.Dispose();
    }

   

   
}
