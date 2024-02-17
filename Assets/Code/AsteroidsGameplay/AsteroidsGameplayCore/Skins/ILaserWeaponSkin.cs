using UnityEngine;

namespace Asteroids2D.Gameplay.Skins {

    public interface ILaserWeaponSkin {

        public Transform LaserShotStartTransform { get; }

        public void VisualizeLaserShot(float duration);
    }
}
