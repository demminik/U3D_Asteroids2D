using Asteroids2D.Gameplay.Interactions;
using Asteroids2D.Gameplay.Skins;
using System;
using UnityEngine;

namespace Asteroids2D.Gameplay.Weapons {

    public class LaserWeaponBehaviour : AbstractWeaponBehaviour {

        [Serializable]
        private struct LaserParams {

            public float ShotDuration;
            public float ShotWidth;
            public float ShotDistance;

            public LayerMask RaycastLayerMask;
        }

        [SerializeField]
        private LaserParams _laserParams = new LaserParams() {
            ShotDuration = 0.2f,
            ShotWidth = 0.1f,
            ShotDistance = 100f,
            RaycastLayerMask = 0,
        };

        public ILaserWeaponSkin Skin { get; set; }

        private bool _isShotActive = false;
        private float _shotEndTime = float.MinValue;

        protected override void ExecuteWeaponShot() {
            _isShotActive = true;
            _shotEndTime = Time.time + _laserParams.ShotDuration;
        }

        private void Update() {
            if (_isShotActive) {
                ProcessShotLogic();

                if (Time.time >= _shotEndTime) {
                    _isShotActive = false;
                }
            }
        }

        private void ProcessShotLogic() {
            var shotTransform = transform;

            if (Skin != null) {
                if (Skin.LaserShotStartTransform != null) {
                    shotTransform = Skin.LaserShotStartTransform;
                }

                Skin.VisualizeLaserShot(_laserParams.ShotDuration);
            }

            var hits = Physics.SphereCastAll(shotTransform.position, _laserParams.ShotWidth, shotTransform.forward, _laserParams.ShotDistance, _laserParams.RaycastLayerMask, QueryTriggerInteraction.UseGlobal);
            UnityEngine.Debug.DrawRay(shotTransform.position, shotTransform.forward * _laserParams.ShotDistance);
            foreach (var hit in hits) {
                var hitReceiver = hit.collider.gameObject.GetComponent<IHitReceiver>();
                if (hitReceiver != null) {
                    hitReceiver.Hit(_weaponModule.WeaponParams.Damage.DamageType, _weaponModule.WeaponParams.Damage.DamageAmount);
                }
            }
        }
    }
}