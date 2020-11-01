using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "JoystickControls", menuName = "SO/Joystick Controls", order = 2)]
    public class JoystickControls : Controls
    {
        [SerializeField] private string rotationAxis = "Rotation";
        [SerializeField] private float rotationSpeed = 50f;
        
        private float _rotationValue = 0f;

        public override bool IsJoystick()
        {
            return true;
        }

        public override float GetRotation(Transform target)
        {
            return _rotationValue;
        }

        public override void Update()
        {
            base.Update();
            _rotationValue += Input.GetAxis(rotationAxis) * Time.deltaTime * rotationSpeed;
        }

        public override void ResetControls()
        {
            base.ResetControls();
            _rotationValue = 0f;
        }
    }
}