using UnityEngine;

namespace TowerDefense.Controllers
{
    public class WallController : MonoBehaviour
    {
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private Rigidbody2D wallRigidbody = null;

        private void Start()
        {
            aliveEntityController.SetHP(100f);
            aliveEntityController.OnKill += Kill;
        }

        private void Kill()
        {
            Destroy(gameObject);
        }
    }
}