using Assets.GAME.Scripts.Common;
using Assets.GAME.Scripts.RemoteSprites;
using Assets.GAME.Scripts.View;
using UnityEngine;

namespace Assets.GAME.Scripts.States {

    public class LoadingState : IState {

        private IStateSwitcher _switcher;
        private SpritesHolder _spritesHolder;
        private LoadingScreenView _view;
        
        public LoadingState(IStateSwitcher switcher, LoadingScreenView view, SpritesHolder spritesHolder)
        {
            _switcher = switcher;
            _view = view;
            _spritesHolder = spritesHolder;
        }

        public void Enter() {
            _view.OnRetryButtonPressedEvent += InitializeSprites;
            _view.Show();

            _spritesHolder.OnLoadCompleteEvent += OnLoadingComplete;
            _spritesHolder.OnLoadFailedEvent += OnLoadingFailed;

            InitializeSprites();
        }

        private void InitializeSprites() {
            _view.SetStatusText("Loading...");
            _view.SetRetryButtonActive(false);

            _ = _spritesHolder.Initialize().ContinueWith(task =>
            {
                if (task.Exception != null) Debug.LogError(task.Exception);
            });
        }

        private void OnLoadingComplete() => _switcher.SwitchState<CardSetupState>();

        private void OnLoadingFailed(string errorText) {
            _view.SetStatusText(errorText);
            _view.SetRetryButtonActive(true);
        }

        public void Exit() {
            _spritesHolder.OnLoadCompleteEvent -= OnLoadingComplete;
            _spritesHolder.OnLoadFailedEvent -= OnLoadingFailed;

            _view.OnRetryButtonPressedEvent -= InitializeSprites;
            _view.Hide();
        }

        public void Update() { }
    }
}
