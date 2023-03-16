using System;
using UnityEngine;

namespace DefaultNamespace.Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;

        public Action<Collider2D> OnUnderFeetYes;
        public Action<Collider2D> OnUnderFeetNo;
        public Animator Animator => _animator;
        public Vector2 MoveDirection { get; set; } = Vector2.zero;
        public int JumpsCount { get; set; } = 0;
        public bool IsGrounded { get; set; }
        public bool IsJumping { get; set; }
        

        public void Move(float speed)
        {
            if (MoveDirection == Vector2.zero)
            {
                return;
            }
            
            _rigidbody2D.velocity += MoveDirection * speed * Time.deltaTime;
            TurnPlayerView(MoveDirection);
        }

        public void Jump(float jumpForce)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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