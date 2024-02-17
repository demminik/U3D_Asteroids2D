using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids2D.Gameplay.Input {

    /// <summary>
    /// Player input prifider based on UnityInputSystem (New)
    /// </summary>
    public class PlayerInputProvider_UIS : MonoBehaviour, IGameplayInputProvider {

        public event Action<InputActionData> OnInputAction;

        private AsteroidsPlayerControls _unityInput;

        private void Awake() {
            _unityInput = new AsteroidsPlayerControls();

            _unityInput.PlayerShip.Accelerate.performed += OnAccelerationPerform;
            _unityInput.PlayerShip.Accelerate.canceled += OnAccelerationCancel;
            _unityInput.PlayerShip.Rotate.performed += OnRotationPerform;
            _unityInput.PlayerShip.Rotate.canceled += OnRotationCancel;
            _unityInput.PlayerShip.Weapon1.performed += OnWeapon1;
            _unityInput.PlayerShip.Weapon2.performed += OnWeapon2;

            _unityInput.PlayerShip.Enable();
        }

        private void OnDestroy() {
            _unityInput = null;
        }

        private void OnAccelerationPerform(InputAction.CallbackContext context) {
            OnInputAction?.Invoke(new InputActionData(EGameplayInputAction.Accelerate, context.ReadValue<float>()));
        }

        private void OnAccelerationCancel(InputAction.CallbackContext context) {
            OnInputAction?.Invoke(new InputActionData(EGameplayInputAction.Accelerate, 0f));
        }

        private void OnRotationPerform(InputAction.CallbackContext context) {
            OnInputAction?.Invoke(new InputActionData(EGameplayInputAction.Rotate, context.ReadValue<float>()));
        }

        private void OnRotationCancel(InputAction.CallbackContext context) {
            OnInputAction?.Invoke(new InputActionData(EGameplayInputAction.Rotate, 0f));
        }

        private void OnWeapon1(InputAction.CallbackContext context) {
            OnInputAction?.Invoke(new InputActionData(EGameplayInputAction.Weapon1, 1f));
        }

        private void OnWeapon2(InputAction.CallbackContext context) {
            OnInputAction?.Invoke(new InputActionData(EGameplayInputAction.Weapon2, 1f));
        }
    }
}