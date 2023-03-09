using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _moveSpeed = 400f;
    [SerializeField] private float _jumpForce = 180f;
    private bool _isJumping;
    private bool _isGrounded;
    private int _jumpsCount = 0;
    [SerializeField] private int _maxJumps = 2;
    private PlayerControls _controls;

    public delegate void Move();

    public event Move MoveEvent;

    public void CallOnMoveEvent() => MoveEvent?.Invoke();

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
        _controls.Player.Move.performed += MoveOnperformed;
        _controls.Player.Jump.performed +=  JumpOnperformed;
    }
    
    private void JumpOnperformed(InputAction.CallbackContext obj)
    {
        if (_isGrounded)
        {
           Jump();
           return;
        }

        if (_isJumping && _jumpsCount < _maxJumps)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _isJumping = true;
        _jumpsCount++;
        Debug.Log($"Прыжок №{_jumpsCount}");
    }

    private void MoveOnperformed(InputAction.CallbackContext obj)
    {
        CallOnMoveEvent();
    }

    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        var direction = _controls.Player.Move.ReadValue<Vector2>();

        _rigidbody2D.velocity += direction * _moveSpeed * Time.deltaTime;
        //_rigidbody2D.AddForce(direction * _moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _isJumping = false;
            _jumpsCount = 0;
            //_rigidbody2D.gravityScale = 10f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
            //_rigidbody2D.gravityScale = 50f;
        }
    }
}
