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

        public EEnemyType Type => _type;
        public int Health => _currentHealth;
        public float MoveSpeed => _currentMoveSpeed;
        public int DamageUnit => _damageUnit;
        public int DamageProjectile => _damageProjectile;
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
            _damageUnit = data.CollisionDamage;
            _damageProjectile = data.ProjectileDamage;
            _attackSpeed = data.AtackSpeed;
        }
    }
}