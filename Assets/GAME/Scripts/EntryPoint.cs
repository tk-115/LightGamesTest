using Assets.GAME.Scripts.RemoteSprites;
using UnityEngine;

namespace Assets.GAME.Scripts {

    public class EntryPoint : MonoBehaviour {

        [field: SerializeField] public SpritesHolder SpritesHolder { get; private set; }

        private void Awake() {
            SpritesHolder.OnLoadCompleteEvent += OnLoadingComplete;
            SpritesHolder.OnLoadFailedEvent += OnLoadingFailed;

            SpritesHolder.Initialize();

        }

        private void OnLoadingComplete() {
            Debug.Log("all setup");
        }

        private void OnLoadingFailed(string errorText) {
            Debug.Log("error: " + errorText);
        }

        private void OnDestroy() {
            SpritesHolder.OnLoadCompleteEvent -= OnLoadingComplete;
            SpritesHolder.OnLoadFailedEvent -= OnLoadingFailed;
        }
    }
}