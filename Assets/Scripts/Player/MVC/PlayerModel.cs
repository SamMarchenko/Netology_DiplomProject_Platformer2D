using UnityEngine;

namespace DefaultNamespace.Players.MVC
{
    public class PlayerModel
    {
        private int _currentHealth;
        private float _currentMoveSpeed;
        private float _currentJumpForce;
        private int _maxJumps;
        private int _damage;
       

        public int Health
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public float MoveSpeed => _currentMoveSpeed;
        public float JumpForce => _currentJumpForce;
        public int MaxJumps => _maxJumps;
        public int Damage => _damage;

        
        public PlayerModel(PlayerData data)
        {
            SetData(data);
        }

        private void SetData(PlayerData data)
        {
            _currentHealth = data.MaxHealth;
            _currentMoveSpeed = data.MoveSpeed;
            _currentJumpForce = data.JumpForce;
            _maxJumps = data.MaxJumps;
            _damage = data.Damage;
        }
    }
}