using UnityEngine;

namespace TowerDefense.Controllers
{
    public class LootHolderController : MonoBehaviour
    {
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private GameObject lootPrefab = null;

        private void Start()
        {
            aliveEntityController.OnKill += DropLoot;
        }

        private void DropLoot()
        {
            Instantiate(lootPrefab, transform.position, transform.rotation);
        }
    }
}