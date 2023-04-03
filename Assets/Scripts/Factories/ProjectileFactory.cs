using DefaultNamespace.Projectiles;
using UnityEngine;

namespace DefaultNamespace.Factories
{
    public class ProjectileFactory
    {
        private readonly ProjectilesPreset _projectilesPreset;

        public ProjectileFactory(ProjectilesPreset projectilesPreset)
        {
            _projectilesPreset = projectilesPreset;
        }

        public ProjectileView CreateProjectile(EUnitType owner, EProjectileType type)
        {
            foreach (var projectileData in _projectilesPreset.ProjectilesData)
            {
                if (projectileData.Owner == owner)
                {
                    var projectile = MonoBehaviour.Instantiate(projectileData.Prefab);
                    projectile.Init(owner,type, projectileData.MoveSpeed, projectileData.ProjectileDamage);

                    return projectile;
                }
            }
            return null;
        }
        
    }
}