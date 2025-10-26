using UnityEngine;

namespace Assets.GAME.Scripts.World {

    [RequireComponent(typeof(SpriteRenderer))]
    public class AdaptiveFloor : MonoBehaviour {

        [SerializeField] private Camera _camera;

        private SpriteRenderer _spriteRenderer;

        public void Initialize() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            FitToScreen();
        }

        private void FitToScreen() {
            float spriteWidth = _spriteRenderer.sprite.bounds.size.x;
            float spriteHeight = _spriteRenderer.sprite.bounds.size.y;

            float worldHeight = _camera.orthographicSize * 2f;
            float worldWidth = worldHeight * _camera.aspect;

            _spriteRenderer.size = new Vector2(worldWidth, worldHeight);

            transform.position = (Vector2)_camera.transform.position;
        }
    }
}
