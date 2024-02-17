using Asteroids.Core.SpaceObjects.Modules;
using Asteroids.Core.Transforms;

namespace Asteroids.Core.SpaceObjects {

    public class Asteroid : SpaceObject {

        protected override int MaxTickablesAmount => 10;

        public SingleDirectionMovementModule MovementModule { get; private set; }
        private SpaceObjectBoundsModule BoundsModule { get; set; }

        public Asteroid(ObjectTransform transform, IResilienceModule resilienceModule,
            SingleDirectionMovementModule movement,
            SpaceObjectBoundsModule bounds)
            : base(transform, resilienceModule) {

            MovementModule = movement;
            RegisterModule(movement);

            BoundsModule = bounds;
            RegisterModule(bounds);
        }

        public override void Dispose() {
            base.Dispose();

            BoundsModule = null;
            MovementModule = null;
        }
    }
}