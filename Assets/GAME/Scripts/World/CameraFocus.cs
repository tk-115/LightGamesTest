using Assets.GAME.Scripts.Card;
using UnityEngine;

namespace Assets.GAME.Scripts.World {

    public class CameraFocus : MonoBehaviour {

        [SerializeField] private Camera _camera;
        [SerializeField] private float _padding;

        public void FocusOnCards(CardMain[] cards) {

            Bounds bounds = new Bounds(cards[0].transform.position, Vector3.zero);

            foreach (CardMain obj in cards) bounds.Encapsulate(obj.transform.position);
            
            //Padding add
            bounds.Expand(_padding * 2f);

            //Camera position
            _camera.transform.position = new Vector3(bounds.center.x, bounds.center.y, _camera.transform.position.z);

            //Orthographic size
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float verticalSize = bounds.size.y / 2f;
            float horizontalSize = (bounds.size.x / 2f) / screenAspect;

            _camera.orthographicSize = Mathf.Max(verticalSize, horizontalSize);
        }
    }
}
