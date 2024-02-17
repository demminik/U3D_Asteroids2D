using Asteroids.Core.SpaceObjects.Modules;
using System;

namespace Asteroids.Core.Parameters {

    [Serializable]
    public struct DamageParams {

        public EDamageType DamageType;
        public int DamageAmount;
    }
}