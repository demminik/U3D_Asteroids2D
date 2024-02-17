using System;

namespace Asteroids.Core.Parameters {

    [Serializable]
    public struct MovementParams {

        public float MaxSpeed;
        public float AccelerationPower;
        public float InertiaPower;
        public float RotationSpeed;
    }
}