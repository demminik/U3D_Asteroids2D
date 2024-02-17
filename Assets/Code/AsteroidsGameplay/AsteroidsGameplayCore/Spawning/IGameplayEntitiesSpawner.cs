using Asteroids2D.Gameplay.Entities;
using Asteroids2D.Gameplay.Skins;
using UnityEngine;

namespace Asteroids2D.Gameplay.Spawning {

    public interface IGameplayEntitiesSpawner {

        public PlayerShip SpawnPlayerShip();
        public Asteroid SpawnAsteroid(Vector3 position, Quaternion rotation, AsteroidSkin skinPrefab);
        public Ufo SpawnUfo();
        public Projectile SpawnProjectile(Vector3 position, Quaternion rotation);
    }
}