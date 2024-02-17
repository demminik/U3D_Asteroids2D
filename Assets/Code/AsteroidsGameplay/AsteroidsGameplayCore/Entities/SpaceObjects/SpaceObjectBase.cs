using Asteroids.Core.SpaceObjects.Modules;
using Asteroids2D.Gameplay.Common;
using Asteroids2D.Gameplay.Input;
using Asteroids2D.Gameplay.Input.Utils;
using Asteroids2D.Gameplay.Spawning;
using System;
using UnityEngine;

namespace Asteroids2D.Gameplay.Entities {

    public abstract class SpaceObjectBase : MonoBehaviour, IControllable, IActivatable {

        protected bool _isActivated;
        public virtual bool IsActivated => _isActivated;

        protected IGameplayInputProvider _inputProvider;
        protected InputActionsQueue _inputActionsQueue;

        protected IGameplayEntitiesSpawner _entitiesSpawner;

        public event Action<GameObject, EDamageType, int> OnDeath;

        protected virtual void Update() {
            if (!IsActivated) {
                return;
            }

            if (_inputActionsQueue != null) {
                _inputActionsQueue.ProcessAndClear(ExecuteInputAction);
            }

            UpdateInternal();
        }

        protected virtual void OnDestroy() {
            ClearInputData();

            _isActivated = false;
        }

        protected virtual void UpdateInternal() {
        }

        public virtual void SetInputProvider(IGameplayInputProvider provider) {
            if (provider != null) {
                ClearInputData();

                _inputActionsQueue = new InputActionsQueue();

                _inputProvider = provider;
                _inputProvider.OnInputAction += ProcessInputAction;
            }
        }

        public virtual void SetEntitiesSpawner(IGameplayEntitiesSpawner entitiesSpawner) {
            _entitiesSpawner = entitiesSpawner;
        }

        private void ClearInputData() {
            if (_inputActionsQueue != null) {
                _inputActionsQueue.Dispose();
                _inputActionsQueue = null;
            }

            if (_inputProvider != null) {
                _inputProvider.OnInputAction -= ProcessInputAction;
                _inputProvider = null;
            }
        }

        private void ProcessInputAction(InputActionData actionData) {
            _inputActionsQueue.Add(actionData);
        }

        protected virtual void ExecuteInputAction(InputActionData actionData) {
        }

        public virtual void Activate() {
            _isActivated = true;
        }

        public virtual void Deactivate() {
            _isActivated = false;
        }

        protected void InvokeDeath(EDamageType damageType, int lethalDamageAmount) {
            OnDeath?.Invoke(gameObject, damageType, lethalDamageAmount);
        }
    }
}