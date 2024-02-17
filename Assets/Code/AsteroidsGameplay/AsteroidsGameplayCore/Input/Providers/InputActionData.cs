namespace Asteroids2D.Gameplay.Input {

    public enum EGameplayInputAction {

        None = 0,
        Accelerate,
        Rotate,
        Weapon1,
        Weapon2,
    }

    public struct InputActionData {

        public EGameplayInputAction Action;
        public float Value;

        public InputActionData(EGameplayInputAction action, float value) {
            Action = action;
            Value = value;
        }
    }
}