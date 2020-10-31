using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "KeyboardControls", menuName = "SO/Keyboard Controls", order = 1)]
    public class KeyboardControls : Controls
    {
        public override bool IsJoystick()
        {
            return false;
        }

        public override float GetRotation(Transform target)
        {
            return CalculateRotationAngle(target);
        }

        private float CalculateRotationAngle(Transform target)
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var targetPosition = target.position;
            mousePosition.z = targetPosition.z;
            return Vector3.SignedAngle(Vector3.right, mousePosition - targetPosition, Vector3.forward);
        }
    }
}