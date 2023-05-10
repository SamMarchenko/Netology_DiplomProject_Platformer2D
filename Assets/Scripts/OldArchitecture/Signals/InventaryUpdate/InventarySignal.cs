using DefaultNamespace.UI;

namespace DefaultNamespace.Signals
{
    public class InventarySignal
    {
        public readonly EInventaryType Type;

        public InventarySignal(EInventaryType type)
        {
            Type = type;
        }
    }
}