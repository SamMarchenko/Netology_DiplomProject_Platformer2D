using DefaultNamespace.Players;
using UnityEngine;

namespace DefaultNamespace.FlyingEnemy
{
    public class FlyingEnemyView : EnemyView
    {
        [SerializeField] private Animator _BehaviourAnimator;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _enemySpriteRenderer;
        [SerializeField] private GameObject _attentionSprite;
        private float _timerAttention = 1f;
        private Vector3 _defaultPosition;
        private float _startBashCount = 1f;
        private float _currentBashCount;
        private bool _canMove = true;


        public Animator BehaviourAnimator => _BehaviourAnimator;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public GameObject AttentionSprite => _attentionSprite;
        public SpriteRenderer SpriteRenderer => _enemySpriteRenderer;
        public bool IsNeedBack;

        private void Start()
        {
            _currentBashCount = _startBashCount;
            _defaultPosition = transform.position;
        }


        private void Update()
        {
            Move();
        }


        public void Move()
        {
            if (!_canMove)
            {
                PauseAfterDamage();
                
                return;
            }
            if (!HasTarget)
            {
                Waiting();
            }
            else
            {
                Chase();
            }

            FlipSprite();
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
        
        private void Chase()
        {
            AttentionSpriteStatus();
            transform.position = Vector3.MoveTowards(transform.position,
                (_target.position + new Vector3(0,10,0)), 10f * Time.deltaTime);
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
        public void TakeDamage()
        {
            if (!_canMove) return;
            
            _canMove = false;
            _currentBashCount = _startBashCount;
        }

        private void Waiting()
        {
            transform.position = Vector3.MoveTowards(transform.position, _defaultPosition, 7f*Time.deltaTime);
        }

        private void FlipSprite()
        {
            if (_target != null)
            {
                _enemySpriteRenderer.flipX = transform.position.x - _target.transform.position.x < 0;
            }
            else
            {
                _enemySpriteRenderer.flipX = transform.position.x - _defaultPosition.x < 0;
            }
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (IsNeedBack)
            {
                return;
            }
            if (!col.gameObject.CompareTag("Player")) return;
            _target = col.transform;
            OnFindTarget?.Invoke();
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