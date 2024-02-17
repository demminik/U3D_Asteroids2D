using UnityEngine;
using Utils;

namespace Asteroids.Core.Bounds {

    public class CubeBoundedObject : IBoundedObject {

        private CubeBounds _bounds;
        public CubeBounds Bounds => _bounds;

        public bool KeepPositionWithinBounds { get; private set; }

        private Vector3 _currentPosition;
        public Vector3 CurrentPosition {
            get => _currentPosition;
            set {
                _currentPosition = value;
                if (KeepPositionWithinBounds && IsPivotOffBounds) {
                    TeleportToBounds();
                }
            }
        }

        private Vector3 _boundsSize;

        public bool IsPivotOffBounds => IsPivotOffBoundsInternal(_currentPosition);

        public bool IsShapeOffBounds => IsShapeOffBoundsInternal(_currentPosition);

        public CubeBoundedObject(CubeBounds bounds, bool keepPositionWithinBounds) : this(keepPositionWithinBounds) {
            UpdateBounds(bounds);
        }

        public CubeBoundedObject(bool keepPositionWithinBounds) {
            KeepPositionWithinBounds = keepPositionWithinBounds;
        }

        public void UpdateBounds(CubeBounds bounds) {
            _bounds = bounds;

            // TODO: make sure that min IS min and max IS max (not important for test app)

            _boundsSize = new Vector3(
                Mathf.Abs(_bounds.Max.x - _bounds.Min.x),
                Mathf.Abs(_bounds.Max.y - _bounds.Min.y),
                Mathf.Abs(_bounds.Max.z - _bounds.Min.z));
        }

        protected bool IsPivotOffBoundsInternal(Vector3 position) {
            return position.x < _bounds.Min.x || position.x > _bounds.Max.x
                || position.y < _bounds.Min.y || position.y > _bounds.Max.y
                || position.z < _bounds.Min.z || position.z > _bounds.Max.z;
        }

        protected virtual bool IsShapeOffBoundsInternal(Vector3 position) {
            return IsPivotOffBoundsInternal(position);
        }

        protected void TeleportToBounds() {
            _currentPosition.x = TransformPositonToWithinBounds(_currentPosition.x, _bounds.Max.x, _boundsSize.x);
            _currentPosition.y = TransformPositonToWithinBounds(_currentPosition.y, _bounds.Max.y, _boundsSize.y);
            _currentPosition.z = TransformPositonToWithinBounds(_currentPosition.z, _bounds.Max.z, _boundsSize.z);
        }

        private float TransformPositonToWithinBounds(float position, float boundMax, float boundSize) {
            if (boundSize <= 0f) {
                return boundMax;
            }

            return boundMax - MathUtils.MathematicalMod(boundMax - position, boundSize);
        }
    }
}