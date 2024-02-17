using System;
using UnityEngine;

namespace Asteroids.Core.Transforms {

    public class ObjectTransform : IDisposable {

        protected Vector3 _position;
        public Vector3 Position {
            get => _position;
            set {
                _position = value;
                OnPositionChanged?.Invoke();
            }
        }
        public event Action OnPositionChanged;

        protected Quaternion _rotation;
        public Quaternion Rotation {
            get => _rotation;
            set {
                _rotation = value;
                OnRotationChanged?.Invoke();
            }
        }
        public event Action OnRotationChanged;

        public void Dispose() {
            OnPositionChanged = null;
            OnRotationChanged = null;
        }
    }
}