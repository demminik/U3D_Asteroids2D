using Asteroids2D.Gameplay.Entities;
using Asteroids2D.Gameplay.Skins;
using UnityEngine;

namespace Asteroids2D.Gameplay.Settings {

    [CreateAssetMenu(fileName = "SpaceObjectSettings", menuName = "Settings/SpaceObjectSettings")]
    public class SpaceObjectSettings : ScriptableObject {

        [SerializeField]
        private SpaceObjectCoreSettings _coreSettings;
        public SpaceObjectCoreSettings CoreSettings => _coreSettings;

        [SerializeField]
        private Asteroid _asteroidPrefab;
        public Asteroid AsteroidPrefab => _asteroidPrefab;

        [SerializeField]
        private AsteroidSkin[] _asteroidSkins = new AsteroidSkin[0];
        public AsteroidSkin GetRandomAsteroidSkin() {
            if(_asteroidSkins.Length == 0) {
                return null;
            }

            return _asteroidSkins[Random.Range(0, _asteroidSkins.Length)];
        }

        [SerializeField]
        private PlayerShip _playerShipPrefab;
        public PlayerShip PlayerShipPrefab => _playerShipPrefab;
        [SerializeField]
        private ShipSkin[] _shipSkins = new ShipSkin[0];
        public ShipSkin GetRandomShipSkin() {
            if (_shipSkins.Length == 0) {
                return null;
            }

            return _shipSkins[Random.Range(0, _shipSkins.Length)];
        }

        [SerializeField]
        private Ufo _ufoPrefab;
        public Ufo UfoPrefab => _ufoPrefab;
        [SerializeField]
        private SpaceObjectSkin[] _ufoSkins = new SpaceObjectSkin[0];
        public SpaceObjectSkin GetRandomUfoSkin() {
            if (_ufoSkins.Length == 0) {
                return null;
            }

            return _ufoSkins[Random.Range(0, _ufoSkins.Length)];
        }

        [SerializeField]
        private Projectile _projectilePrefab;
        public Projectile ProjectilePrefab => _projectilePrefab;
    }
}