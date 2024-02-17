using Asteroids.Core.Bounds;
using Asteroids.Core.Tickables;
using Asteroids.Core.Transforms;

namespace Asteroids.Core.SpaceObjects.Modules {

    public class SpaceObjectBoundsModule : ISpaceObjectModule, ITickable {

        private ObjectTransform _transform;
        private IBoundedObject _boundedObject;

        public SpaceObjectBoundsModule(ObjectTransform transform, IBoundedObject boundedObject) {
            _transform = transform;
            _boundedObject = boundedObject;
        }

        public void Dispose() {
            _transform = null;
        }

        public void Tick(float deltaTime) {
            _boundedObject.CurrentPosition = _transform.Position;
            _transform.Position = _boundedObject.CurrentPosition;
        }
    }
}