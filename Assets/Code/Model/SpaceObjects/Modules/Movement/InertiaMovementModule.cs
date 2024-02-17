using Asteroids.Core.Parameters;
using Asteroids.Core.Transforms;
using UnityEngine;

namespace Asteroids.Core.SpaceObjects.Modules {

    public class InertiaMovementModule : IMovementModule {

        private struct RuntimeParams {

            public float MaxSpeedMultiplier;
            public Vector3 TargetSpeed;
            public Vector3 CurrentSpeed;

            public Quaternion TargetRotation;
        }

        private ObjectTransform _transform;

        // TODO: this can be buffable, consider it (not important for demo)
        private MovementParams _movementParams;
        private RuntimeParams _runtimeParams;

        // TODO: need to pass world forward/up values in constructor for more flexibility
        private Vector3 _worldDirectionForward = Vector3.forward;

        private float _accelerationPower = 0f;
        private Vector3 _rotationPower;

        public float CurrentSpeed => _runtimeParams.CurrentSpeed.magnitude;

        public InertiaMovementModule(ObjectTransform transform, MovementParams movementParams) {
            _transform = transform;

            _movementParams = movementParams;
            _runtimeParams = new RuntimeParams() {
                TargetRotation = Quaternion.Euler(_worldDirectionForward), 
                MaxSpeedMultiplier = 1f,
            };
        }

        public void Dispose() {
            _transform = null;
        }

        public void Tick(float deltaTime) {
            TickRotation(deltaTime);
            TickPosition(deltaTime);
        }

        private void TickRotation(float deltaTime) {
            _runtimeParams.TargetRotation = _runtimeParams.TargetRotation * Quaternion.Euler(_rotationPower * _movementParams.RotationSpeed * deltaTime);

            _transform.Rotation = Quaternion.RotateTowards(_transform.Rotation, _runtimeParams.TargetRotation, _movementParams.RotationSpeed * deltaTime);
        }

        private void TickPosition(float deltaTime) {
            ApplyMovementInertia(deltaTime);
            ApplyMovementInput(deltaTime);

            _transform.Position += _runtimeParams.CurrentSpeed * deltaTime;
        }

        private void ApplyMovementInertia(float deltaTime) {
            var inertiaPower = _movementParams.InertiaPower;

            var dot = Vector3.Dot(_runtimeParams.CurrentSpeed.normalized, _runtimeParams.TargetSpeed.normalized);
            if (dot > 0f) {
                inertiaPower *= 1f - dot;
            }

            _runtimeParams.CurrentSpeed = Vector3.MoveTowards(_runtimeParams.CurrentSpeed, Vector3.zero, inertiaPower * deltaTime);
        }

        private void ApplyMovementInput(float deltaTime) {
            if (Mathf.Approximately(_accelerationPower, 0f)) {
                _runtimeParams.TargetSpeed = Vector3.zero;
                return;
            }

            var inputVector = _runtimeParams.TargetRotation * _worldDirectionForward;
            _runtimeParams.TargetSpeed = inputVector * _movementParams.MaxSpeed * _runtimeParams.MaxSpeedMultiplier;
            _runtimeParams.CurrentSpeed = Vector3.MoveTowards(_runtimeParams.CurrentSpeed, _runtimeParams.TargetSpeed, _accelerationPower * _movementParams.AccelerationPower * deltaTime);
        }

        public void SetMaxSpeedMultiplier(float multiplier) {
            _runtimeParams.MaxSpeedMultiplier = multiplier;
        }

        public void SetAccelerationPower(float power) {
            _accelerationPower = power;
        }

        public void SetPitchPower(float power) {
            _rotationPower.x = power;
        }

        public void SetYawPower(float power) {
            _rotationPower.y = power;
        }

        public void SetRollPower(float power) {
            _rotationPower.z = power;
        }

        public void SetTargetRotation(Quaternion rotation, bool immediateTransformUpdate) {
            _runtimeParams.TargetRotation = rotation;
            if(immediateTransformUpdate) {
                _transform.Rotation = _runtimeParams.TargetRotation;
            }
        }
    }
}