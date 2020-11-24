using System.Collections;
using System.Linq;
using Cinemachine;
using TowerDefense.Controllers.AI;
using TowerDefense.Controllers.Audio;
using TowerDefense.SO;
using TowerDefense.UIController;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject[] singletonsToInstantiate = null;
        [SerializeField] private LevelStateController levelStateController = null;
        [SerializeField] private LootManager lootManager = null;
        [SerializeField] private PlayerController playerControllerPrefab = null;
        [SerializeField] private Controls playerControls = null;
        [SerializeField] private Transform[] spawnPoints = null;
        [SerializeField] private float spawnCooldown = 5;
        [SerializeField] private WaveData[] waves = null;
        [SerializeField] private WaveMessageController waveMessageController = null;
        [SerializeField] private AudioClip gameMusic = null;
        [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

        private PlayerController _playerController = null;
        private RelicController _relicController = null;
        private GraphController _graphController = null;

        private int _enemiesToSpawn = 0;
        private int _killedEnemies = 0;
        private int _currentWave = 0;

        private void Awake()
        {
            InstantiateSingletons();
            lootManager.Restart();
            _relicController = FindObjectOfType<RelicController>();
            _graphController = FindObjectOfType<GraphController>();
            SpawnPlayer();
        }

        private void Start()
        {
            _relicController.OnForceFieldBroken += _graphController.UpdateCompleteGraph;
            _relicController.OnRelicTouched += PlayerLose;
            _enemiesToSpawn = CalculatesEnemiesToSpawn();
            StartWave(0);
            levelStateController.Init();
            AudioController.instance.PlayMusic(gameMusic);
        }

        private void Update()
        {
            if (playerControls.GetPauseButton())
            {
                levelStateController.Pause();
            }
        }

        private void StartWave(int wave)
        {
            _currentWave = wave;
            StartCoroutine(waves[wave].StartWave(this));
        }

        public void WaveStarted()
        {
            waveMessageController.ShowMessage(waves[_currentWave].waveName);
        }

        private void SpawnDelayedPlayer()
        {
            virtualCamera.Follow = null;
            StartCoroutine(SpawnPlayerCoroutine());
        }

        private IEnumerator SpawnPlayerCoroutine()
        {
            yield return new WaitForSeconds(spawnCooldown);
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            var spawnPoint = _relicController.GetSpawnPoint();
            _playerController = Instantiate(playerControllerPrefab, spawnPoint, Quaternion.identity);
            _playerController.OnTowerPlaced += _graphController.UpdateCompleteGraph;
            _playerController.OnTowerRemoved += _graphController.UpdateCompleteGraph;
            virtualCamera.Follow = _playerController.GetRigidbodyTransform();
            _playerController.OnKill += SpawnDelayedPlayer;
            _playerController.SetControls(playerControls);
        }

        public void SpawnEnemy(EnemyController enemyController)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var enemyInstance = Instantiate(enemyController, spawnPoint.position, spawnPoint.rotation);
            enemyInstance.SetTarget(_relicController.GetTargetTransform());
            enemyInstance.OnKill += HandleEnemyKilled;
        }

        private void HandleEnemyKilled()
        {
            _killedEnemies++;
            if (_killedEnemies >= _enemiesToSpawn)
            {
                PlayerWins();
            }
        }

        private int CalculatesEnemiesToSpawn()
        {
            return waves.Sum(wave => wave.GetEnemiesToSpawn());
        }

        public void FinishCurrentWave()
        {
            if (_currentWave + 1 < waves.Length)
            {
                StartWave(_currentWave+1);
            }
        }
        
        private void PlayerLose()
        {
            StopAllCoroutines();
            levelStateController.Lose();
        }

        private void PlayerWins()
        {
            StopAllCoroutines();
            levelStateController.Win();
        }

        private void InstantiateSingletons()
        {
            foreach (var prefab in singletonsToInstantiate)
            {
                Instantiate(prefab);
            }
        }
    }
}