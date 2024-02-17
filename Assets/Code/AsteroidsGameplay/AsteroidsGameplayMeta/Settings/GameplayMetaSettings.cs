using System;
using UnityEngine;

namespace Asteroids2D.Gameplay.Meta.Settings {

    [CreateAssetMenu(fileName = "GameplayMetaSettings", menuName = "Settings/GameplayMetaSettings")]
    public class GameplayMetaSettings : ScriptableObject {

        [Serializable]
        public struct ScoresData {

            public int AsteroidScore;
            public int AsteroidPartScore;
            public int UfoScore;
        }

        [SerializeField]
        private ScoresData _scores;
        public ScoresData Scores => _scores;
    }
}