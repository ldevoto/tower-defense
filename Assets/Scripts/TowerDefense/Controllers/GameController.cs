using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefense.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private EnemyController[] enemyPrefabs = null;

        private PlayerController _playerController = null;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(2, 5));
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var enemyInstance = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], new Vector3(-5, Random.Range(-5, 5.0f), 0), Quaternion.identity);
            enemyInstance.target = _playerController.GetPlayerTransform();
        }
    }
}