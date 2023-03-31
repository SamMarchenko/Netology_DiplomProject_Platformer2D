using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : IDisposable
{
    private Vector2 _direction;
    private PlayerControls _controls;
    public Action<Vector2> OnMove;
    public Action OnJump;
    public Action OnBaseAttack;
    public Action OnStrongAttackStart;
    public Action OnStrongAttackEnd;
    public Action OnBlockStart;
    public Action OnBlockEnd;
    public Action OnTransform;

    public PlayerInput()
    {
        _controls = new PlayerControls();
        _controls.Player.Enable();
        _controls.Player.Move.performed += OnMovePerformed;
        _controls.Player.Move.canceled += OnMoveCanceled;
        _controls.Player.Jump.performed += OnJumpPerformed;
        _controls.Player.BaseAttack.performed += BaseAttackOnperformed;
        _controls.Player.StrongAttack.performed += StrongAttackOnperformed;
        _controls.Player.StrongAttack.canceled += StrongAttackOncanceled;
        _controls.Player.Block.performed += BlockOnperformed;
        _controls.Player.Block.canceled += BlockOncanceled;
        _controls.Player.Transform.performed += TransformOnperformed;
    }

    private void TransformOnperformed(InputAction.CallbackContext obj)
    {
        OnTransform?.Invoke();
    }

    private void BlockOncanceled(InputAction.CallbackContext obj)
    {
        OnBlockEnd?.Invoke();
    }

    private void BlockOnperformed(InputAction.CallbackContext obj)
    {
        OnBlockStart?.Invoke();
    }

    private void StrongAttackOncanceled(InputAction.CallbackContext obj)
    {
        OnStrongAttackEnd?.Invoke();
    }

    private void StrongAttackOnperformed(InputAction.CallbackContext obj)
    {
        OnStrongAttackStart?.Invoke();
    }

    private void BaseAttackOnperformed(InputAction.CallbackContext obj)
    {
        OnBaseAttack?.Invoke();
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
        _controls.Player.Move.canceled -= OnMoveCanceled;
        _controls.Player.Jump.performed -= OnJumpPerformed;
        _controls.Player.BaseAttack.performed -= BaseAttackOnperformed;
        _controls.Player.StrongAttack.performed -= StrongAttackOnperformed;
        _controls.Dispose();
    }
}