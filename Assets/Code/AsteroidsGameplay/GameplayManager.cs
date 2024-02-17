using Asteroids.Core.Bounds;
using Asteroids.Core.Meta;
using Asteroids.Core.SpaceObjects.Modules;
using Asteroids.Core.Tickables;
using Asteroids.Core.UI;
using Asteroids2D.Gameplay.Entities;
using Asteroids2D.Gameplay.Input;
using Asteroids2D.Gameplay.Meta.Settings;
using Asteroids2D.Gameplay.Settings;
using Asteroids2D.Gameplay.Skins;
using Asteroids2D.Gameplay.Spawning;
using UnityEngine;

namespace Asteroids2D.Gameplay {

    public class GameplayManager : MonoBehaviour, IScoreProvider, IGameplayEntitiesSpawner {

        [SerializeField]
        private SpaceObjectSettings _spaceObjectSettings;

        [SerializeField]
        private GameplayMetaSettings _gameplayMetaSettings;

        [SerializeField]
        private PlayerInputProvider_UIS _playerInputProvider;

        [SerializeField]
        private Camera _mainCamera;

        [SerializeField]
        private SpawnParameters _asteroidSpawnParams;

        [SerializeField]
        private SpawnParameters _ufoSpawnParams;

        [SerializeField]
        private AsteroidsHUD _hud;

        [SerializeField]
        private AsteroidsEndgameScreen _endgameScreen;

        private TimedSpawnLogic _asteroidSpawning;
        private TimedSpawnLogic _ufoSpawning;

        private CubeBoundedObject _worldBoundedObject;
        private PlayerShip _playerShip;

        private TickableSet _tickableSet;

        public int Score { get; private set; }

        private bool _isGameRunning = false;

        private void Awake() {
            _worldBoundedObject = new CubeBoundedObject(GetWorldBounds(), true);
            _endgameScreen.OnRestart += RestartGame;

            RunGame();
        }

        private void Update() {
            if (_isGameRunning) {
                _tickableSet.Tick(Time.deltaTime);
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow)) {
                EndGame();
            }
        }

        private void OnDestroy() {
            ClearInternalStuff();
        }

        private void RunGame() {
            if (_isGameRunning) {
                Debug.LogError($"The game is already running!");
                return;
            }

            _tickableSet = new TickableSet(5);

            _playerShip = SpawnPlayerShip();

            _asteroidSpawning = new TimedSpawnLogic(_asteroidSpawnParams.SpawnInterval, _asteroidSpawnParams.SpawnIntensityCurve, _asteroidSpawnParams.SpawnToTheLimit);
            _asteroidSpawning.OnSpawn += () => { SpawnAsteroid(); };
            _asteroidSpawning.Restart();
            _tickableSet.Add(_asteroidSpawning);

            _ufoSpawning = new TimedSpawnLogic(_ufoSpawnParams.SpawnInterval, _ufoSpawnParams.SpawnIntensityCurve, _ufoSpawnParams.SpawnToTheLimit);
            _ufoSpawning.OnSpawn += () => { SpawnUfo(); };
            _ufoSpawning.Restart();
            _tickableSet.Add(_ufoSpawning);

            if (_hud != null) {
                _hud.SetTarget(_playerShip);
                _hud.SetScoreProvider(this);
                _hud.Show();
            }
            if (_endgameScreen != null) {
                _endgameScreen.Hide();
            }

            Score = 0;

            _isGameRunning = true;
        }

        private void EndGame() {
            if (_isGameRunning) {
                ClearInternalStuff();
                ClearEntities();

                if (_hud != null) {
                    _hud.Hide();
                }
                if (_endgameScreen != null) {
                    _endgameScreen.SetScore(Score);
                    _endgameScreen.Show();
                }

                _isGameRunning = false;
            }
        }

        private void RestartGame() {
            EndGame();
            RunGame();
        }

        private void ClearInternalStuff() {
            if (_tickableSet != null) {
                _tickableSet.Dispose();
                _tickableSet = null;
            }

            if (_asteroidSpawning != null) {
                _asteroidSpawning.Dispose();
                _asteroidSpawning = null;
            }

            if (_ufoSpawning != null) {
                _ufoSpawning.Dispose();
                _ufoSpawning = null;
            }

            if (_hud != null) {
                _hud.SetTarget(null);
                _hud.SetScoreProvider(null);
            }
        }

        private void ClearEntities() {
            // very lazy
            var createdEntities = FindObjectsOfType<SpaceObjectBase>();
            foreach (var entity in createdEntities) {
                GameObject.Destroy(entity.gameObject);
            }
        }

        private CubeBounds GetWorldBounds() {
            if (_mainCamera == null) {
                Debug.LogError($"Failed to retrieve valid screen bounds: {nameof(_mainCamera)} is not assigned");

                return new CubeBounds() {
                    Min = Vector3.one * -1,
                    Max = Vector3.one,
                };
            }

            var boundMin = _mainCamera.ScreenToWorldPoint(Vector3.zero);
            boundMin.y = 0f;
            var boundMax = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
            boundMax.y = 0f;

            return new CubeBounds() {
                Min = boundMin,
                Max = boundMax,
            };
        }

        public PlayerShip SpawnPlayerShip() {
            var instance = GameplayEntitiesFactory.CreatePlayerShip(_spaceObjectSettings, _worldBoundedObject, _spaceObjectSettings.GetRandomShipSkin(), _playerInputProvider);
            instance.SetEntitiesSpawner(this);

            instance.Model.Transform.Position = Vector3.zero;
            instance.Model.MovementModule.SetTargetRotation(Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up), true);

            instance.Activate();
            instance.OnDeath += ProcessPlayerShipDeath;

            return instance;
        }

        private void ProcessPlayerShipDeath(GameObject instance, EDamageType damageType, int lethalDamageAmount) {
            var deadInstance = instance.GetComponent<PlayerShip>();
            deadInstance.OnDeath -= ProcessPlayerShipDeath;

            EndGame();
        }

        private Asteroid SpawnAsteroid() {
            var spawnPosition = new Vector3(Random.Range(_worldBoundedObject.Bounds.Min.x, _worldBoundedObject.Bounds.Max.x),
                0f,
                Random.Range(_worldBoundedObject.Bounds.Min.z, _worldBoundedObject.Bounds.Max.z));
            var spawnRotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
            var skin = _spaceObjectSettings.GetRandomAsteroidSkin();

            return SpawnAsteroid(spawnPosition, spawnRotation, skin);
        }

        public Asteroid SpawnAsteroid(Vector3 position, Quaternion rotation, AsteroidSkin skinPrefab) {
            var instance = GameplayEntitiesFactory.CreateAsteroid(_spaceObjectSettings, _worldBoundedObject, skinPrefab);
            instance.SetEntitiesSpawner(this);

            instance.Model.Transform.Position = position;
            instance.Model.Transform.Rotation = rotation;

            instance.Activate();
            instance.OnDeath += ProcessAsteroidDeath;

            return instance;
        }

        private void ProcessAsteroidDeath(GameObject instance, EDamageType damageType, int lethalDamageAmount) {
            var deadInstance = instance.GetComponent<Asteroid>();
            deadInstance.OnDeath -= ProcessAsteroidDeath;
            Score += deadInstance.IsAsteroidPart ? _gameplayMetaSettings.Scores.AsteroidPartScore : _gameplayMetaSettings.Scores.AsteroidScore;

            // TODO: use objects pooling (not important for demo)
            GameObject.Destroy(instance);

            if (!deadInstance.IsAsteroidPart) {
                _asteroidSpawning.Despawn();
            }
        }

        public Ufo SpawnUfo() {
            var instance = GameplayEntitiesFactory.CreateUfo(_spaceObjectSettings, _worldBoundedObject, _spaceObjectSettings.GetRandomUfoSkin());
            instance.SetEntitiesSpawner(this);

            instance.Model.Transform.Position = new Vector3(Random.Range(_worldBoundedObject.Bounds.Min.x, _worldBoundedObject.Bounds.Max.x),
                0f,
                Random.Range(_worldBoundedObject.Bounds.Min.z, _worldBoundedObject.Bounds.Max.z));
            instance.Model.MovementModule.SetTargetRotation(Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up), true);
            instance.Model.MovementModule.SetAccelerationPower(IMovementModule.ACCELERATION_POWER_DEFAULT);

            instance.Model.TransformFollowModule.TargetTransform = _playerShip.Model.Transform;

            instance.Activate();
            instance.OnDeath += ProcessUfoDeath;

            return instance;
        }

        private void ProcessUfoDeath(GameObject instance, EDamageType damageType, int lethalDamageAmount) {
            var deadInstance = instance.GetComponent<Ufo>();
            deadInstance.OnDeath -= ProcessUfoDeath;
            Score += _gameplayMetaSettings.Scores.UfoScore;

            // TODO: use objects pooling (not important for demo)
            GameObject.Destroy(instance);

            _ufoSpawning.Despawn();
        }

        public Projectile SpawnProjectile(Vector3 position, Quaternion rotation) {
            var instance = GameplayEntitiesFactory.CreateProjectile(_spaceObjectSettings, _worldBoundedObject);
            instance.SetEntitiesSpawner(this);

            instance.Model.Transform.Position = position;
            instance.Model.Transform.Rotation = rotation;
            instance.Model.MovementModule.SetAccelerationPower(IMovementModule.ACCELERATION_POWER_DEFAULT);

            instance.Activate();
            instance.OnDeath += ProcessProjectileDeath;

            return instance;
        }

        private void ProcessProjectileDeath(GameObject instance, EDamageType damageType, int lethalDamageAmount) {
            var deadInstance = instance.GetComponent<Projectile>();
            deadInstance.OnDeath -= ProcessProjectileDeath;

            // TODO: use objects pooling (not important for demo)
            GameObject.Destroy(instance);
        }
    }
}