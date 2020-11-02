using System;
using System.Collections;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class LifeBarController : MonoBehaviour
    {
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] private float transitionTime = 0.5f;

        private Coroutine _barUpdateCoroutine = null;
        private static readonly int Percentage = Animator.StringToHash("Percentage");

        private void Start()
        {
            aliveEntityController.OnHpChange += UpdateLifeBar;
        }

        private void UpdateLifeBar()
        {
            if (_barUpdateCoroutine != null)
            {
                StopCoroutine(_barUpdateCoroutine);
            }

            _barUpdateCoroutine = StartCoroutine(UpdateLifeBarCoroutine(aliveEntityController.GetHpPercentage()));
        }

        private IEnumerator UpdateLifeBarCoroutine(float destinationPercentage)
        {
            var currentPercentage = animator.GetFloat(Percentage);
            
            if (Math.Abs(currentPercentage - destinationPercentage) < Mathf.Epsilon) yield break;
            
            for (var currentTime = 0f; currentTime <= transitionTime; currentTime += Time.deltaTime)
            {
                //Debug.LogFormat("currentPercentage: {0}, destinationPercentage: {1}, t: {2}", currentPercentage, destinationPercentage, currentTime / transitionTime);
                yield return null;
                animator.SetFloat(Percentage, Mathf.Lerp(currentPercentage, destinationPercentage, currentTime / transitionTime));
            }
            animator.SetFloat(Percentage, destinationPercentage);
        }
    }
}