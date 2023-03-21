﻿using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class WalkingEnemyView : EnemyView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _enemySpriteRenderer;
        [SerializeField] private GameObject _attentionSprite;
        private float _timerAttention = 1f;

        public Animator Animator => _animator;
        public Vector2 MoveDirection { get; set; } = Vector2.zero;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public GameObject AttentionSprite => _attentionSprite;


        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;

            _target = col.transform;
            _rigidbody2D.velocity = Vector2.zero;
            OnFindTarget?.Invoke();
        }


        public void Move()
        {
            if (!HasTarget)
            {
                Patrol();
            }
            else
            {
                Chase();
            }

            FlipSprite();
        }

        private void Chase()
        {
            AttentionSpriteStatus();
            
            MoveDirection = transform.position.x - _target.transform.position.x > 0 ? Vector2.left : Vector2.right;

            if ((_enemySpriteRenderer.flipX && MoveDirection.x < 0) || (!_enemySpriteRenderer.flipX && MoveDirection.x > 0))
            {
                _rigidbody2D.velocity = Vector2.zero;
            }

            _rigidbody2D.velocity += MoveDirection * 15 * Time.deltaTime;
        }

        private void AttentionSpriteStatus()
        {
            if (_attentionSprite.activeSelf)
            {
                _timerAttention -= Time.deltaTime;
                if (_timerAttention <= 0)
                {
                    _attentionSprite.SetActive(false);
                    _timerAttention = 2f;
                }
            }
        }

        private void Patrol()
        {
            _rigidbody2D.velocity += MoveDirection * 15 * Time.deltaTime;
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