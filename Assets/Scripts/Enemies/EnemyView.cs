﻿using System;
using System.Reflection;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class EnemyView : MonoBehaviour
    {
        [SerializeField] private EEnemyType _type;
        protected Transform _target;

        public Action OnFindTarget;
        public Action OnLoseTarget;
        public Action OnTheEdgePlatform;
        
        public EEnemyType Type => _type;
        public bool HasTarget => _target != null;
        public Transform Target => _target;
    }
}