using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class WalkingEnemyView : EnemyView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _enemySpriteRenderer;

        public Animator Animator => _animator;
        public Vector3 MoveDirection { get; set; } = Vector3.zero;


        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;

            _target = col.transform;
            OnFindTarget?.Invoke();
        }


        public void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                transform.position + MoveDirection, 5 * Time.deltaTime);
            FlipSprite();
        }

        private void FlipSprite()
        {
            _enemySpriteRenderer.flipX = MoveDirection.x > 0;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            _target = null;
            OnLoseTarget?.Invoke();
        }
    }
}