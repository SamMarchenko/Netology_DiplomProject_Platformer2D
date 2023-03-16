namespace DefaultNamespace
{
    public class EnemyModel
    {
        private EEnemyType _type;
        private int _currentHealth;
        private float _currentMoveSpeed;
        private float _damage;
        private float _attackSpeed;


        public EEnemyType Type => _type;
        public int Health => _currentHealth;
        public float MoveSpeed => _currentMoveSpeed;
        public float Damage => _damage;
        public float AttackSpeed => _attackSpeed;


        public EnemyModel(EnemyData data)
        {
            SetData(data);
        }

        private void SetData(EnemyData data)
        {
            _type = data.Type;
            _currentHealth = data.MaxHealth;
            _currentMoveSpeed = data.MoveSpeed;
            _damage = data.Damage;
            _attackSpeed = data.AtackSpeed;
        }
    }
}