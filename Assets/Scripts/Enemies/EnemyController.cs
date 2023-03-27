using System;
using System.Collections.Generic;
using DefaultNamespace.Factories;
using DefaultNamespace.Signals;
using DefaultNamespace.Strategy;

namespace DefaultNamespace
{
    public class EnemyController : IDisposable
    {
        private readonly List<IBehaviourStrategy> _strategies;
        private readonly EnemyView _view;
        private readonly EnemyModel _model;
        private readonly ProjectileFactory _projectileFactory;
        private readonly PlayerSignalBus _playerSignalBus;
        private IBehaviourStrategy _currentStrategy;

        public EnemyController(List<IBehaviourStrategy> strategies, EnemyView view,
            EnemyModel model, ProjectileFactory projectileFactory, PlayerSignalBus playerSignalBus)
        {
            _projectileFactory = projectileFactory;
            _playerSignalBus = playerSignalBus;
            _strategies = strategies;
            _view = view;
            _view.ProjectileFactory = _projectileFactory;
            Subscribe();
            _model = model;

            SetStrategy(view);
            _currentStrategy.PassiveBehaviour(_view);
        }

        private void Subscribe()
        {
            _view.OnFindTarget += OnFindTarget;
            _view.OnLoseTarget += OnLoseTarget;
            _view.OnConnectWithPlayer += OnConnectWithPlayer;
        }

        private void UnSubscribe()
        {
            _view.OnFindTarget -= OnFindTarget;
            _view.OnLoseTarget -= OnLoseTarget;
            _view.OnConnectWithPlayer -= OnConnectWithPlayer;
        }

        private void OnConnectWithPlayer(EUnitType unit)
        {
            switch (unit)
            {
                case EUnitType.Enemy:
                    _playerSignalBus.PlayerTakeDamage(new PlayerDamageSignal(_model.DamageUnit));
                    break;
                case EUnitType.Projectile:
                    _playerSignalBus.PlayerTakeDamage(new PlayerDamageSignal(_model.DamageProjectile));
                    break;
            }
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

        public void Dispose()
        {
            UnSubscribe();
        }
    }
}