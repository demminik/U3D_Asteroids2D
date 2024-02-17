using Asteroids.Core.SpaceObjects.Modules;
using Asteroids2D.Gameplay.Input;
using Asteroids2D.Gameplay.Skins;
using System;
using UnityEngine;

namespace Asteroids2D.Gameplay.Entities {

    public class Asteroid : SkinnableSpaceObject<Asteroids.Core.SpaceObjects.Asteroid, AsteroidSkin> {

        [Serializable]
        public struct BrokenPartsData {

            public float PartsSpreadDegrees;
            public int PartsAmount;
            public float PartSpeedMultiplier;
        }

        [SerializeField]
        private BrokenPartsData _postDeathPartsData;

        public bool IsAsteroidPart { get; private set; }

        protected override void OnDestroy() {
            if (Model != null) {
                Model.Transform.OnPositionChanged -= ProcessModelPositionChange;
            }

            base.OnDestroy();
        }

        protected override void InitializeModelInternal() {
            Model.Transform.OnPositionChanged += ProcessModelPositionChange;
        }

        public override void SetInputProvider(IGameplayInputProvider provider) {
        }

        private void ProcessModelPositionChange() {
            transform.position = Model.Transform.Position;
        }

        protected override void ProcessModelDeath(EDamageType damageType, int lethalDamageAmount) {
            if (damageType != EDamageType.TotalDestruction
                && _entitiesSpawner != null
                && _postDeathPartsData.PartsAmount > 0) {

                SpawnPostDeathParts();
            }

            base.ProcessModelDeath(damageType, lethalDamageAmount);
        }

        private void SpawnPostDeathParts() {
            // lazy parts creation
            var partSkin = _currentSkin != null ? _currentSkin.PartSkin : null;
            if(partSkin == null) {
                return;
            }

            var partPosition = Model.Transform.Position;
            var startPartDegreeOffset = 0f;

            var spreadDegrees = Mathf.Abs(_postDeathPartsData.PartsSpreadDegrees);
            if(spreadDegrees > 0f) {
                startPartDegreeOffset = (_postDeathPartsData.PartsAmount - 1f) / 2f * spreadDegrees;
                if(_postDeathPartsData.PartsAmount % 2f > 0f) {
                    startPartDegreeOffset += spreadDegrees * 0.5f;
                }
            }

            var startPartRotation = Model.Transform.Rotation * Quaternion.AngleAxis(startPartDegreeOffset, Vector3.up);

            for (int i = 0; i < _postDeathPartsData.PartsAmount; i++) {
                var partRotation = startPartRotation * Quaternion.AngleAxis(-spreadDegrees * i, Vector3.up);

                var partInstance = _entitiesSpawner.SpawnAsteroid(partPosition, partRotation, partSkin);
                partInstance.IsAsteroidPart = true;

                if (Model.MovementModule.MovementParams.MaxSpeed > 0
                    && partInstance.Model.MovementModule.MovementParams.MaxSpeed > 0) {

                    var targetSpeed = partInstance.Model.MovementModule.MovementParams.MaxSpeed * _postDeathPartsData.PartSpeedMultiplier;
                    partInstance.Model.MovementModule.SetMaxSpeedMultiplier(targetSpeed / partInstance.Model.MovementModule.MovementParams.MaxSpeed);
                }
            }
        }
    }
}