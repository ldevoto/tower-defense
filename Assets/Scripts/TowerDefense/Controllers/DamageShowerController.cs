using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class DamageShowerController : MonoBehaviour
    {
        [SerializeField] private TMP_Text text = null;
        
        private Animator _animator = null;
        private static readonly int ShowParam = Animator.StringToHash("Show");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Show(float damage)
        {
            text.text = damage.ToString();
            _animator.SetTrigger(ShowParam);
            StartCoroutine(DisableCoroutine());
        }

        private IEnumerator DisableCoroutine()
        {
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length + _animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            gameObject.SetActive(false);
        }
    }
}