namespace DefaultNamespace
{
    public class PassiveEnemyModel
    {
        private EEnemyType _type;
        private float _damage;
        public EEnemyType Type => _type;
        public float Damage => _damage;
        
        public PassiveEnemyModel(EnemyData data)
        {
            SetData(data);
        }
        
        private void SetData(EnemyData data)
        {
            _type = data.Type;
            _damage = data.Damage;
        }
    }
}