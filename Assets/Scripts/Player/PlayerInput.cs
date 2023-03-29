using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : IDisposable
{
    private Vector2 _direction;
    private PlayerControls _controls;
    public Action<Vector2> OnMove;
    public Action OnJump;

    public PlayerInput()
    {
        _controls = new PlayerControls();
        _controls.Player.Enable();
        _controls.Player.Move.performed += OnMovePerformed;
        _controls.Player.Move.canceled += OnMoveCanceled;
        _controls.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        _direction = Vector2.zero;
        OnMove?.Invoke(_direction);
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        OnJump?.Invoke();
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        _direction = obj.ReadValue<Vector2>();
        OnMove?.Invoke(_direction);
    }
    
    public void Dispose()
    {
        _controls.Player.Disable();
        _controls.Player.Move.performed -= OnMovePerformed;
        _controls.Player.Jump.performed -= OnJumpPerformed;
        _controls.Dispose();
    }
}