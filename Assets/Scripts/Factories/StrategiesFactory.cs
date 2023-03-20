using System.Collections.Generic;
using DefaultNamespace.Strategy;

namespace DefaultNamespace.Factories
{
    public class StrategiesFactory
    {
        private List<IBehaviourStrategy> _strategies = new List<IBehaviourStrategy>();

        public List<IBehaviourStrategy> CreateStrategies()
        {
            _strategies.Add(new MovingEnemyStrategy());
            _strategies.Add(new PassiveEnemyStrategy());
            _strategies.Add(new PeekOutEnemyStrategy());

            return _strategies;
        }
    }
}