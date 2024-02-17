using Asteroids.Core.SpaceObjects.Modules;
using Asteroids2D.Gameplay.Input;
using Asteroids2D.Gameplay.Interactions;

namespace Asteroids2D.Gameplay.Entities {

    public class Projectile : SpaceObject<Asteroids.Core.SpaceObjects.Projectile> {

        private void OnTriggerEnter(UnityEngine.Collider other) {
            if (Model != null) {
                var hitReceiver = other.gameObject.GetComponent<IHitReceiver>();
                if (hitReceiver != null) {
                    hitReceiver.Hit(Model.DamageModule.DamageParams.DamageType, Model.DamageModule.DamageParams.DamageAmount);
                }

                // destroy self
                Model.ResilienceModule.Hit(EDamageType.TotalDestruction, int.MaxValue);
            }
        }

        protected override void OnDestroy() {
            if (Model != null) {
                Model.Transform.OnPositionChanged -= ProcessModelPositionChange;
                Model.LifetimeModule.OnLifetimeFinished -= ProcessLifetimeFinished;
            }

            base.OnDestroy();
        }

        protected override void InitializeModelInternal() {
            Model.Transform.OnPositionChanged += ProcessModelPositionChange;
            Model.LifetimeModule.OnLifetimeFinished += ProcessLifetimeFinished;
        }

        public override void SetInputProvider(IGameplayInputProvider provider) {
        }

        private void ProcessModelPositionChange() {
            transform.SetPositionAndRotation(Model.Transform.Position, Model.Transform.Rotation);
        }

        private void ProcessLifetimeFinished() {
            Model.ResilienceModule.Hit(EDamageType.TotalDestruction, int.MaxValue);
        }
    }
}