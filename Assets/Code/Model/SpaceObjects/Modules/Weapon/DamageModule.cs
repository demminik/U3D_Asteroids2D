using Asteroids.Core.Parameters;

namespace Asteroids.Core.SpaceObjects.Modules {

    public class DamageModule : ISpaceObjectModule {

        public DamageParams DamageParams { get; private set; }

        public DamageModule(DamageParams damageParams) {
            DamageParams = damageParams;
        }

        public void Dispose() {
        }
    }
}