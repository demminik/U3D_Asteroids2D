using Asteroids.Core.SpaceObjects.Modules;
using Asteroids.Core.Transforms;

namespace Asteroids.Core.SpaceObjects {

    public class Ship : SpaceObject {

        protected override int MaxTickablesAmount => 10;

        public InertiaMovementModule MovementModule { get; private set; }
        private SpaceObjectBoundsModule BoundsModule { get; set; }
        public SpaceObjectWeaponModule Weapon1 { get; private set; }
        public SpaceObjectWeaponModule Weapon2 { get; private set; }

        public Ship(ObjectTransform transform, IResilienceModule resilienceModule,
            InertiaMovementModule movement,
            SpaceObjectBoundsModule bounds,
            SpaceObjectWeaponModule weapon1,
            SpaceObjectWeaponModule weapon2)
            : base(transform, resilienceModule) {

            MovementModule = movement;
            RegisterModule(movement);

            BoundsModule = bounds;
            RegisterModule(bounds);

            Weapon1 = weapon1;
            RegisterModule(weapon1);

            Weapon2 = weapon2;
            RegisterModule(weapon2);
        }

        public override void Dispose() {
            base.Dispose();

            BoundsModule = null;
            MovementModule = null;
            Weapon1 = null;
            Weapon2 = null;
        }
    }
}