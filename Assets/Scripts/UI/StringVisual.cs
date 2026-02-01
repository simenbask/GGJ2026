using com.simenbask.sod.Runtime;
using TMPro;
using UnityEngine;

namespace simenbask.GGJBit.UI
{
    public class StringVisual : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private Reference<string> _string;

        private void Start()
        {
            _text.text = _string.Value;
        }

        private void OnEnable()
        {
            _string.RegisterListener(SetText);
        }

        private void OnDisable()
        {
            _string.UnregisterListener(SetText);
        }

        private void SetText(string text)
        {
            _text.text = text;
        }
    }
}
