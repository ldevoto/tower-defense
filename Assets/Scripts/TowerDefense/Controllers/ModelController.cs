using System;
using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class ModelController : MonoBehaviour
    {
        [SerializeField] private TowerData towerData = null;
        [SerializeField] private WallController towerPrefab = null;

        private static readonly int IsValid = Animator.StringToHash("IsValid");
        private static readonly int IsInvalid = Animator.StringToHash("IsInvalid");
        private Animator _animator = null;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public int GetCost()
        {
            return towerData.cost;
        }

        public WallController GetPrefab()
        {
            return towerPrefab;
        }

        public void ChangeAllowedPosition(bool allowedPosition)
        {
            _animator.SetTrigger(allowedPosition ? IsValid : IsInvalid);
        }
    }
}