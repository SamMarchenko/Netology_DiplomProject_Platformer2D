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
        public Action OnFarFromPlatform;
        public Action<EUnitType> OnConnectWithPlayer;
        public Action<EnemyView> OnDead;
        public bool IsDead;

        public EEnemyType Type => _type;
        public bool HasTarget => _target != null;
        public Transform Target
        {
            get => _target;
            set => _target = value;
        }
        public bool IsRequiredKilling;
        public Vector3 DamageShakeForce = new Vector3(4f, 4f, 0);

        public ProjectileFactory ProjectileFactory { get; set; }
        
        public void Dead()
        {
            OnDead?.Invoke(this);
            Destroy(gameObject);
        }
    }
}