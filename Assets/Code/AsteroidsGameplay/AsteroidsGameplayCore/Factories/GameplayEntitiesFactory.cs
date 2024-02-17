using Asteroids.Core.Bounds;
using Asteroids.Core.Factories;
using Asteroids2D.Gameplay.Input;
using Asteroids2D.Gameplay.Settings;
using Asteroids2D.Gameplay.Skins;
using UnityEngine;

namespace Asteroids2D.Gameplay.Spawning {

    /// <summary>
    /// Not and abstract factory
    /// But still encapsulates objects creation in one place 
    /// </summary>
    public static class GameplayEntitiesFactory {

        public static Entities.Asteroid CreateAsteroid(SpaceObjectSettings spaceObjectSettings, CubeBoundedObject worldBoundedObject, AsteroidSkin skinPrefab) {
            var asteroid = SpaceObjectFactory.CreateAsteroid(worldBoundedObject, spaceObjectSettings.CoreSettings);

            var instance = GameObject.Instantiate(spaceObjectSettings.AsteroidPrefab);
            instance.InitializeModel(asteroid);

            if (skinPrefab != null) {
                var skinInstance = GameObject.Instantiate(skinPrefab);
                instance.ApplySkin(skinInstance);
            }

            return instance;
        }

        public static Entities.PlayerShip CreatePlayerShip(SpaceObjectSettings spaceObjectSettings, CubeBoundedObject worldBoundedObject, ShipSkin skinPrefab, IGameplayInputProvider playerInputProvider) {
            var ship = SpaceObjectFactory.CreateShip(worldBoundedObject, spaceObjectSettings.CoreSettings);

            var instance = GameObject.Instantiate(spaceObjectSettings.PlayerShipPrefab);
            instance.InitializeModel(ship);
            instance.SetInputProvider(playerInputProvider);

            if (skinPrefab != null) {
                var skinInstance = GameObject.Instantiate(skinPrefab);
                instance.ApplySkin(skinInstance);
            }

            return instance;
        }

        public static Entities.Ufo CreateUfo(SpaceObjectSettings spaceObjectSettings, CubeBoundedObject worldBoundedObject, SpaceObjectSkin skinPrefab) {
            var ufo = SpaceObjectFactory.CreateUfo(worldBoundedObject, spaceObjectSettings.CoreSettings);

            var instance = GameObject.Instantiate(spaceObjectSettings.UfoPrefab);
            instance.InitializeModel(ufo);

            if (skinPrefab != null) {
                var skinInstance = GameObject.Instantiate(skinPrefab);
                instance.ApplySkin(skinInstance);
            }

            return instance;
        }

        public static Entities.Projectile CreateProjectile(SpaceObjectSettings spaceObjectSettings, CubeBoundedObject worldBoundedObject) {
            var projectile = SpaceObjectFactory.CreateProjectile(worldBoundedObject, spaceObjectSettings.CoreSettings);

            var instance = GameObject.Instantiate(spaceObjectSettings.ProjectilePrefab);
            instance.InitializeModel(projectile);

            return instance;
        }
    }
}