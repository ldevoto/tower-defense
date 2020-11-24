using System.Collections;
using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "SO/WaveData", order = 0)]
    public class WaveData : ScriptableObject
    {
        public string waveName = null;
        public int enemiesToSpawn = 0;
        public EnemyController[] enemyPrefabs = null;
        public EnemyController bossPrefab = null;
        public float startDelay = 0;
        public float minCooldown = 0;
        public float deltaCooldown = 0;

        public IEnumerator StartWave(GameController gameController)
        {
            yield return new WaitForSeconds(startDelay);
            gameController.WaveStarted();
            for (var i = 0; i < enemiesToSpawn; i++)
            {
                gameController.SpawnEnemy(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
                yield return new WaitForSeconds(Random.Range(minCooldown, minCooldown+deltaCooldown));
            }

            gameController.SpawnEnemy(bossPrefab);
            gameController.FinishCurrentWave();
        }

        public int GetEnemiesToSpawn()
        {
            return enemiesToSpawn + 1;
        }
    }
}