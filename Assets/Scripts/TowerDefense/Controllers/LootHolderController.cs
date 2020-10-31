using System;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class LootHolderController : MonoBehaviour
    {
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private GameObject lootPrefab = null;

        private void OnEnable()
        {
            aliveEntityController.OnKill += DropLoot;
        }

        private void OnDisable()
        {
            if (aliveEntityController.OnKill != null)
            {
                aliveEntityController.OnKill -= DropLoot;
            }
        }

        private void DropLoot()
        {
            Instantiate(lootPrefab, transform.position, transform.rotation);
        }
    }
}