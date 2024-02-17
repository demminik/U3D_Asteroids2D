using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Core.UI {

    public class AsteroidsEndgameScreen : AsteroidsScreen {

        [SerializeField]
        private TMP_Text _txtScore;

        [SerializeField]
        private Button _btnRestart;

        public event Action OnRestart;

        public void SetScore(int score) {
            if (_txtScore != null) {
                _txtScore.text = $"Game score: {score}";
            }
        }

        private void Awake() {
            BindButtons();
        }

        private void OnDestroy() {
            OnRestart = null;
        }

        private void BindButtons() {
            if (_btnRestart != null) {
                _btnRestart.onClick.AddListener(ProcessRestart);
            }
        }

        private void ProcessRestart() {
            OnRestart?.Invoke();
        }
    }
}