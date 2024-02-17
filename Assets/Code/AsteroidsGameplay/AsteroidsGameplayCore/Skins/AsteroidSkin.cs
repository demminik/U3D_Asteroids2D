using UnityEngine;

namespace Asteroids2D.Gameplay.Skins {

    public class AsteroidSkin : SpaceObjectSkin {

        [SerializeField]
        private float _rotationSpeed = 60f;

        [SerializeField]
        private AsteroidSkin _partSkin;
        public AsteroidSkin PartSkin => _partSkin;

        protected override void Update() {
            base.Update();

            transform.rotation = transform.rotation * Quaternion.AngleAxis(_rotationSpeed * Time.deltaTime, Vector3.up);
        }
    }
}