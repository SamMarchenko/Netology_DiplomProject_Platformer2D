using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Projectiles;
using UnityEngine;

[CreateAssetMenu]
public class ProjectilesPreset : ScriptableObject
{
    [SerializeField] private List<ProjectileData> _projectilesData;
    public List<ProjectileData> ProjectilesData => _projectilesData;
}