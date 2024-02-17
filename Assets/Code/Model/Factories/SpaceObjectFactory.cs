using Asteroids.Core.Bounds;
using Asteroids.Core.SpaceObjects;
using Asteroids.Core.SpaceObjects.Modules;
using Asteroids.Core.SpaceObjects.Settings;
using Asteroids.Core.Transforms;

namespace Asteroids.Core.Factories {

    /// <summary>
    /// Not and abstract factory
    /// But still encapsulates objects creation in one place 
    /// </summary>
    public static class SpaceObjectFactory {
        public static Asteroid CreateAsteroid(IBoundedObject boundedObject, ISpaceObjectSettingsProvider settingsProvider) {
            var settings = settingsProvider.GetAsteroidSettings();

            var objectTransform = new ObjectTransform();
            var boundsModule = new SpaceObjectBoundsModule(objectTransform, boundedObject);
            var movementModule = new SingleDirectionMovementModule(objectTransform, settings.Movement);

            var result = new Asteroid(objectTransform,
                new SpaceObjectDefaultResilienceModule(settings.Resilience),
                movementModule,
                boundsModule);

            return result;
        }

        public static Ship CreateShip(IBoundedObject boundedObject, ISpaceObjectSettingsProvider settingsProvider) {
            var settings = settingsProvider.GetShipSettings();

            var objectTransform = new ObjectTransform();
            var boundsModule = new SpaceObjectBoundsModule(objectTransform, boundedObject);
            var movementModule = new InertiaMovementModule(objectTransform, settings.Movement);
            var weapon1 = new SpaceObjectWeaponModule(settings.WeaponProjectile);
            var weapon2 = new SpaceObjectWeaponModule(settings.WeaponLaser);

            var result = new Ship(objectTransform,
                new SpaceObjectDefaultResilienceModule(settings.Resilience),
                movementModule,
                boundsModule,
                weapon1,
                weapon2);

            return result;
        }

        public static Ufo CreateUfo(IBoundedObject boundedObject, ISpaceObjectSettingsProvider settingsProvider) {
            var settings = settingsProvider.GetUfoSettings();

            var objectTransform = new ObjectTransform();
            var boundsModule = new SpaceObjectBoundsModule(objectTransform, boundedObject);
            var movementModule = new InertiaMovementModule(objectTransform, settings.Movement);
            var followModule = new TransformFollowModule(objectTransform, movementModule);

            var result = new Ufo(objectTransform,
                new SpaceObjectDefaultResilienceModule(settings.Resilience),
                movementModule,
                boundsModule,
                followModule);

            return result;
        }

        public static Projectile CreateProjectile(IBoundedObject boundedObject, ISpaceObjectSettingsProvider settingsProvider) {
            var settings = settingsProvider.GetProjectileSettings();

            var objectTransform = new ObjectTransform();
            var boundsModule = new SpaceObjectBoundsModule(objectTransform, boundedObject);
            var movementModule = new SingleDirectionMovementModule(objectTransform, settings.Movement);
            var lifetimeModule = new LimitedLifetimeModule(settings.Lifetime);
            var damageModule = new DamageModule(settings.Damage);

            var result = new Projectile(objectTransform,
                new SpaceObjectDefaultResilienceModule(settings.Resilience),
                movementModule,
                boundsModule,
                lifetimeModule,
                damageModule);

            return result;
        }
    }
}