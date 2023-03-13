using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.SO
{
    [CreateAssetMenu(fileName = "NewEnemiesPreset", menuName = "CharactersConfigs/EnemiesPreset")]
    public class EnemiesPresets : ScriptableObject
    {
        [SerializeField] List<EnemyParameters>  _enemies;
    }
}