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
        [SerializeField] private PlaceHolderController placeHolderController = null;
        [SerializeField] private Rigidbody2D playerRigidbody = null;
        [SerializeField] private float speed = 1f;
        [SerializeField] private LootManager lootManager = null;
        
        public Action OnTowerPlaced = null;
        public Action OnTowerRemoved = null;
        public Action OnKill = null;

        private bool _constructionMode = false;

        private void Awake()
        {
            controls.ResetControls();
            SetConstructionMode(false);
        }

        private void Start()
        {
            aliveEntityController.SetHP(100f);
            aliveEntityController.OnKill += Kill;
        }

        public void PickUp(LootData loot)
        {
            lootManager.AddLoot(loot.quantity);
        }

        public Transform GetPlayerTransform()
        {
            return aliveEntityController.gameObject.transform;
        }

        public int GetCurrentLoot()
        {
            return lootManager.GetCurrentLoot();
        }

        private void Update()
        {
            controls.Update();
            if (controls.GetAction3())
            {
                SetConstructionMode(!_constructionMode);
            }

            if (_constructionMode)
            {
                if (controls.GetNextButton())
                {
                    placeHolderController.Next();
                } 
                else if (controls.GetPreviousButton())
                {
                    placeHolderController.Previous();
                }
                if (controls.GetAction1())
                {
                    if (placeHolderController.Place(out var wallController))
                    {
                        wallController.OnKill += OnTowerRemoved;
                        OnTowerPlaced?.Invoke();
                    }
                }
            }
            else
            {
                if (controls.GetHoldingAction1())
                {
                    shooterController.Shot();
                }
            }

        }

        private void SetConstructionMode(bool constructionMode)
        {
            _constructionMode = constructionMode;
            placeHolderController.gameObject.SetActive(_constructionMode);
        }

        private void FixedUpdate()
        {
            playerRigidbody.MovePosition(playerRigidbody.transform.position + controls.GetMovement() * speed);
            playerRigidbody.MoveRotation(controls.GetRotation(playerRigidbody.transform));
            controls.ClearValues();
        }

        private void Kill()
        {
            Destroy(gameObject);
            OnKill?.Invoke();
        }
    }
}