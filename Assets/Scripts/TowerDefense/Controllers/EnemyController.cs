using System;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private ShooterController shooterController = null;
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private Rigidbody2D enemyRigidbody = null;
        [SerializeField] private float speed = 1f;

        private void Start()
        {
            aliveEntityController.SetHP(1000f);
            aliveEntityController.OnKill += Kill;
        }

        private void Kill()
        {
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            enemyRigidbody.MovePosition(enemyRigidbody.position + Vector2.right * (Time.fixedDeltaTime * speed));
        }
    }
}