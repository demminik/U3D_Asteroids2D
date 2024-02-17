using UnityEngine;

namespace Asteroids.Core.Bounds {

    public struct CubeBounds {

        public Vector3 Min;
        public Vector3 Max;

        public CubeBounds(Vector3 min, Vector3 max) {
            Min = min;
            Max = max;
        }
    }
}