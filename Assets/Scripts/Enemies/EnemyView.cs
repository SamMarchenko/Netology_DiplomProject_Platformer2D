using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private EEnemyType _type;
        
        public EEnemyType Type => _type;
    }
}