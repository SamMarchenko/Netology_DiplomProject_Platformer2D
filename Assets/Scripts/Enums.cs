namespace DefaultNamespace
{
    public enum EEnemyType
    {
        PassiveEnemy,
        WalkingEnemy,
        FlyingEnemy,
        PeekOutEnemy
    }

    public enum EUnitType
    {
        Player,
        TransformedPlayer,
        Enemy,
        Projectile
    }

    public enum EProjectileType
    {
        Melee,
        Range
    }

    public enum ELootType
    {
        Shield
    }

    public enum EAttackType
    {
        BaseAttack,
        StrongAttack
    }

    public enum EAnimStates
    {
        Idle,
        Run,
        Jump,
        Transform
    }

    public enum EPlatformType
    {
        WalkingEnemyPlatform,
        FlyingEnemyPlatform
    }
}