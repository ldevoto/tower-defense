using System;
using TowerDefense.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.UIController
{
    public class LevelStateController : MonoBehaviour
    {
        [SerializeField] private Controls[] controls = null;
        [SerializeField] private Animator uiAnimator = null;
        
        private static readonly int InitParam = Animator.StringToHash("Init");
        private static readonly int PauseParam = Animator.StringToHash("Pause");
        private static readonly int WinParam = Animator.StringToHash("Win");
        private static readonly int LoseParam = Animator.StringToHash("Lose");
        private static readonly int CloseParam = Animator.StringToHash("Close");

        public void Init()
        {
            Time.timeScale = 0;
            DisableControls();
            uiAnimator.SetTrigger(InitParam);
        }
        
        public void Pause()
        {
            Time.timeScale = 0;
            DisableControls();
            uiAnimator.SetTrigger(PauseParam);
        }

        public void Win()
        {
            Time.timeScale = 0;
            DisableControls();
            uiAnimator.SetTrigger(WinParam);
        }

        public void Lose()
        {
            Time.timeScale = 0;
            DisableControls();
            uiAnimator.SetTrigger(LoseParam);
        }

        public void HandlePlay()
        {
            Time.timeScale = 1;
            EnableControls();
            uiAnimator.SetTrigger(CloseParam);
        }

        public void HandleRestart()
        {
            Time.timeScale = 1;
            EnableControls();
            uiAnimator.SetTrigger(CloseParam);
            SceneManager.LoadScene(0);
        }

        public void HandleQuit()
        {
            Time.timeScale = 1;
            EnableControls();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }

        private void DisableControls()
        {
            foreach (var control in controls)
            {
                control.isInUI = true;
            }
        }

        private void EnableControls()
        {
            foreach (var control in controls)
            {
                control.isInUI = false;
            }
        }
    }
}