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
        Enemy,
        Projectile
    }

    public enum EAnimStates
    {
        Idle,
        Run,
        Jump
    }

    public enum EPlatformType
    {
        WalkingEnemyPlatform,
        FlyingEnemyPlatform
    }
}