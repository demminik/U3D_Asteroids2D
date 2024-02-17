using System;
using UnityEngine;

namespace Asteroids2D.Gameplay.Spawning {

    [Serializable]
    public struct SpawnParameters {

        public float SpawnInterval;
        public AnimationCurve SpawnIntensityCurve;
        public bool SpawnToTheLimit;
    }
}
