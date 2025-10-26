using Assets.GAME.Scripts.Common;
using Assets.GAME.Scripts.World;

namespace Assets.GAME.Scripts.States {

    public class CardSetupState : IState {

        private IStateSwitcher _switcher;
        private CardSpawner _spawner;

        public CardSetupState(IStateSwitcher switcher, CardSpawner spawner)
        {
            _switcher = switcher;
            _spawner = spawner;
        }

        public void Enter() {
            _spawner.Initialize();
            _switcher.SwitchState<GameplayState>();
        }

        public void Exit() { }

        public void Update() { }
    }
}
