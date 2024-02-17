using Asteroids.Core.Tickables;
using Asteroids2D.Gameplay.Common;
using System;
using UnityEngine;

namespace Asteroids2D.Gameplay.Skins {

    public class DefaultSpaceObjectSkinActivationLogic : ITickable, IActivatable, IDisposable {

        public bool IsActivated { get; private set; }

        public event Action OnActivated;
        public event Action OnDectivated;

        private Transform _targetTransform;
        private float _activationDuration;
        private float _activationTimeLeft;

        private Vector3 _defaultScale;

        public DefaultSpaceObjectSkinActivationLogic(float activationDuration, Transform targetTransform) {
            _activationDuration = activationDuration;
            _activationTimeLeft = 0f;

            _targetTransform = targetTransform;
            _defaultScale = targetTransform != null ? targetTransform.localScale : Vector3.one;
        }

        public void Dispose() {
            OnActivated = null;
            OnDectivated = null;
            _targetTransform = null;
        }

        public void Tick(float deltaTime) {
            if (_activationTimeLeft > 0f) {
                _activationTimeLeft -= deltaTime;

                if (_activationTimeLeft < 0f) {
                    _activationTimeLeft = 0f;
                }

                var activationProgress = 1f - (_activationTimeLeft / _activationDuration);
                ProgressActivation(activationProgress);

                if (activationProgress >= 1f) {
                    DoActivate();
                }
            }
        }

        public void Activate() {
            if(_targetTransform == null) {
                Debug.LogError($"Activation failed! Transform is invalid");
                return;
            }

            if (IsActivated || _activationTimeLeft > 0f) {
                Debug.LogError($"Activation failed! Already {(IsActivated ? "activated" : "activating")}");
                return;
            }

            if (_activationDuration > 0f) {
                _activationTimeLeft = _activationDuration;
                ProgressActivation(0f);
            } else {
                DoActivate();
            }
        }

        public void Deactivate() {
            if (IsActivated || _activationTimeLeft > 0f) {
                IsActivated = false;
                _activationTimeLeft = 0f;
                OnDectivated?.Invoke();
            }
        }

        private void DoActivate() {
            _activationTimeLeft = 0f;
            ProgressActivation(1f);
            IsActivated = true;
            OnActivated?.Invoke();
        }

        protected virtual void ProgressActivation(float progress) {
            _targetTransform.localScale = _defaultScale * Mathf.Max(0.001f, progress);
        }
    }
}