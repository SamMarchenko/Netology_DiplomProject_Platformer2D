namespace DefaultNamespace
{
    public class WalkingEnemyModel
    {
        private EEnemyType _type;
        private int _currentHealth;
        private float _currentMoveSpeed;
        private float _damage;

        public EEnemyType Type => _type;
        public int Health => _currentHealth;
        public float MoveSpeed => _currentMoveSpeed;
        public float Damage => _damage;
       


        public WalkingEnemyModel(EnemyData data)
        {
            SetData(data);
        }

        private void SetData(EnemyData data)
        {
            _type = data.Type;
            _currentHealth = data.MaxHealth;
            _currentMoveSpeed = data.MoveSpeed;
            _damage = data.Damage;
        }
    }
}