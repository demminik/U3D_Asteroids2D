using Asteroids.Core.SpaceObjects.Modules;
using Asteroids2D.Gameplay.Common;
using Asteroids2D.Gameplay.Interactions;
using System;
using UnityEngine;

namespace Asteroids2D.Gameplay.Skins {

    public class SpaceObjectSkin : MonoBehaviour, IActivatable, IHitReceiver {

        [SerializeField]
        private float _activationDuration = 0.5f;

        public bool IsActivated => _activationLogic?.IsActivated ?? false;

        public event Action<Collider> OnCollided;
        public event Action<EDamageType, int> OnHitReceived;

        private DefaultSpaceObjectSkinActivationLogic _activationLogic;

        protected virtual void Awake() {
            _activationLogic = new DefaultSpaceObjectSkinActivationLogic(_activationDuration, transform);
        }

        protected virtual void Update() {
            _activationLogic.Tick(Time.deltaTime);
        }

        protected virtual void OnTriggerEnter(Collider other) {
            if (IsActivated) {
                OnCollided?.Invoke(other);
            }
        }

        private void OnDestroy() {
            OnCollided = null;

            if(_activationLogic != null ) {
                _activationLogic.Dispose();
                _activationLogic = null;
            }
        }

        public void Activate() {
            _activationLogic?.Activate();
        }

        public void Deactivate() {
            _activationLogic?.Deactivate();
        }

        public void Hit(EDamageType damageType, int damageAmount) {
            OnHitReceived?.Invoke(damageType, damageAmount);
        }
    }
}