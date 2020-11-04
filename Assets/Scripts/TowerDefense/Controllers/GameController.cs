using System.Collections;
using TowerDefense.Controllers.AI;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private EnemyController[] enemyPrefabs = null;
        [SerializeField] private int enemiesToSpawn = 10;

        private PlayerController _playerController = null;
        private RelicController _relicController = null;
        private GraphController _graphController = null;

        private int _spawnedEnemies = 0;
        private int _killedEnemies = 0;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _relicController = FindObjectOfType<RelicController>();
            _graphController = FindObjectOfType<GraphController>();
            _relicController.OnForceFieldBroken += _graphController.UpdateCompleteGraph;
            _playerController.OnTowerPlaced += _graphController.UpdateCompleteGraph;
            _playerController.OnTowerRemoved += _graphController.UpdateCompleteGraph;
            _relicController.OnRelicTouched += PlayerLose;
            StartCoroutine(SpawnEnemies());
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
            var enemyInstance = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], new Vector3(-5, Random.Range(-5, 5.0f), 0), Quaternion.identity);
            enemyInstance.target = _playerController.GetPlayerTransform();
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