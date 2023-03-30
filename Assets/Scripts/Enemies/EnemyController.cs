using System;
using System.Collections.Generic;
using DefaultNamespace.Factories;
using DefaultNamespace.Signals;
using DefaultNamespace.Strategy;

namespace DefaultNamespace
{
    public class EnemyController : IDisposable, IEnemyDamageListener
    {
        private readonly List<IBehaviourStrategy> _strategies;
        private readonly EnemyView _view;
        private readonly EnemyModel _model;
        private readonly ProjectileFactory _projectileFactory;
        private readonly PlayerSignalBus _playerSignalBus;
        private readonly EnemyDamageSignalHandler _enemyDamageSignalHandler;
        private IBehaviourStrategy _currentStrategy;

        public EnemyController(List<IBehaviourStrategy> strategies, EnemyView view,
            EnemyModel model, ProjectileFactory projectileFactory, PlayerSignalBus playerSignalBus, EnemyDamageSignalHandler enemyDamageSignalHandler)
        {
            _projectileFactory = projectileFactory;
            _playerSignalBus = playerSignalBus;
            _enemyDamageSignalHandler = enemyDamageSignalHandler;
            _enemyDamageSignalHandler.AddListener(this);
            _strategies = strategies;
            _view = view;
            _view.ProjectileFactory = _projectileFactory;
            Subscribe();
            _model = model;
            SetView();
            SetStrategy(view);
            _currentStrategy.PassiveBehaviour(_view);
        }

        private void SetView()
        {
            _view.IsRequiredKilling = _model.IsRequiredKilling;
        }

        private void Subscribe()
        {
            _view.OnFindTarget += OnFindTarget;
            _view.OnLoseTarget += OnLoseTarget;
            _view.OnConnectWithPlayer += OnConnectWithPlayer;
            _view.OnDead += OnDead;
        }

        private void OnDead(EnemyView obj)
        {
            _enemyDamageSignalHandler.RemoveListener(this);
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

        public void OnEnemyDamage(EnemyDamageSignal signal)
        {
            if (signal.Enemy != _view)
            {
                return;
            }
            int damage = signal.Damage;

            _model.Health = _model.Health - damage < 0 ? 0 : _model.Health - damage;
            
            _currentStrategy.TakeDamageBehaviour(_view, _model.Health);
        }
    }
}