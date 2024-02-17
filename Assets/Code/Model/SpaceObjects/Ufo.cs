using Asteroids.Core.SpaceObjects.Modules;
using Asteroids.Core.Transforms;

namespace Asteroids.Core.SpaceObjects {

    public class Ufo : SpaceObject {

        protected override int MaxTickablesAmount => 10;

        public InertiaMovementModule MovementModule { get; private set; }
        private SpaceObjectBoundsModule BoundsModule { get; set; }
        public TransformFollowModule TransformFollowModule { get; private set; }

        public Ufo(ObjectTransform transform, IResilienceModule resilienceModule,
            InertiaMovementModule movement,
            SpaceObjectBoundsModule bounds,
            TransformFollowModule followModule)
            : base(transform, resilienceModule) {

            MovementModule = movement;
            RegisterModule(movement);

            BoundsModule = bounds;
            RegisterModule(bounds);

            TransformFollowModule = followModule;
            RegisterModule(followModule);
        }

        public override void Dispose() {
            base.Dispose();

            BoundsModule = null;
            MovementModule = null;
        }
    }
}