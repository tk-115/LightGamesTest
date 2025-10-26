using Assets.GAME.Scripts.Common;
using Assets.GAME.Scripts.View;
using Assets.GAME.Scripts.World.Player;

namespace Assets.GAME.Scripts.States {

    public class GameplayState : IState {

        private IStateSwitcher _switcher;
        private PlayerSelector _playerSelector;
        private GameplayScreenView _view;

        public GameplayState(IStateSwitcher switcher, PlayerSelector playerSelector, GameplayScreenView view)
        {
            _switcher = switcher;
            _playerSelector = playerSelector;
            _view = view;
        }

        public void Enter() {
            _playerSelector.OnLastPairCompleteEvent += OnGameOver;
            _playerSelector.OnPairCompleteEvent += OnPairComplete;

            _playerSelector.SetInputWork(true);
            _view.Show();
        }

        private void OnPairComplete(int pairsCount) => _view.SetPairsCollected(pairsCount);

        private void OnGameOver() => _switcher.SwitchState<CardSetupState>();
        
        public void Exit() {
            _playerSelector.OnLastPairCompleteEvent -= OnGameOver;
            _playerSelector.OnPairCompleteEvent -= OnPairComplete;

            _playerSelector.SetInputWork(false);
            _view.Hide();
        }
    }
}
