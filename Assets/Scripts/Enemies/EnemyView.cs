﻿using System;
using DefaultNamespace.Factories;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class EnemyView : MonoBehaviour
    {
        [SerializeField] private EEnemyType _type;
        protected Transform _target;

        public EUnitType UnitType => EUnitType.Enemy;
        public Action OnFindTarget;
        public Action OnLoseTarget;
        public Action OnTheEdgePlatform;
        public Action OnConnectWithPlayer;

        public EEnemyType Type => _type;
        public bool HasTarget => _target != null;
        public Transform Target => _target;
        public ProjectileFactory ProjectileFactory { get; set; }
    }
}