using Asteroids.Core.Meta;
using Asteroids2D.Gameplay.Entities;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Core.UI {

    /// <summary>
    /// Very lazy HUD, non-reactive, just per-frame data reading
    /// </summary>
    public class AsteroidsHUD : AsteroidsScreen, IDisposable {

        [SerializeField]
        private TMP_Text _txtScore;

        [SerializeField]
        private TMP_Text _txtCoordinates;

        [SerializeField]
        private TMP_Text _txtRotationAngle;

        [SerializeField]
        private TMP_Text _txtSpeed;

        [SerializeField]
        private TMP_Text _txtLaserCharges;

        [SerializeField]
        private Image _imgLaserAmmoRestoreCooldown;

        [SerializeField]
        private Image _imgLaserShotCooldown;

        private PlayerShip _playerShip;
        private bool _hasValidTarget;

        private IScoreProvider _scoreProvider;

        public void SetTarget(PlayerShip ship) {
            _playerShip = ship;
            _hasValidTarget = _playerShip != null;
        }

        public void SetScoreProvider(IScoreProvider scoreProvider) {
            _scoreProvider = scoreProvider;
        }

        private void Update() {
            if (_hasValidTarget) {
                // TODO: need to pass world forward/up values in constructor for more flexibility
                var worldForward = Vector3.forward;

                _txtCoordinates.text = $"Coordinates x:{_playerShip.Model.Transform.Position.x.ToString("F2")} z:{_playerShip.Model.Transform.Position.z.ToString("F2")}";
                _txtRotationAngle.text = $"Angle: {Quaternion.Angle(Quaternion.Euler(worldForward), _playerShip.Model.Transform.Rotation).ToString("F0")}";
                _txtSpeed.text = $"Speed: {_playerShip.Model.MovementModule.CurrentSpeed.ToString("F0")}";

                if (_playerShip.Model.Weapon2 != null) {
                    _txtLaserCharges.text = $"{_playerShip.Model.Weapon2.CurrentParams.CurrentAmmo}";
                    _imgLaserAmmoRestoreCooldown.fillAmount = _playerShip.Model.Weapon2.WeaponParams.AmmoRestoreDuration > 0f
                        ? _playerShip.Model.Weapon2.CurrentParams.AmmoRestoreCooldown / _playerShip.Model.Weapon2.WeaponParams.AmmoRestoreDuration
                        : 0f;
                    _imgLaserShotCooldown.fillAmount = _playerShip.Model.Weapon2.WeaponParams.ShotCooldown > 0f
                        ? _playerShip.Model.Weapon2.CurrentParams.ShotCooldown / _playerShip.Model.Weapon2.WeaponParams.ShotCooldown
                        : 0f;
                } else {
                    _txtLaserCharges.text = 0.ToString();
                    _imgLaserAmmoRestoreCooldown.fillAmount = 0f;
                    _imgLaserAmmoRestoreCooldown.fillAmount = 0f;
                }
            }

            if(_scoreProvider != null) {
                _txtScore.text = $"Score: {_scoreProvider.Score}";
            }
        }

        private void OnDestroy() {
            Dispose();
        }

        public void Dispose() {
            _playerShip = null;
            _hasValidTarget = false;
            _scoreProvider = null;
        }
    }
}