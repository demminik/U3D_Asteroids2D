using System;

namespace Asteroids.Core.Parameters {

    [Serializable]
    public struct ResilienceParams {

        public int Health;
    }

    public struct ResilienceRuntimeParams {

        public int MaxHealth;
        public int CurrentHealth;

        public ResilienceRuntimeParams(ResilienceParams staticParams) {
            MaxHealth = staticParams.Health;
            CurrentHealth = staticParams.Health;
        }
    }
}