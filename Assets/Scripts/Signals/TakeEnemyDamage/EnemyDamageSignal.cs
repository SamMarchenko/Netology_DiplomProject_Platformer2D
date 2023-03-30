namespace DefaultNamespace.Signals
{
    public class EnemyDamageSignal
    {
        public readonly EnemyView Enemy;
        public readonly int Damage;

        public EnemyDamageSignal(EnemyView enemy, int damage)
        {
            Enemy = enemy;
            Damage = damage;
        }
    }
}