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

        protected virtual void Start()
        {
            SetText("0");
        }

        public void UpdateText(int loot)
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

            _lastLootUpdate = loot;
            SetText(_lastLootUpdate.ToString());
        }

        private void SetText(string text)
        {
            textComponent.text = text;
        }
    }
}