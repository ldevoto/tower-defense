using System.Collections;
using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class LootController : MonoBehaviour
    {
        [SerializeField] private Animator animator = null;
        
        private static readonly int PickupAnim = Animator.StringToHash("Pickup");
        private static readonly int SpawnParam = Animator.StringToHash("Spawn");
        
        private LootData _loot = null;
        private bool _hasBeenCollected = false;

        public void SpawnWith(LootData lootData, Transform spawnTransform)
        {
            SetLoot(lootData);
            _hasBeenCollected = false;
            var transform1 = transform;
            transform1.position = spawnTransform.position;
            transform1.rotation = spawnTransform.rotation;
            animator.SetTrigger(SpawnParam);
        }

        private void SetLoot(LootData lootData)
        {
            _loot = lootData;
        }

        private void Pickup()
        {
            animator.SetTrigger(PickupAnim);
            StartCoroutine(DisableCoroutine());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_hasBeenCollected) return;
            if (!other.gameObject.CompareTag("Player")) return;
            
            other.gameObject.GetComponentInParent<PlayerController>().PickUp(_loot);
            _hasBeenCollected = true;
            Pickup();
        }
        
        private IEnumerator DisableCoroutine()
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            gameObject.SetActive(false);
        }
    }
}