namespace DefaultNamespace.Signals
{
    public class PlayerHealSignal
    {
        public readonly int Heal;
        public PlayerHealSignal(int heal)
        {
            Heal = heal;
        }
    }
}