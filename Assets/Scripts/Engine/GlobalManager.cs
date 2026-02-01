using simenbask.GGJBit.Input;
using System;
using UnityEngine;

namespace simenbask.GGJBit.Engine
{
    public class GlobalManager : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        public static Camera Camera;
        public static InputActions Input;

        private void Awake()
        {
            Camera = _camera;
            Input = new InputActions();
            Input.Enable();
            Input.Interact.Enable();
        }

        private void OnDestroy()
        {
            Input.Interact.Disable();
            Input.Disable();
        }
    }
}
