using System;
using DefaultNamespace.Projectiles;

namespace DefaultNamespace
{
    [Serializable]
    public class ProjectileData
    {
        public ProjectileView Prefab;
        public EUnitType Owner;
        public float MoveSpeed;
        public int ProjectileDamage;
    }
}