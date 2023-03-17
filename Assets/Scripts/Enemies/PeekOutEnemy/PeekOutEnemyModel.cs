namespace DefaultNamespace
{
    public class PeekOutEnemyModel
    {
        private EEnemyType _type;
        private int _currentHealth;
        private float _damage;
        private float _attackSpeed;
        
        public EEnemyType Type => _type;
        public int Health => _currentHealth;
        public float Damage => _damage;
        public float AttackSpeed => _attackSpeed;


        public PeekOutEnemyModel(EnemyData data)
        {
            SetData(data);
        }

        private void SetData(EnemyData data)
        {
            _type = data.Type;
            _currentHealth = data.MaxHealth;
            _damage = data.Damage;
            _attackSpeed = data.AtackSpeed;
        }
    }
}