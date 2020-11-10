using System;
using System.Collections;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class AutoDisableController : MonoBehaviour
    {
        [SerializeField] private float ttl = 3f;

        private void OnEnable()
        {
            StartCoroutine(AutoDisable());
        }
        
        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator AutoDisable()
        {
            yield return new WaitForSeconds(ttl);
            gameObject.SetActive(false);
        }
    }
}