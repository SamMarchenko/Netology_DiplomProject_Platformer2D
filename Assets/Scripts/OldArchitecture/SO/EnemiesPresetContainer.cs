using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.SO
{
    [CreateAssetMenu]
    public class EnemiesPresetContainer : ScriptableObject
    {
        [SerializeField] private List<EnemiesPreset> _enemiesPresets;

        public List<EnemiesPreset> EnemiesPresets => _enemiesPresets;
    }
}