using UnityEngine;

namespace DefaultNamespace.Players.MVC
{
    public class PlayerModel
    {
        private int _currentHealth;
        private float _currentMoveSpeed;
        private float _currentJumpForce;
        private int _maxJumps;
       

        public int Health => _currentHealth;
        public float MoveSpeed => _currentMoveSpeed;
        public float JumpForce => _currentJumpForce;
        public int MaxJumps => _maxJumps;

        
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
        }
    }
}