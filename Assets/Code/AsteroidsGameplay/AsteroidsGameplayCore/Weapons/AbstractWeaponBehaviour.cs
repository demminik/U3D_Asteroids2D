using Asteroids.Core.SpaceObjects.Modules;
using UnityEngine;

namespace Asteroids2D.Gameplay.Weapons {

    public abstract class AbstractWeaponBehaviour : MonoBehaviour {

        protected SpaceObjectWeaponModule _weaponModule;
        public SpaceObjectWeaponModule WeaponModule {
            get => _weaponModule;
            set {
                ClearWeaponModule();
                _weaponModule = value;
                _weaponModule.OnShot += ExecuteWeaponShot;
            }
        }

        protected virtual void OnDestroy() {
            ClearWeaponModule();
        }

        protected void ClearWeaponModule() {
            if (_weaponModule != null) {
                _weaponModule.OnShot -= ExecuteWeaponShot;
                _weaponModule = null;
            }
        }

        protected abstract void ExecuteWeaponShot();
    }
}