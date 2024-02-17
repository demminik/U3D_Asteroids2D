using Asteroids.Core.SpaceObjects.Modules;
using System;

namespace Asteroids2D.Gameplay.Interactions {

    public interface IHitReceiver {

        public event Action<EDamageType, int> OnHitReceived;

        public void Hit(EDamageType damageType, int damageAmount);
    }
}