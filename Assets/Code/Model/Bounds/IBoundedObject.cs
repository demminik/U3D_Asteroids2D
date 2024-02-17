using UnityEngine;

namespace Asteroids.Core.Bounds {

    public interface IBoundedObject {

        public bool KeepPositionWithinBounds { get; }

        public bool IsPivotOffBounds { get; }
        public bool IsShapeOffBounds { get; }

        public Vector3 CurrentPosition { get; set; }
    }
}