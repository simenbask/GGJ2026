using com.simenbask.sod.Runtime;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace simenbask.GGJBit.Engine
{
    public class LensManager : MonoBehaviour
    {
        [SerializeField]
        private BoolVariable _lensActive;

        private void Awake()
        {
            _lensActive.Value = false;
        }

        private void Start()
        {
            GlobalManager.Input.Interact.Lens.performed += (context) => { ToggleLens(); };
        }

        public void ToggleLens()
        {
            _lensActive.Value = !_lensActive.Value;
        }
    }
}
