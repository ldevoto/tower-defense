using System;
using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Controls controls = null;
        [SerializeField] private ShooterController shooterController = null;
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private Rigidbody2D playerRigidbody = null;
        [SerializeField] private float speed = 1f;
        public Action<LootSO> OnPickupLoot = null;

        private void Awake()
        {
            controls.ResetControls();
        }

        private void Start()
        {
            aliveEntityController.SetHP(100f);
        }

        public void PickUp(LootSO loot)
        {
            OnPickupLoot?.Invoke(loot);
        }

        private void Update()
        {
            controls.Update();
            if (controls.GetAction1())
            {
                shooterController.Shot();
            }
        }

        private void FixedUpdate()
        {
            //Debug.LogFormat("{0}", controls.GetMovement().ToString("F6"));
            //Debug.LogFormat("{0}", controls.GetRotation(playerRigidbody.transform).ToString("F4"));
            playerRigidbody.MovePosition(playerRigidbody.transform.position + controls.GetMovement() * speed);
            playerRigidbody.MoveRotation(controls.GetRotation(playerRigidbody.transform));
            controls.ClearValues();
        }
    }
}