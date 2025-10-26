using UnityEngine;

namespace Assets.GAME.Scripts.View {

    public abstract class ScreenViewBase : MonoBehaviour {

        [SerializeField] protected GameObject Screen;

        public virtual void Show() => Screen.SetActive(true);
        
        public virtual void Hide() => Screen.SetActive(false);
        
    }
}
