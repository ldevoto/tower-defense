using System.Collections;
using TMPro;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class LootShowerController : MonoBehaviour
    {
        [SerializeField] private TMP_Text textComponent = null;
        [SerializeField] private float updateTime = 1f;

        private int _lastLootUpdate = 0;
        private PlayerController _playerController = null;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            _playerController.OnPickupLoot += () => UpdateText(_playerController.GetCurrentLoot());
            SetText("0");
        }

        private void UpdateText(int loot)
        {
            StopAllCoroutines();
            StartCoroutine(UpdateTextCoroutine(loot));
        }

        private IEnumerator UpdateTextCoroutine(int loot)
        {
            var timePassed = 0f;
            var baseLoot = _lastLootUpdate;
            while (timePassed <= updateTime)
            {
                _lastLootUpdate = Mathf.CeilToInt(Mathf.Lerp(baseLoot, loot, timePassed / updateTime));
                SetText(_lastLootUpdate.ToString());
                timePassed += Time.deltaTime;
                yield return null;
            }
        }

        private void SetText(string text)
        {
            textComponent.text = text;
        }
    }
}