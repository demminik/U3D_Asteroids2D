using Asteroids.Core.Tickables;
using Asteroids.Core.Transforms;
using UnityEngine;

namespace Asteroids.Core.SpaceObjects.Modules {

    public class TransformFollowModule : ISpaceObjectModule, ITickable {

        private ObjectTransform _transform;
        private IMovementModule _movementModule;

        public ObjectTransform TargetTransform { get; set; }

        public TransformFollowModule(ObjectTransform transform, IMovementModule movementModule) {
            _transform = transform;
            _movementModule = movementModule;
        }

        public void Dispose() {
            _transform = null;
            _movementModule = null;
            TargetTransform = null;
        }

        public void Tick(float deltaTime) {
            // TODO: add support of bounds to calculate better follow path (not important for demo)

            if(_movementModule != null) {
                if (TargetTransform != null) {
                    _movementModule.SetAccelerationPower(1f);
                    _movementModule.SetTargetRotation(Quaternion.LookRotation(TargetTransform.Position - _transform.Position), false);
                } else {
                    _movementModule.SetAccelerationPower(0f);
                }
            }
        }
    }
}