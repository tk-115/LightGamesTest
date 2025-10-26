using System;
using TMPro;
using UnityEngine;

namespace Assets.GAME.Scripts.View {

    public class LoadingScreenView : ScreenViewBase {

        public event Action OnRetryButtonPressedEvent;

        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private GameObject _retryButton;

        public void SetRetryButtonActive(bool flag) => _retryButton.SetActive(flag);

        public void OnRetryButtonPressed() => OnRetryButtonPressedEvent?.Invoke();

        public void SetStatusText(string text) => _statusText.text = text;
    }
}
