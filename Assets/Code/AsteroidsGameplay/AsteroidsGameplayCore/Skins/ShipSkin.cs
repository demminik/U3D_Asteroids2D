using UnityEngine;

namespace Asteroids2D.Gameplay.Skins {

    /// <summary>
    /// Very lazy skin with all-in-one logic for both ship skin and weapon
    /// </summary>
    public class ShipSkin : SpaceObjectSkin, ILaserWeaponSkin, IProjectileWeaponSkin {

        [SerializeField]
        private Transform _mainWeaponBulletStart;
        public Transform ProjectileShotStartTransform => _mainWeaponBulletStart;

        [SerializeField]
        private Transform _laserWeaponBulletStart;
        public Transform LaserShotStartTransform => _laserWeaponBulletStart;

        [SerializeField]
        private ParticleSystem _exhaust;

        [SerializeField]
        private GameObject _laser;

        private bool _isLaserEnabled = false;
        private float _laserEndTime;

        protected override void Awake() {
            base.Awake();

            SetLaserEnabled(false);
        }

        public void SetExhauseEnabled(float exhausePower) {
            if (_exhaust != null) {
                if (exhausePower > 0f) {
                    _exhaust.Play(true);
                } else {
                    _exhaust.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                }
            }
        }

        public void VisualizeProjectileShot() {
        }

        public void VisualizeLaserShot(float duration) {
            if (_laser != null) {
                SetLaserEnabled(true);
                _laserEndTime = Time.time + duration;
            }
        }

        protected override void Update() {
            base.Update();

            if(_isLaserEnabled && Time.time >= _laserEndTime) {
                SetLaserEnabled(false);
            }
        }

        public void SetLaserEnabled(bool isEnabled) {
            if (_laser != null) {
                _isLaserEnabled = isEnabled;
                _laser.SetActive(isEnabled);
            }
        }
    }
}