using System;
using System.Collections.Generic;
using Refactor.States;
using UnityEngine;
using Zenject;

namespace Refactor
{
    public class GameStateMachine : MonoBehaviour
    {
        private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _activeState;

        [Inject]
        public void Construct(List<IState> states)
        {
            Debug.Log("GameStateMachine");

            foreach (var state in states)
            {
                _states[state.GetType()] = state;
            }
            
            foreach (var state in _states.Values)
            {
                state.InjectStateMachine(this);
            }

            DontDestroyOnLoad(this);

            Enter<MainMenuLoopState>();
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IState =>
            _states[typeof(TState)] as TState;
    }
}