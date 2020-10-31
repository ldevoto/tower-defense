using System;
using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Controls controls = null;
        [SerializeField] private Rigidbody2D playerRigidbody = null;
        [SerializeField] private float speed = 1f;
        public Action<LootSO> OnPickupLoot = null;

        private void Awake()
        {
            controls.ResetControls();
        }

        public void PickUp(LootSO loot)
        {
            OnPickupLoot?.Invoke(loot);
        }

        private void Update()
        {
            controls.Update();
        }

        private void FixedUpdate()
        {
            Debug.LogFormat("{0}", controls.GetMovement().ToString("F6"));
            playerRigidbody.MovePosition(playerRigidbody.transform.position + controls.GetMovement() * speed);
            playerRigidbody.MoveRotation(controls.GetRotation(playerRigidbody.transform));
            controls.ClearValues();
        }
    }
}