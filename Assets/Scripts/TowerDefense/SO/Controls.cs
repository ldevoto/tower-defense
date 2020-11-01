using UnityEngine;

namespace TowerDefense.SO
{
    public abstract class Controls : ScriptableObject
    {
        [SerializeField] private string action1 = "Action";
        [SerializeField] private string action2 = "Action2";
        [SerializeField] private string horizontalAxis = "Horizontal";
        [SerializeField] private string verticalAxis = "Vertical";
        
        private Vector3 _movement = Vector3.zero;
        private bool _action1Value = false;
        private bool _action2Value = false;
        protected Camera Camera;

        public abstract bool IsJoystick();
        
        public abstract float GetRotation(Transform target);
        
        public Vector3 GetMovement()
        {
            return _movement;
        }

        public bool GetAction1()
        {
            return _action1Value;
        }

        public bool GetAction2()
        {
            return _action2Value;
        }

        public virtual void Update()
        {
            //Debug.LogFormat("x: {0}, y: {1}, clamp: {2}", Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis), GetClampedMovement() * Time.deltaTime);
            _movement += GetClampedMovement() * Time.deltaTime;
            _action1Value = Input.GetButton(action1);
            _action2Value = Input.GetButton(action2);
        }

        private Vector3 GetClampedMovement()
        {
            return Vector3.ClampMagnitude(new Vector3(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis), 0f), 1f);
        }

        public void ClearValues()
        {
            _movement = Vector3.zero;
            _action1Value = false;
            _action2Value = false;
        }

        public void ResetControls()
        {
            Camera = Camera.main;
            ClearValues();
        }
    }
}