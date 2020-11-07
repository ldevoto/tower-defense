using System.Collections;
using UnityEngine;
using TMPro;

namespace TowerDefense.Controllers
{
    public class UpgradeShowerController : MonoBehaviour
    {
        [SerializeField] private TMP_Text costText = null;
        [SerializeField] private Animator animator = null;
        
        private static readonly int HideParam = Animator.StringToHash("Hide");
        private static readonly int BuyParam = Animator.StringToHash("Buy");

        public void ShowCost(int cost)
        {
            costText.text = cost.ToString();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (!gameObject.activeInHierarchy) return;
            
            animator.SetTrigger(HideParam);
            StartCoroutine(DisableCoroutine());
        }

        public void Buy()
        {
            if (!gameObject.activeInHierarchy) return;
            
            animator.SetTrigger(BuyParam);
            StartCoroutine(DisableCoroutine());
        }

        private IEnumerator DisableCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }
    }
}