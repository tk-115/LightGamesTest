using Assets.GAME.Scripts.View;
using UnityEngine;

namespace Assets.GAME.Scripts.Card {

    [RequireComponent(typeof(BoxCollider2D))]
    public class CardMain : MonoBehaviour {

        [SerializeField] private CardView _view;
        [SerializeField] private CardConfig _config;

        private float _timer;
        private CardState _state;
        private Sprite _frontSprite;
        private Sprite _backSprite;

        public int ID { get; private set; }

        public void Initialize(int id, Sprite frontSprite, Sprite backSprite) {
            ID = id;
            _frontSprite = frontSprite;
            _backSprite = backSprite;

            _view.Initialize();
            Spawn();
        }

        private void Spawn() {
            _view.SetCardSprite(_frontSprite);
            _view.OnCardRotateEvent += OnCardRotate;
            _timer = 0;
            _state = CardState.SPAWN;
            _view.PlayAnimation(_state.ToString());
        }

        public void Select() {
            if (_state != CardState.IDLE) return;

            _timer = 0;
            _state = CardState.SELECT;
            _view.PlayAnimation(_state.ToString());
        }

        public void Deselect() {
            _timer = 0;
            _state = CardState.DESELECT;
            _view.PlayAnimation(CardState.SELECT.ToString());
        }

        private void OnCardRotate() {
            if (_state == CardState.SELECT)
                _view.SetCardSprite(_backSprite);
            else if (_state == CardState.DESELECT)
                _view.SetCardSprite(_frontSprite);
        }

        private void SpawnState() {
            if (_timer >= _config.SpawnDelay) {
                _timer = 0;
                _state = CardState.IDLE;
            }
        }

        private void SelectState() {
            if (_timer >= _config.SelectDelay) {
                _timer = 0;
                _state = CardState.SELECTED;
            }
        }

        private void DeselectState() {
            if (_timer >= _config.SelectDelay) {
                _timer = 0;
                _state = CardState.IDLE;
            }
        }

        private void HideState() {
            if (_timer >= _config.HideDelay) {
                _timer = 0;
                Destroy(gameObject);
            }
        }

        private void Update() {

            _timer += Time.deltaTime;

            switch (_state) {
                case CardState.SPAWN:
                    SpawnState();
                    break;
                case CardState.SELECT:
                    SelectState();
                    break;
                case CardState.DESELECT:
                    DeselectState();
                    break;
                case CardState.HIDE:
                    HideState();
                    break;
            }
        }

        private void OnDestroy() {
            _view.OnCardRotateEvent -= OnCardRotate;
        }
    }
}
