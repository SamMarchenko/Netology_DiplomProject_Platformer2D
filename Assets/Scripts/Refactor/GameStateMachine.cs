using System;
using System.Collections.Generic;
using Refactor.States;
using UnityEngine;
using Zenject;

namespace Refactor
{
    public class GameStateMachine : MonoBehaviour, ICoroutineRunner
    {
        private Dictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>();
        private IExitableState _activeState;

        [Inject]
        public void Construct(List<IExitableState> states)
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
        
        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }
        
        

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}