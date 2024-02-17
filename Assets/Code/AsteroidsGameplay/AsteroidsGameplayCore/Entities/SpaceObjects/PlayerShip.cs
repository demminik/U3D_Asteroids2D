using Asteroids.Core.SpaceObjects.Modules;
using Asteroids2D.Gameplay.Input;
using Asteroids2D.Gameplay.Skins;
using Asteroids2D.Gameplay.Spawning;
using Asteroids2D.Gameplay.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids2D.Gameplay.Entities {

    public class PlayerShip : SkinnableSpaceObject<Asteroids.Core.SpaceObjects.Ship, ShipSkin> {

        [SerializeField]
        private ProjectileWeaponBehaviour _projectileWeapon;

        [SerializeField]
        private LaserWeaponBehaviour _laserWeapon;

        private Dictionary<EGameplayInputAction, Action<InputActionData>> _actionProcessors = new Dictionary<EGameplayInputAction, Action<InputActionData>>();

        protected override void OnDestroy() {
            _actionProcessors.Clear();

            if(Model != null) {
                Model.Transform.OnPositionChanged -= ProcessModelPositionChange;
                Model.Transform.OnRotationChanged -= ProcessModelRotationChange;
            }

            base.OnDestroy();
        }

        public override void SetEntitiesSpawner(IGameplayEntitiesSpawner entitiesSpawner) {
            base.SetEntitiesSpawner(entitiesSpawner);

            if(_entitiesSpawner != null) {
                if(_projectileWeapon != null) {
                    _projectileWeapon.SetEntitiesSpawner(_entitiesSpawner);
                }
            }
        }

        protected override void InitializeModelInternal() {
            base.InitializeModelInternal();

            _actionProcessors.Add(EGameplayInputAction.Accelerate, ProcessAction_Accelerate);
            _actionProcessors.Add(EGameplayInputAction.Rotate, ProcessAction_Rotate);
            _actionProcessors.Add(EGameplayInputAction.Weapon1, ProcessAction_Weapon1);
            _actionProcessors.Add(EGameplayInputAction.Weapon2, ProcessAction_Weapon2);

            Model.Transform.OnPositionChanged += ProcessModelPositionChange;
            Model.Transform.OnRotationChanged += ProcessModelRotationChange;

            if(_projectileWeapon != null) {
                _projectileWeapon.WeaponModule = Model.Weapon1;
            }
            if(_laserWeapon != null) {
                _laserWeapon.WeaponModule = Model.Weapon2;
            }
        }

        private void ProcessModelPositionChange() {
            transform.position = Model.Transform.Position;
        }

        private void ProcessModelRotationChange() {
            transform.rotation = Model.Transform.Rotation;
        }

        protected override void SkinPreCleanup(ShipSkin skinInstance) {
            if (_projectileWeapon != null) {
                _projectileWeapon.Skin = null;
            }
            if (_laserWeapon != null) {
                _laserWeapon.Skin = null;
            }
        }

        protected override void ProcessSkinApplied(ShipSkin skinInstance) {
            if (_projectileWeapon != null) {
                _projectileWeapon.Skin = skinInstance;
            }
            if (_laserWeapon != null) {
                _laserWeapon.Skin = skinInstance;
            }
        }

        protected override void ProcessSkinCollision(Collider collider) {
            // die if hit something
            Model.ResilienceModule.Hit(EDamageType.TotalDestruction, int.MaxValue);
        }

        protected override void ExecuteInputAction(InputActionData actionData) {
            if (_actionProcessors.TryGetValue(actionData.Action, out var actionProcessor)) {
                actionProcessor(actionData);
            }
        }

        private void ProcessAction_Accelerate(InputActionData actionData) {
            if (Model != null) {
                Model.MovementModule.SetAccelerationPower(actionData.Value);
            }
            if (_currentSkin != null) {
                _currentSkin.SetExhauseEnabled(actionData.Value);
            }
        }

        private void ProcessAction_Rotate(InputActionData actionData) {
            if (Model != null) {
                Model.MovementModule.SetYawPower(actionData.Value);
            }
        }

        private void ProcessAction_Weapon1(InputActionData actionData) {
            if (Model != null) {
                Model.Weapon1.Shoot();
            }
        }

        private void ProcessAction_Weapon2(InputActionData actionData) {
            if (Model != null) {
                Model.Weapon2.Shoot();
            }
        }
    }
}