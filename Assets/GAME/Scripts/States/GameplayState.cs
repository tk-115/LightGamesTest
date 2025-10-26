using Assets.GAME.Scripts.Common;
using Assets.GAME.Scripts.World.Player;

namespace Assets.GAME.Scripts.States {

    public class GameplayState : IState {

        private IStateSwitcher _switcher;
        private PlayerSelector _playerSelector;

        public GameplayState(IStateSwitcher switcher, PlayerSelector playerSelector)
        {
            _switcher = switcher;
            _playerSelector = playerSelector;
        }

        public void Enter() {
            _playerSelector.OnLastPairCompleteEvent += OnGameOver;
            _playerSelector.SetInputWork(true);
        }

        private void OnGameOver() => _switcher.SwitchState<CardSetupState>();
        
        public void Exit() {
            _playerSelector.OnLastPairCompleteEvent -= OnGameOver;
            _playerSelector.SetInputWork(false);
        }
    }
}
