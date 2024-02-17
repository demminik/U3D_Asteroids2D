using System;
using System.Collections.Generic;

namespace Asteroids2D.Gameplay.Input.Utils {

    public class InputActionsQueue : IDisposable {

        private List<InputActionData> _actions;

        public InputActionsQueue() {
            _actions = new List<InputActionData>();
        }

        public void Dispose() {
            _actions.Clear();
            _actions = null;
        }

        public void Add(InputActionData actionData) {
            _actions.Add(actionData);
        }

        public void Process(Action<InputActionData> callback) {
            if (callback != null) {
                for (int i = 0; i < _actions.Count; i++) {
                    var item = _actions[i];
                    callback(item);
                }
            }
        }

        public void Clear() {
            _actions.Clear();
        }

        public void ProcessAndClear(Action<InputActionData> callback) {
            Process(callback);
            Clear();
        }
    }
}