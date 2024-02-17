using Asteroids.Core.Tickables;
using UnityEngine;

namespace Asteroids.Core.SpaceObjects.Modules {

    public interface IMovementModule : ISpaceObjectModule, ITickable {

        public const float ACCELERATION_POWER_DEFAULT = 1f;

        public void SetMaxSpeedMultiplier(float multiplier);
        public void SetAccelerationPower(float power);

        public void SetPitchPower(float power);
        public void SetYawPower(float power);
        public void SetRollPower(float power);

        public void SetTargetRotation(Quaternion rotation, bool immediateTransformUpdate);
    }
}