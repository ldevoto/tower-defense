﻿using System.Collections;
using TowerDefense.Controllers.AI;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerController playerControllerPrefab = null;
        [SerializeField] private EnemyController[] enemyPrefabs = null;
        [SerializeField] private Transform[] spawnPoints = null;
        [SerializeField] private int enemiesToSpawn = 10;
        [SerializeField] private float spawnCooldown = 5;

        private PlayerController _playerController = null;
        private RelicController _relicController = null;
        private GraphController _graphController = null;

        private int _spawnedEnemies = 0;
        private int _killedEnemies = 0;

        private void Awake()
        {
            _relicController = FindObjectOfType<RelicController>();
            _graphController = FindObjectOfType<GraphController>();
            SpawnPlayer();
        }

        private void Start()
        {
            _relicController.OnForceFieldBroken += _graphController.UpdateCompleteGraph;
            _relicController.OnRelicTouched += PlayerLose;
            StartCoroutine(SpawnEnemies());
        }

        private void SpawnDelayedPlayer()
        {
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
            _playerController.OnKill += SpawnDelayedPlayer;
        }

        private IEnumerator SpawnEnemies()
        {
            while (_spawnedEnemies < enemiesToSpawn)
            {
                yield return new WaitForSeconds(Random.Range(2, 5));
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            _spawnedEnemies++;
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var enemyInstance = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
            enemyInstance.target = _relicController.transform;
            enemyInstance.OnKill += HandleEnemyKilled;
        }

        private void HandleEnemyKilled()
        {
            _killedEnemies++;
            if (_killedEnemies >= enemiesToSpawn)
            {
                PlayerWins();
            }
        }
        
        private void PlayerLose()
        {
            StopAllCoroutines();
            Debug.Log("Perdiste");
            Time.timeScale = 0f;
        }

        private void PlayerWins()
        {
            StopAllCoroutines();
            Debug.Log("Ganaste!");
        }
    }
}