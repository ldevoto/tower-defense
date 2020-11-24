using System;
using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ShooterController shooterController = null;
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private PlaceHolderController placeHolderController = null;
        [SerializeField] private Rigidbody2D playerRigidbody = null;
        [SerializeField] private UpgradeWatcherController watcherController = null;
        [SerializeField] private LootManager lootManager = null;
        [SerializeField] private PlayerData playerData = null;
        
        public Action OnTowerPlaced = null;
        public Action OnTowerRemoved = null;
        public Action OnKill = null;

        private WallController _wallController = null;
        private bool _constructionMode = false;
        private Controls _controls = null;

        private void Awake()
        {
            SetConstructionMode(false);
        }

        private void Start()
        {
            shooterController.SetCooldown(playerData.shotCooldown);
            shooterController.SetShotData(playerData.shotData);
            aliveEntityController.SetHP(100f);
            aliveEntityController.OnKill += Kill;
            watcherController.OnTowerEnter += OnTowerEnter;
            watcherController.OnTowerLeave += OnTowerLeave;
        }
        
        private void OnTowerEnter(WallController wallController)
        {
            _wallController = wallController;
            _wallController.ShowUpgrade();
        }

        private void OnTowerLeave(WallController wallController)
        {
            _wallController.HideUpgrade();
            _wallController = null;
        }

        public void PickUp(LootData loot)
        {
            lootManager.AddLoot(loot.quantity);
        }
        
        public void SetControls(Controls controls)
        {
            _controls = controls;
            _controls.ResetControls();
        }
        
        private void Update()
        {
            _controls.Update();
            if (_controls.GetAction3())
            {
                SetConstructionMode(!_constructionMode);
            }

            if (_wallController && _controls.GetAction2())
            {
                if (_wallController.TryBuyUpgrade(lootManager))
                {
                    watcherController.ClearWatcher();
                }
            }

            if (_constructionMode)
            {
                if (_controls.GetNextButton())
                {
                    placeHolderController.Next();
                } 
                else if (_controls.GetPreviousButton())
                {
                    placeHolderController.Previous();
                }
                if (_controls.GetAction1())
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
                if (_controls.GetHoldingAction1())
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

        public Transform GetRigidbodyTransform()
        {
            return playerRigidbody.transform;
        }

        private void FixedUpdate()
        {
            playerRigidbody.MovePosition(playerRigidbody.transform.position + _controls.GetMovement() * playerData.movementSpeed);
            playerRigidbody.MoveRotation(_controls.GetRotation(playerRigidbody.transform));
            _controls.ClearValues();
        }

        private void Kill()
        {
            Destroy(gameObject);
            OnKill?.Invoke();
        }
    }
}