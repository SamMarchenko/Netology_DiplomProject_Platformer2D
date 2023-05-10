using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu]
public class EnemiesPreset : ScriptableObject
{
   [SerializeField] private int _levelNumber;
   [SerializeField] private List<EnemyData> _enemiesData;
   
   public int LevelNumber => _levelNumber;
   public List<EnemyData> EnemiesData => _enemiesData;
}