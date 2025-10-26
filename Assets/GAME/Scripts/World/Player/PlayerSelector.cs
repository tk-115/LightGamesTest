using Assets.GAME.Scripts.Card;
using System;
using UnityEngine;

namespace Assets.GAME.Scripts.World.Player {

    public class PlayerSelector : MonoBehaviour {

        public event Action OnLastPairCompleteEvent;
        public event Action<int> OnPairCompleteEvent;

        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerSelectorConfig _config;

        private CardMain _firstSelectedCard = null;
        private CardMain _secondSelectedCard = null;

        private bool _inputWork = false;
        private bool _timeoutWork = false;

        private int _pairsComplete;
        private int _targetPairsCount;
        private int _totalPairsComplete;
        private float _timer;

        public void ResetPairsComplete() => _pairsComplete = 0;

        public void SetTargetPairsCount(int value) => _targetPairsCount = value;

        public void SetInputWork(bool flag) => _inputWork = flag;

        public void SetTimeoutWork(bool flag) {
            _timer = 0;
            _timeoutWork = flag;
        }

        private void HandleMouseInput() {
            if (Input.GetMouseButtonDown(0)) {
                Vector2 worldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                SelectCardAt(worldPos);
            }
        }

        private void HandleTouchInput() {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
                Vector2 worldPos = _camera.ScreenToWorldPoint(Input.touches[0].position);
                SelectCardAt(worldPos);
            }
        }

        private void SelectCardAt(Vector2 worldPos) {
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f, _config.CardLayer);

            if (hit.collider != null) {
                CardMain card = hit.collider.GetComponent<CardMain>();

                if (card != null) OnCardSelected(card);
            }
        }

        private void OnCardSelected(CardMain card) {
            if (_firstSelectedCard == null) {
                _firstSelectedCard = card;
                card.Select();
                return;
            }

            if (card == _firstSelectedCard) return;

            if (_secondSelectedCard == null) {
                _secondSelectedCard = card;
                card.Select();
                SetInputWork(false);
                SetTimeoutWork(true);
            }
        }

        private void ResolvePair() {
            if (_firstSelectedCard == null || _secondSelectedCard == null) return;

            if (_firstSelectedCard.ID == _secondSelectedCard.ID) {
                _firstSelectedCard.Hide();
                _secondSelectedCard.Hide();

                _pairsComplete++;
                _totalPairsComplete++;
                OnPairCompleteEvent?.Invoke(_totalPairsComplete);
            }
            else {
                _firstSelectedCard.Deselect();
                _secondSelectedCard.Deselect();
            }

            SetInputWork(true);
            _firstSelectedCard = _secondSelectedCard = null;

            if (_pairsComplete >= _targetPairsCount) OnLastPairCompleteEvent?.Invoke();
        }

        private void Update() {
            if (_inputWork == true) {
#if UNITY_EDITOR
                HandleMouseInput();
#else
                HandleTouchInput();
#endif
            }

            if (_timeoutWork == true) {
                _timer += Time.deltaTime;

                if (_timer >= _config.TimeoutDelay) {
                    SetTimeoutWork(false);
                    ResolvePair();
                }
            }
        }
    }
}