using Asteroids.Core.Tickables;
using System;
using UnityEngine;

namespace Asteroids2D.Gameplay.Spawning {

    public class TimedSpawnLogic : IDisposable, ITickable {

        private float _spawnInterval;
        private AnimationCurve _spawnIntensityCurve;
        private bool _alwaysSpawnToTheLimit;

        private float _startTime;
        private float _lastSpawnTime;

        private int _amountSpawned;
        private int _maxSpawnAmount;

        public event Action OnSpawn;

        private float CurrentTime => Time.time;

        public TimedSpawnLogic(float spawnInterval, AnimationCurve spawnIntensityCurve, bool alwaysSpawnToTheLimit) {
            _spawnInterval = spawnInterval;
            _spawnIntensityCurve = spawnIntensityCurve;
            _alwaysSpawnToTheLimit = alwaysSpawnToTheLimit;
        }

        public void Restart() {
            _startTime = CurrentTime;
            _lastSpawnTime = float.MinValue;
            _maxSpawnAmount = 0;
            _amountSpawned = 0;
        }

        public void TryToSpawn() {
            if (CurrentTime > _lastSpawnTime + _spawnInterval) {
                _maxSpawnAmount = Mathf.RoundToInt(_spawnIntensityCurve.Evaluate(CurrentTime - _startTime));
                if (_maxSpawnAmount > _amountSpawned) {
                    for (int i = _amountSpawned; i < _maxSpawnAmount; i++) {
                        OnSpawn?.Invoke();
                        _amountSpawned++;

                        if (!_alwaysSpawnToTheLimit) {
                            break;
                        }
                    }
                }
                _lastSpawnTime = CurrentTime;
            }
        }

        public void Despawn() {
            _amountSpawned = Mathf.Max(0, _amountSpawned - 1);
        }

        public void Dispose() {
            _spawnIntensityCurve = null;
            OnSpawn = null;
        }

        public void Tick(float deltaTime) {
            TryToSpawn();
        }
    }
}