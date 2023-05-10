namespace DefaultNamespace.Signals
{
    public class PlayerDamageSignal
    {
        public readonly int Damage;

        public PlayerDamageSignal(int damage)
        {
            Damage = damage;
        }
    }
}