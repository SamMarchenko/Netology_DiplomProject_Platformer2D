using DefaultNamespace.Projectiles;
using UnityEngine;

namespace DefaultNamespace.Factories
{
    public class ProjectileFactory
    {
        private readonly ProjectileView _fireProjectilePref;

        public ProjectileFactory(ProjectileView fireProjectilePref)
        {
            _fireProjectilePref = fireProjectilePref;
        }

        public ProjectileView CreateProjectile()
        {
            var projectile = MonoBehaviour.Instantiate(_fireProjectilePref);
            return projectile;
        }
    }
}