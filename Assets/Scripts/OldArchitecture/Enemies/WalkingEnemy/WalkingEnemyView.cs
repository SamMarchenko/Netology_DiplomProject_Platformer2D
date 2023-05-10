﻿using System;
using DefaultNamespace.Players;
using UnityEngine;

namespace DefaultNamespace
{
    public class WalkingEnemyView : EnemyView
    {
        [SerializeField] private Animator _BehaviourAnimator;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _enemySpriteRenderer;
        [SerializeField] private GameObject _boomAnimation;
        [SerializeField] private GameObject _attentionSprite;
        private float _timerAttention = 1f;
        private bool _canMove = true;
        private float _startBashCount = 1f;
        private float _currentBashCount;

        public Animator BehaviourAnimator => _BehaviourAnimator;
        public GameObject BoomAnimation => _boomAnimation;
        public Vector2 MoveDirection { get; set; } = Vector2.zero;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public GameObject AttentionSprite => _attentionSprite;
        public SpriteRenderer SpriteRenderer => _enemySpriteRenderer;

        private void Start()
        {
            _currentBashCount = _startBashCount;
        }


        private void Update()
        {
            if (!_canMove)
            {
                PauseAfterDamage();
                
                return;
            }
            Move();
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

        public void ExplodeSelf()
        {
            _enemySpriteRenderer.enabled = false;
            _boomAnimation.SetActive(true);
        }

        private void PauseAfterDamage()
        {
            _rigidbody2D.velocity =  Vector2.zero;
            _currentBashCount -= Time.deltaTime;
            if (_currentBashCount <= 0)
            {
                _canMove = true;
                _currentBashCount = _startBashCount;
            }
        }

        public void TakeDamage()
        {
            if (!_canMove) return;
            
            _canMove = false;
            _currentBashCount = _startBashCount;
        }


        private void Chase()
        {
            AttentionSpriteStatus();

            MoveDirection = transform.position.x - _target.transform.position.x > 0 ? Vector2.left : Vector2.right;

            if ((_enemySpriteRenderer.flipX && MoveDirection.x < 0) ||
                (!_enemySpriteRenderer.flipX && MoveDirection.x > 0))
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
                    _timerAttention = 1f;
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


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;

            _target = col.transform;
            _rigidbody2D.velocity = Vector2.zero;
            OnFindTarget?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            _target = null;
            OnLoseTarget?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player") && !IsDead)
            {
                OnConnectWithPlayer?.Invoke(EUnitType.Enemy);
            }
        }
    }
}