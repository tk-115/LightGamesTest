
namespace Assets.GAME.Scripts.Common {

    public interface IStateSwitcher {

        public void SwitchState<T>() where T : IState;
        public void Update();
    }
}
