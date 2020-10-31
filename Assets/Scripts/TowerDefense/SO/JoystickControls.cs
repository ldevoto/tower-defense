﻿using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "JoystickControls", menuName = "SO/Joystick Controls", order = 2)]
    public class JoystickControls : Controls
    {
        [SerializeField] private string horizontalRotationAxis = "HorizontalRotation";
        [SerializeField] private string verticalRotationAxis = "VerticalRotation";
        
        private Vector3 _RotationPosition = Vector3.zero;

        public override bool IsJoystick()
        {
            return true;
        }

        public override float GetRotation(Transform target)
        {
            return CalculateRotationAngle();
        }

        public override void Update()
        {
            base.Update();
            _RotationPosition = new Vector3(Input.GetAxis(horizontalRotationAxis), Input.GetAxis(verticalRotationAxis), 0f);
        }

        private float CalculateRotationAngle()
        {
            return Vector3.SignedAngle(Vector3.right, _RotationPosition - Vector3.zero, Vector3.forward);
        }
    }
}