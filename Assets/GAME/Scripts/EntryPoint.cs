using Assets.GAME.Scripts.Common;
using Assets.GAME.Scripts.RemoteSprites;
using Assets.GAME.Scripts.States;
using Assets.GAME.Scripts.View;
using Assets.GAME.Scripts.World;
using Assets.GAME.Scripts.World.CardSpawn;
using Assets.GAME.Scripts.World.Player;
using UnityEngine;

namespace Assets.GAME.Scripts {

    public class EntryPoint : MonoBehaviour {

        [field: SerializeField] public SpritesHolder SpritesHolder { get; private set; }
        [field: SerializeField] public CardSpawner CardSpawner { get; private set; }
        [field: SerializeField] public CameraFocus CameraFocus { get; private set; }
        [field: SerializeField] public PlayerSelector PlayerSelector { get; private set; }

        [field: SerializeField] public LoadingScreenView LoadingScreenView { get; private set; }
        [field: SerializeField] public GameplayScreenView GameplayScreenView { get; private set; }

        private IStateSwitcher _switcher;

        private void Awake() {
            _switcher = new GameStateMachine(this);
        }
    }
}