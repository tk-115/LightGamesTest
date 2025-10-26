using Assets.GAME.Scripts.Common;
using Assets.GAME.Scripts.World;
using Assets.GAME.Scripts.World.CardSpawn;
using Assets.GAME.Scripts.World.Player;

namespace Assets.GAME.Scripts.States {

    public class CardSetupState : IState {

        private IStateSwitcher _switcher;
        private CardSpawner _spawner;
        private CameraFocus _cameraFocus;
        private PlayerSelector _playerSelector;
        private AdaptiveFloor _adaptiveFloor;

        public CardSetupState(IStateSwitcher switcher, CardSpawner spawner, 
            CameraFocus cameraFocus, PlayerSelector playerSelector, AdaptiveFloor adaptiveFloor)
        {
            _switcher = switcher;
            _spawner = spawner;
            _cameraFocus = cameraFocus;
            _playerSelector = playerSelector;
            _adaptiveFloor = adaptiveFloor;
        }

        public void Enter() {
            _spawner.Initialize();
            _cameraFocus.FocusOnCards(_spawner.SpawnedCards.ToArray());

            _playerSelector.ResetPairsComplete();
            _playerSelector.SetTargetPairsCount(_spawner.PairCount);

            _adaptiveFloor.Initialize();

            _switcher.SwitchState<GameplayState>();
        }

        public void Exit() { }
    }
}
