using Asteroids.Core.SpaceObjects.Modules;
using Asteroids2D.Gameplay.Skins;
using UnityEngine;

namespace Asteroids2D.Gameplay.Entities {

    public abstract class SkinnableSpaceObject<TModel, TSkin> : SpaceObject<TModel>, ISkinnableSpaceObject<TSkin>
        where TModel : Asteroids.Core.SpaceObjects.SpaceObject
        where TSkin : SpaceObjectSkin {

        [SerializeField]
        private Transform _skinRoot;

        public override bool IsActivated => base.IsActivated && (_currentSkin == null || _currentSkin.IsActivated);

        protected TSkin _currentSkin;

        protected override void OnDestroy() {
            base.OnDestroy();

            ClearCurrentSkin();
        }

        public void ApplySkin(TSkin skinInstance) {
            if (_currentSkin != null) {
                ClearCurrentSkin();
            }

            _currentSkin = skinInstance;
            _currentSkin.OnCollided += ProcessSkinCollision;
            _currentSkin.OnHitReceived += ProcessSkinHit;
            _currentSkin.transform.parent = _skinRoot != null ? _skinRoot : transform;
            _currentSkin.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            if (_isActivated) {
                _currentSkin.Activate();
            }

            ProcessSkinApplied(skinInstance);
        }

        public void ClearCurrentSkin() {
            SkinPreCleanup(_currentSkin);
            _currentSkin.Deactivate();
            _currentSkin.OnCollided -= ProcessSkinCollision;
            _currentSkin.OnHitReceived -= ProcessSkinHit;
            _currentSkin = null;
        }

        protected virtual void SkinPreCleanup(TSkin skinInstance) {
        }

        protected virtual void ProcessSkinApplied(TSkin skinInstance) {
        }

        protected virtual void ProcessSkinCollision(Collider collider) {
        }

        protected virtual void ProcessSkinHit(EDamageType damageType, int damageAmount) {
            Model.ResilienceModule.Hit(damageType, damageAmount);
        }

        public override void Activate() {
            base.Activate();

            if (_currentSkin != null) {
                _currentSkin.Activate();
            }
        }

        public override void Deactivate() {
            base.Deactivate();

            if (_currentSkin != null) {
                _currentSkin.Deactivate();
            }
        }
    }
}