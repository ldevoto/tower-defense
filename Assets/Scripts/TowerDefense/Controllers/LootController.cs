using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class LootController : MonoBehaviour
    {
        [SerializeField] private Animator animator = null;
        [SerializeField] private LootSO loot = null;
        private Collider2D _collider2D = null;
        private static readonly int PickupAnim = Animator.StringToHash("Pickup");

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        private void Pickup()
        {
            animator.SetTrigger(PickupAnim);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            other.gameObject.GetComponentInParent<PlayerController>().PickUp(loot);
            Pickup();
            _collider2D.enabled = false;
            Debug.LogWarning("No se destruyó");
            // TODO: Destroy(gameObject);
        }
    }
}