using UnityEngine;

namespace TowerDefense.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private ShooterController shooterController = null;
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private Rigidbody2D playerRigidbody = null;
        [SerializeField] private float speed = 1f;

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