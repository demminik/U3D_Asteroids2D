using System;

namespace Asteroids.Core.Parameters {

    [Serializable]
    public struct WeaponParams {

        public DamageParams Damage;

        public int MaxAmmo;
        public float AmmoRestoreDuration;

        public float ShotCooldown;
    }
}