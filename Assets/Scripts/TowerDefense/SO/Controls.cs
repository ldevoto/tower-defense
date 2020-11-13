using UnityEngine;

namespace TowerDefense.SO
{
    public abstract class Controls : ScriptableObject
    {
        [SerializeField] private string action1 = "Action";
        [SerializeField] private string action2 = "Action2";
        [SerializeField] private string action3 = "Action3";
        [SerializeField] private string nextButton = "NextButton";
        [SerializeField] private string previousButton = "PreviousButton";
        [SerializeField] private string pauseButton = "PauseButton";
        [SerializeField] private string horizontalAxis = "Horizontal";
        [SerializeField] private string verticalAxis = "Vertical";

        public bool isInUI = false;
        
        private Vector3 _movement = Vector3.zero;
        protected Camera Camera;

        public abstract bool IsJoystick();
        
        public abstract float GetRotation(Transform target);
        
        public Vector3 GetMovement()
        {
            return isInUI ? Vector3.zero : _movement;
        }

        public bool GetHoldingAction1()
        {
            return !isInUI && Input.GetButton(action1);
        }
        
        public bool GetAction1()
        {
            return !isInUI && Input.GetButtonDown(action1);
        }

        public bool GetAction2()
        {
            return !isInUI && Input.GetButtonDown(action2);
        }
        
        public bool GetAction3()
        {
            return !isInUI && Input.GetButtonDown(action3);
        }
        
        public bool GetNextButton()
        {
            return !isInUI && Input.GetButtonDown(nextButton);
        }
        
        public bool GetPreviousButton()
        {
            return !isInUI && Input.GetButtonDown(previousButton);
        }

        public bool GetPauseButton()
        {
            return !isInUI && Input.GetButtonDown(pauseButton);
        }

        public virtual void Update()
        {
            if (isInUI) return;
            //Debug.LogFormat("x: {0}, y: {1}, clamp: {2}", Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis), GetClampedMovement() * Time.deltaTime);
            _movement += GetClampedMovement() * Time.deltaTime;
        }

        private Vector3 GetClampedMovement()
        {
            return Vector3.ClampMagnitude(new Vector3(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis), 0f), 1f);
        }

        public void ClearValues()
        {
            _movement = Vector3.zero;
        }

        public virtual void ResetControls()
        {
            Camera = Camera.main;
            ClearValues();
        }
    }
}