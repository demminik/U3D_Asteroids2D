using Asteroids.Core.Tickables;
using System;

namespace Asteroids.Core.SpaceObjects.Modules {

    public class LimitedLifetimeModule : ISpaceObjectModule, ITickable {

        private float _lifetimeCurrent;

        public event Action OnLifetimeFinished;

        public LimitedLifetimeModule(float lifetime) {
            _lifetimeCurrent = lifetime;
        }

        public void Dispose() {
        }

        public void Tick(float deltaTime) {
            if (_lifetimeCurrent > 0f) {
                _lifetimeCurrent -= deltaTime;
                if (_lifetimeCurrent <= 0) {
                    _lifetimeCurrent = 0f;
                    OnLifetimeFinished?.Invoke();
                }
            }
        }
    }
}