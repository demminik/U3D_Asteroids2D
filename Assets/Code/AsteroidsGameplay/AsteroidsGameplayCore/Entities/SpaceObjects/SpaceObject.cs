using Asteroids.Core.SpaceObjects.Modules;
using UnityEngine;

namespace Asteroids2D.Gameplay.Entities {

    public abstract class SpaceObject<TModel> : SpaceObjectBase
        where TModel : Asteroids.Core.SpaceObjects.SpaceObject {

        public TModel Model { get; protected set; }

        protected override void UpdateInternal() {
            base.UpdateInternal();

            if(Model != null) {
                Model.Tick(Time.deltaTime);
            }
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            ClearCurrentModel();

            _isActivated = false;
        }

        public void InitializeModel(TModel model) {
            if (model != null) {
                ClearCurrentModel();
                Model = model;
                Model.ResilienceModule.OnDeath += ProcessModelDeath;

                InitializeModelInternal();
            }
        }

        protected virtual void InitializeModelInternal() {
        }

        private void ClearCurrentModel() {
            if (Model != null) {
                Model.ResilienceModule.OnDeath -= ProcessModelDeath;
                Model.Dispose();
                Model = null;
            }
        }

        protected virtual void ProcessModelDeath(EDamageType damageType, int lethalDamageAmount) {
            InvokeDeath(damageType, lethalDamageAmount);
        }
    }
}