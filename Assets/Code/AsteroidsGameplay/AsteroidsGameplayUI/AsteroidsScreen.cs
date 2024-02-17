using UnityEngine;

namespace Asteroids.Core.UI {

    /// <summary>
    /// Very lazy HUD, non-reactive, just per-frame data reading
    /// </summary>
    public class AsteroidsScreen : MonoBehaviour {

        public virtual void Show() {
            gameObject.SetActive(true);
        }

        public virtual void Hide() {
            gameObject.SetActive(false);
        }
    }
}