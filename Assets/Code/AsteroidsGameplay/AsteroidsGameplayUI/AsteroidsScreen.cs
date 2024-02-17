using UnityEngine;

namespace Asteroids.Core.UI {

    public class AsteroidsScreen : MonoBehaviour {

        public virtual void Show() {
            gameObject.SetActive(true);
        }

        public virtual void Hide() {
            gameObject.SetActive(false);
        }
    }
}