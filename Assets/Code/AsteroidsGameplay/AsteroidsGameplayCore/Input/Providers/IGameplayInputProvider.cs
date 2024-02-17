using System;

namespace Asteroids2D.Gameplay.Input {

    /// <summary>
    /// Very simple player input interface just provide unified actions logic, not tied to chosen input system
    /// /// </summary>
    public interface IGameplayInputProvider {

        public event Action<InputActionData> OnInputAction;
    }
}