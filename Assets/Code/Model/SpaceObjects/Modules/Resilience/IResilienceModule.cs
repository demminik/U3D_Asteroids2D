using System;

namespace Asteroids.Core.SpaceObjects.Modules {

    public interface IResilienceModule : ISpaceObjectModule {

        public void Hit(EDamageType damageType, int damageAmount);

        public event Action<EDamageType, int> OnHit;
        public event Action<EDamageType, int> OnDeath;
    }
}