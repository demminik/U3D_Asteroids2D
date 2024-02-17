namespace Asteroids.Core.SpaceObjects.Settings {

    public interface ISpaceObjectSettingsProvider {

        public AsteroidSettings GetAsteroidSettings();
        public ShipSettings GetShipSettings();
        public UfoSettings GetUfoSettings();

        // can be expanded to use different projectiled (not important for demo project)
        public ProjectileSettings GetProjectileSettings();
    }
}