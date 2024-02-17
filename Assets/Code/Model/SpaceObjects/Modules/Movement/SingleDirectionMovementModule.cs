using Asteroids.Core.Parameters;
using Asteroids.Core.Transforms;
using UnityEngine;

namespace Asteroids.Core.SpaceObjects.Modules {

    public class SingleDirectionMovementModule : IMovementModule {

        private struct RuntimeParams {

            public float CurrentSpeed;
            public float MaxSpeedMultiplier;
            public float AccelerationPower;
        }

        private ObjectTransform _transform;

        private MovementParams _movementParams;
        public MovementParams MovementParams => _movementParams;
        private RuntimeParams _runtimeParams;

        // TODO: pass world forward/up direction (not important for demo)
        private Vector3 _worldForward = Vector3.forward;

        public SingleDirectionMovementModule(ObjectTransform transform, MovementParams movementParams) {
            _transform = transform;

            _movementParams = movementParams;
            _runtimeParams = new RuntimeParams() {
                MaxSpeedMultiplier = 1f,
            };
        }

        public void Dispose() {
            _transform = null;
        }

        public void Tick(float deltaTime) {
            _runtimeParams.CurrentSpeed = Mathf.MoveTowards(_runtimeParams.CurrentSpeed, _movementParams.MaxSpeed * _runtimeParams.MaxSpeedMultiplier, _movementParams.AccelerationPower * _movementParams.AccelerationPower * deltaTime);

            var movementVector = _transform.Rotation * _worldForward * _runtimeParams.CurrentSpeed * deltaTime;
            _transform.Position = _transform.Position + movementVector;
        }

        public void SetMaxSpeedMultiplier(float multiplier) {
            _runtimeParams.MaxSpeedMultiplier = multiplier;
        }

        public void SetAccelerationPower(float power) {
            _runtimeParams.AccelerationPower = power;
        }

        public void SetPitchPower(float power) {
        }

        public void SetYawPower(float power) {
        }

        public void SetRollPower(float power) {
        }

        public void SetTargetRotation(Quaternion rotation, bool immediateTransformUpdate) {
        }
    }
}