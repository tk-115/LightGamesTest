using Assets.GAME.Scripts.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.GAME.Scripts.States {

    public class GameStateMachine : IStateSwitcher {

        private List<IState> _allStates;
        private IState _currentState;

        public GameStateMachine(EntryPoint entryPoint) {

            _allStates = new List<IState>() {

                new LoadingState(this, entryPoint.LoadingScreenView, entryPoint.SpritesHolder),

                new CardSetupState(this, entryPoint.CardSpawner, entryPoint.CameraFocus, entryPoint.PlayerSelector),

                new GameplayState(this, entryPoint.PlayerSelector)
            };

            _currentState = _allStates[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IState {

            IState state = _allStates.FirstOrDefault(state => state is T);

            if (state == null) {
                Debug.Log("Required state not registered!");
                return;
            }

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}
