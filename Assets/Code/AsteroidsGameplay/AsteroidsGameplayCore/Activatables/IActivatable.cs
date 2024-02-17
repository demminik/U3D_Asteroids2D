namespace Asteroids2D.Gameplay.Common {

    public interface IActivatable { 

        bool IsActivated { get; }

        void Activate();
        void Deactivate();
    }
}