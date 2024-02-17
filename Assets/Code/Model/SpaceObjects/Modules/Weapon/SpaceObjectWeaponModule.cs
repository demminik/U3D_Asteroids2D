using Asteroids.Core.Parameters;
using Asteroids.Core.Tickables;
using System;

namespace Asteroids.Core.SpaceObjects.Modules {

    public class SpaceObjectWeaponModule : ISpaceObjectModule, ITickable {

        public struct RuntimeParams {

            public int CurrentAmmo;
            public float AmmoRestoreCooldown;

            public float ShotCooldown;
        }

        // TODO: this can be buffable, consider it (not important for demo)
        private WeaponParams _weaponParams;
        public WeaponParams WeaponParams => _weaponParams;

        private RuntimeParams _runtimeParams;
        public RuntimeParams CurrentParams => _runtimeParams;

        public event Action OnShot;
        public event Action OnAmmoRestored;
        public event Action OnShotCooldownFinished;

        public SpaceObjectWeaponModule(WeaponParams weaponParams) {
            _weaponParams = weaponParams;

            _runtimeParams = new RuntimeParams() {
                CurrentAmmo = _weaponParams.MaxAmmo,
            };
        }

        public void Dispose() {
            OnShot = null;
            OnAmmoRestored = null;
            OnShotCooldownFinished = null;
        }

        public void Tick(float deltaTime) {
            TickShotCooldown(deltaTime);
            TickAmmoRestoration(deltaTime);
        }

        private void TickShotCooldown(float deltaTime) {
            if (_runtimeParams.ShotCooldown > 0f) {
                _runtimeParams.ShotCooldown -= deltaTime;

                if (_runtimeParams.ShotCooldown <= 0f) {
                    _runtimeParams.ShotCooldown = 0f;
                    OnShotCooldownFinished?.Invoke();
                }
            }
        }

        private void TickAmmoRestoration(float deltaTime) {
            if (_runtimeParams.AmmoRestoreCooldown > 0f) {
                _runtimeParams.AmmoRestoreCooldown -= deltaTime;

                if (_runtimeParams.AmmoRestoreCooldown <= 0f) {
                    _runtimeParams.CurrentAmmo++;
                    OnAmmoRestored?.Invoke();

                    _runtimeParams.AmmoRestoreCooldown = _runtimeParams.CurrentAmmo >= _weaponParams.MaxAmmo
                        ? 0f
                        : _runtimeParams.AmmoRestoreCooldown + _weaponParams.AmmoRestoreDuration;
                }
            }
        }

        public void Shoot() {
            if (_weaponParams.MaxAmmo > 0 && _runtimeParams.CurrentAmmo <= 0) {
                return;
            }

            if (_runtimeParams.ShotCooldown > 0f) {
                return;
            }

            _runtimeParams.ShotCooldown = _weaponParams.ShotCooldown;

            if (_weaponParams.MaxAmmo > 0) {
                _runtimeParams.CurrentAmmo--;

                if (_runtimeParams.AmmoRestoreCooldown <= 0f) {
                    _runtimeParams.AmmoRestoreCooldown = _weaponParams.AmmoRestoreDuration;
                }

            }

            OnShot?.Invoke();
        }
    }
}