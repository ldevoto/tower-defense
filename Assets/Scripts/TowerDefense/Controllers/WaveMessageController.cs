using TMPro;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class WaveMessageController : MonoBehaviour
    {
        [SerializeField] private TMP_Text text = null;
        [SerializeField] private Animator animator = null;
        private static readonly int Show = Animator.StringToHash("Show");

        public void ShowMessage(string message)
        {
            text.text = message;
            animator.SetTrigger(Show);
        }
    }
}