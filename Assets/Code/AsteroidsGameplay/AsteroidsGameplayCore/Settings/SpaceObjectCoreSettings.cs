using Asteroids.Core.SpaceObjects.Settings;
using UnityEngine;

namespace Asteroids2D.Gameplay.Settings {

    [CreateAssetMenu(fileName = "SpaceObjectCoreSettings", menuName = "Settings/SpaceObjectCoreSettings")]
    public class SpaceObjectCoreSettings : ScriptableObject, ISpaceObjectSettingsProvider {

        [SerializeField]
        private AsteroidSettings[] _asteroidSettings = new AsteroidSettings[0];
        public AsteroidSettings GetAsteroidSettings() {
            if (_asteroidSettings.Length > 0) {
                return _asteroidSettings[Random.Range(0, _asteroidSettings.Length)];
            }

            return AsteroidSettings.Default;
        }

        [SerializeField]
        private ShipSettings[] _shipSettings = new ShipSettings[0];
        public ShipSettings GetShipSettings() {
            if (_shipSettings.Length > 0) {
                return _shipSettings[Random.Range(0, _shipSettings.Length)];
            }

            return ShipSettings.Default;
        }

        [SerializeField]
        private UfoSettings[] _ufoSettings = new UfoSettings[0];
        public UfoSettings GetUfoSettings() {
            if (_ufoSettings.Length > 0) {
                return _ufoSettings[Random.Range(0, _ufoSettings.Length)];
            }

            return UfoSettings.Default;
        }

        [SerializeField]
        private ProjectileSettings _projectileSettings = ProjectileSettings.Default;
        public ProjectileSettings GetProjectileSettings() {
            return _projectileSettings;
        }
    }
}