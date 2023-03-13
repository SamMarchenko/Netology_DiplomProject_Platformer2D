using System;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;
        [SerializeField] private float _moveSpeed = 400f;
        [SerializeField] private float _jumpForce = 180f;

        public Action<Collider2D> OnUnderFeetYes;
        public Action<Collider2D> OnUnderFeetNo;
        public Vector2 Direction { get; set; } = Vector2.zero;
        public int MaxJumps { get; set; } = 2;
        public int JumpsCount { get; set; } = 0;
        public bool IsGrounded { get; set; }
        public bool IsJumping { get; set; }


        private void Update()
        {
            Move();
        }


        private void Move()
        {
            if (Direction == Vector2.zero)
            {
                return;
            }

            Debug.Log("Move");
            _rigidbody2D.velocity += Direction * _moveSpeed * Time.deltaTime;
            TurnPlayerView(Direction);
        }

        public void Jump()
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            IsJumping = true;
            JumpsCount++;
            Debug.Log($"Прыжок №{JumpsCount}");
        }

        public void TurnPlayerView(Vector2 direction) => _playerSpriteRenderer.flipX = direction.x < 0f;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            OnUnderFeetYes?.Invoke(collider);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnUnderFeetNo?.Invoke(other);
        }
    }
}