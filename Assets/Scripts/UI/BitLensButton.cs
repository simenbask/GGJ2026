using com.simenbask.sod.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace simenbask.GGJBit.UI
{
    public class BitLensButton : MonoBehaviour
    {
        [SerializeField]
        private Sprite _activeSprite, _inactiveSprite;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private Reference<bool> _lens;

        private void Start()
        {
            SetIcon(_lens.Value);
        }

        private void OnEnable()
        {
            _lens.RegisterListener(SetIcon);
        }

        private void OnDisable()
        {
            _lens.UnregisterListener(SetIcon);
        }

        private void SetIcon(bool active)
        {
            _image.sprite = active ? _activeSprite : _inactiveSprite;
        }
    }
}
