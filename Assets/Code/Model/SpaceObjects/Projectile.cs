using Asteroids.Core.SpaceObjects.Modules;
using Asteroids.Core.Transforms;

namespace Asteroids.Core.SpaceObjects {

    public class Projectile : SpaceObject {

        protected override int MaxTickablesAmount => 10;

        public SingleDirectionMovementModule MovementModule { get; private set; }
        private SpaceObjectBoundsModule BoundsModule { get; set; }
        public LimitedLifetimeModule LifetimeModule { get; private set; }
        public DamageModule DamageModule { get; private set; }

        public Projectile(ObjectTransform transform, IResilienceModule resilienceModule,
            SingleDirectionMovementModule movement,
            SpaceObjectBoundsModule bounds,
            LimitedLifetimeModule lifetime,
            DamageModule damage)
            : base(transform, resilienceModule) {

            MovementModule = movement;
            RegisterModule(movement);

            BoundsModule = bounds;
            RegisterModule(bounds);

            LifetimeModule = lifetime;
            RegisterModule(lifetime);

            DamageModule = damage;
            RegisterModule(damage);
        }

        public override void Dispose() {
            base.Dispose();

            BoundsModule = null;
            MovementModule = null;
            LifetimeModule = null;
            DamageModule = null;
        }
    }
}