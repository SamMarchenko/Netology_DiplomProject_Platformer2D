using System;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _moveSpeed = 400f;
        [SerializeField] private float _jumpForce = 180f;
        
        public Vector2 Direction { get; set; } = Vector2.zero;
        public int MaxJumps { get; set; } = 2;
        public int  JumpsCount { get; set; } = 0;
        public bool IsGrounded { get; set; }
        public bool IsJumping { get; set; }


        public void Jump()
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            IsJumping = true;
            JumpsCount++;
            Debug.Log($"Прыжок №{JumpsCount}");
        }

        private void Update()
        {
            Move();
        }
        
        

        public void Move()
        {
            if (Direction == Vector2.zero)
            {
                return;
            }
            Debug.Log("Move");
            _rigidbody2D.velocity += Direction * _moveSpeed * Time.deltaTime;
            
            Direction = Vector2.zero;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Ground"))
            {
                IsGrounded = true;
                IsJumping = false;
                JumpsCount = 0;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                IsGrounded = false;
            }
        }
    }
}