using Asteroids2D.Gameplay.Skins;
using Asteroids2D.Gameplay.Spawning;

namespace Asteroids2D.Gameplay.Weapons {

    public class ProjectileWeaponBehaviour : AbstractWeaponBehaviour {

        public IProjectileWeaponSkin Skin { get; set; }
        public IGameplayEntitiesSpawner _entitiesSpawner;

        protected override void OnDestroy() {
            base.OnDestroy();

            Skin = null;
            _entitiesSpawner = null;
        }

        public void SetEntitiesSpawner(IGameplayEntitiesSpawner entitiesSpawner) {
            _entitiesSpawner = entitiesSpawner;
        }

        protected override void ExecuteWeaponShot() {
            if (_entitiesSpawner != null) {
                var projectileStartTransform = transform;
                if (Skin != null && Skin.ProjectileShotStartTransform != null) {
                    projectileStartTransform = Skin.ProjectileShotStartTransform;
                }
                _entitiesSpawner.SpawnProjectile(projectileStartTransform.position, projectileStartTransform.rotation);
            }
        }
    }
}