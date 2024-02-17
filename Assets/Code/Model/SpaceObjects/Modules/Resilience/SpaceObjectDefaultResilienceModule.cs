using Asteroids.Core.Parameters;
using System;

namespace Asteroids.Core.SpaceObjects.Modules {

    public class SpaceObjectDefaultResilienceModule : IResilienceModule {

        private ResilienceRuntimeParams _currentParams;
        public ResilienceRuntimeParams Params => _currentParams;

        private ResilienceParams _startParams;

        public bool IsAlive => _currentParams.CurrentHealth > 0;

        public event Action<EDamageType, int> OnHit;
        public event Action<EDamageType, int> OnDeath;

        public SpaceObjectDefaultResilienceModule(ResilienceParams paramsData) {
            _startParams = paramsData;
            ResetState();
        }

        public void ResetState() {
            _currentParams = new ResilienceRuntimeParams(_startParams);
        }

        public void Hit(EDamageType damageType, int damageAmount) {
            if (!IsAlive || damageType == EDamageType.None) {
                return;
            }

            _currentParams.CurrentHealth -= damageAmount;
            OnHit?.Invoke(damageType, damageAmount);
            if (!IsAlive) {
                _currentParams.CurrentHealth = 0;
                OnDeath?.Invoke(damageType, damageAmount);
            }
        }

        public void Dispose() {
            OnHit = null;
            OnDeath = null;
        }
    }
}