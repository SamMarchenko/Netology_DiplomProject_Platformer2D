using System;
using DefaultNamespace.Signals;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : IDisposable, IPauseListener
{
    private Vector2 _direction;
    private PlayerControls _controls;
    private bool _isPause;
    
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
        if (_isPause) return;
        OnTransform?.Invoke();
    }

    private void BlockOncanceled(InputAction.CallbackContext obj)
    {
        if (_isPause) return;
        OnBlockEnd?.Invoke();
    }

    private void BlockOnperformed(InputAction.CallbackContext obj)
    {
        if (_isPause) return;
        OnBlockStart?.Invoke();
    }

    private void StrongAttackOncanceled(InputAction.CallbackContext obj)
    {
        if (_isPause) return;
        OnStrongAttackEnd?.Invoke();
    }

    private void StrongAttackOnperformed(InputAction.CallbackContext obj)
    {
        if (_isPause) return;
        OnStrongAttackStart?.Invoke();
    }

    private void BaseAttackOnperformed(InputAction.CallbackContext obj)
    {
        if (_isPause) return;
        OnBaseAttack?.Invoke();
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        if (_isPause) return;
        _direction = Vector2.zero;
        OnMove?.Invoke(_direction);
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        if (_isPause) return;
        OnJump?.Invoke();
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        if (_isPause) return;
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

    public void OnPause(PauseSignal signal)
    {
        _isPause = signal.IsPause;
    }
}