﻿using System.Collections.Generic;
using DefaultNamespace.Factories;
using DefaultNamespace.Strategy;

namespace DefaultNamespace
{
    public class EnemyController
    {
        private readonly List<IBehaviourStrategy> _strategies;
        private readonly EnemyView _view;
        private readonly EnemyModel _model;
        private readonly ProjectileFactory _projectileFactory;
        private IBehaviourStrategy _currentStrategy;

        public EnemyController(List<IBehaviourStrategy> strategies, EnemyView view,
            EnemyModel model, ProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;
            _strategies = strategies;
            _view = view;
            _view.ProjectileFactory = _projectileFactory;
            _view.OnFindTarget += OnFindTarget;
            _view.OnLoseTarget += OnLoseTarget;
            _model = model;
           
            SetStrategy(view);
            _currentStrategy.PassiveBehaviour(_view);
        }
        

        private void OnLoseTarget()
        {
            _currentStrategy.PassiveBehaviour(_view);
        }

        private void OnFindTarget()
        {
           _currentStrategy.ActiveBehaviour(_view);
        }

        private void SetStrategy(EnemyView view)
        {
            switch (view.Type)
            {
                case EEnemyType.PassiveEnemy:
                    foreach (var strategy in _strategies)
                    {
                        if (strategy is PassiveEnemyStrategy)
                        {
                            _currentStrategy = strategy;
                        }
                    }
                    break;
                case EEnemyType.WalkingEnemy:
                    foreach (var strategy in _strategies)
                    {
                        if (strategy is MovingEnemyStrategy)
                        {
                            _currentStrategy = strategy;
                        }
                    }
                    break;
                case EEnemyType.FlyingEnemy:
                    foreach (var strategy in _strategies)
                    {
                        if (strategy is FlyingEnemyStrategy)
                        {
                            _currentStrategy = strategy;
                        }
                    }
                    break;
                case EEnemyType.PeekOutEnemy:
                    foreach (var strategy in _strategies)
                    {
                        if (strategy is PeekOutEnemyStrategy)
                        {
                            _currentStrategy = strategy;
                        }
                    }
                    break;
            }
        }
    }
}