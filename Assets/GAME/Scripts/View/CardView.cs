using System;
using UnityEngine;

namespace Assets.GAME.Scripts.View {

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class CardView : MonoBehaviour {

        public event Action OnCardRotateEvent;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        public void Initialize() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        public void OnCardRotate() => OnCardRotateEvent?.Invoke();

        public void PlayAnimation(string key) => _animator.Play(key, 0, 0);

        public void SetCardSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;
    }
}
