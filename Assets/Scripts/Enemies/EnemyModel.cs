namespace DefaultNamespace
{
    public class EnemyModel
    {
        private EEnemyType _type;
        private int _currentHealth;
        private float _currentMoveSpeed;
        private int _damageUnit;
        private int _damageProjectile;
        private float _attackSpeed;
        private bool _isRequiredKilling;

        public EEnemyType Type => _type;
        public int Health
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }
        public float MoveSpeed => _currentMoveSpeed;
        public int DamageUnit => _damageUnit;
        public int DamageProjectile => _damageProjectile;
        public float AttackSpeed => _attackSpeed;

        public bool IsRequiredKilling => _isRequiredKilling;

        public EnemyModel(EnemyData data)
        {
            SetData(data);
        }

        private void SetData(EnemyData data)
        {
            _type = data.Type;
            _currentHealth = data.MaxHealth;
            _currentMoveSpeed = data.MoveSpeed;
            _damageUnit = data.CollisionDamage;
            _damageProjectile = data.ProjectileDamage;
            _attackSpeed = data.AtackSpeed;
            _isRequiredKilling = data.IsRequiredKilling;
        }
    }
}