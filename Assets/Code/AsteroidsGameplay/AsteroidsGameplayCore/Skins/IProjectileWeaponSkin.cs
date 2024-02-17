using UnityEngine;

namespace Asteroids2D.Gameplay.Skins {

    public interface IProjectileWeaponSkin {

        public Transform ProjectileShotStartTransform { get; }

        public void VisualizeProjectileShot();
    }
}
