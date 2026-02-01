using com.simenbask.sod.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace simenbask.GGJBit.UI
{
    public class AndOrButton : MonoBehaviour
    {
        [SerializeField]
        private bool _isAnd;

        [SerializeField]
        private Reference<bool> _and;

        [SerializeField]
        private Button _button;

        private void Start()
        {
            SetVisual(_and.Value);
        }

        private void OnEnable()
        {
            _and.RegisterListener(SetVisual);
        }

        private void OnDisable()
        {
            _and.UnregisterListener(SetVisual);
        }

        private void SetVisual(bool active)
        {
            _button.interactable = active != _isAnd;
        }
    }
}
