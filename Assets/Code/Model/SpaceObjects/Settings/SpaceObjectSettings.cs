using Asteroids.Core.Parameters;
using System;

namespace Asteroids.Core.SpaceObjects.Settings {

    [Serializable]
    public struct AsteroidSettings {

        public static AsteroidSettings Default => new AsteroidSettings() {
            Resilience = new ResilienceParams() {
                Health = 1,
            },
            Movement = new MovementParams() {
                MaxSpeed = 10f,
                AccelerationPower = float.MaxValue,
                InertiaPower = 0f,
                RotationSpeed = 0f,
            },
        };

        public ResilienceParams Resilience;
        public MovementParams Movement;
    }

    [Serializable]
    public struct ShipSettings {

        public static ShipSettings Default => new ShipSettings() {
            Resilience = new ResilienceParams() {
                Health = 1,
            },
            Movement = new MovementParams() {
                MaxSpeed = 10f,
                AccelerationPower = 15f,
                InertiaPower = 10f,
                RotationSpeed = 180f,
            },
            WeaponProjectile = new WeaponParams() {
                MaxAmmo = 0,
                AmmoRestoreDuration = 0,
                ShotCooldown = 0.35f,
            },
            WeaponLaser = new WeaponParams() {
                MaxAmmo = 2,
                AmmoRestoreDuration = 3,
                ShotCooldown = 1f,
            },
        };

        public ResilienceParams Resilience;
        public MovementParams Movement;
        public WeaponParams WeaponProjectile;
        public WeaponParams WeaponLaser;
    }

    [Serializable]
    public struct UfoSettings {

        public static UfoSettings Default => new UfoSettings() {
            Resilience = new ResilienceParams() {
                Health = 1,
            },
            Movement = new MovementParams() {
                MaxSpeed = 10f,
                AccelerationPower = 15f,
                InertiaPower = 10f,
                RotationSpeed = 180f,
            },
        };

        public ResilienceParams Resilience;
        public MovementParams Movement;
    }

    [Serializable]
    public struct ProjectileSettings {

        public static ProjectileSettings Default => new ProjectileSettings() {
            Resilience = new ResilienceParams() {
                Health = 1,
            },
            Movement = new MovementParams() {
                MaxSpeed = 10f,
                AccelerationPower = float.MaxValue,
                InertiaPower = 0f,
                RotationSpeed = 0f,
            },
            Damage = new DamageParams() {
                DamageType = Modules.EDamageType.Default,
                DamageAmount = 1,
            },
            Lifetime = 1f,
        };

        public ResilienceParams Resilience;
        public MovementParams Movement;
        public DamageParams Damage;
        public float Lifetime;
    }
}