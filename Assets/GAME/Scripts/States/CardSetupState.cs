using Assets.GAME.Scripts.Common;
using Assets.GAME.Scripts.World;
using Assets.GAME.Scripts.World.CardSpawn;

namespace Assets.GAME.Scripts.States {

    public class CardSetupState : IState {

        private IStateSwitcher _switcher;
        private CardSpawner _spawner;
        private CameraFocus _cameraFocus;

        public CardSetupState(IStateSwitcher switcher, CardSpawner spawner, CameraFocus cameraFocus)
        {
            _switcher = switcher;
            _spawner = spawner;
            _cameraFocus = cameraFocus;
        }

        public void Enter() {
            _spawner.Initialize();
            _cameraFocus.FocusOnCards(_spawner.SpawnedCards.ToArray());
            _switcher.SwitchState<GameplayState>();
        }

        public void Exit() { }

        public void Update() { }
    }
}
