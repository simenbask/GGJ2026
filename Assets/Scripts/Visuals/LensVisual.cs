using com.simenbask.sod.Runtime;
using System.Collections;
using UnityEngine;

namespace simenbask.GGJBit.Visuals
{
    public class LensVisual : MonoBehaviour
    {
        private const float MaxSize = 260f;

        [SerializeField]
        private Reference<float> _wipeDuration;

        [SerializeField]
        private Reference<bool> _lens;

        [SerializeField]
        private RectTransform _rt;

        private Coroutine _visualCoroutine;

        private void Start()
        {
            _rt.sizeDelta = new Vector2(0f, _rt.sizeDelta.y);
        }

        private void OnEnable()
        {
            _lens.RegisterListener(SetVisual);
        }

        private void OnDisable()
        {
            _lens.UnregisterListener(SetVisual);
        }

        private void SetVisual(bool active)
        {
            if (_visualCoroutine != null)
                StopCoroutine(_visualCoroutine);

            _visualCoroutine = StartCoroutine(Wipe(active));
        }

        private IEnumerator Wipe(bool active)
        {
            float start = _rt.sizeDelta.x;
            float end = active ? MaxSize : 0f;

            for (float time = 0f; time < _wipeDuration.Value; time += Time.deltaTime)
            {
                float t = time / _wipeDuration.Value;
                t = t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;
                float size = Mathf.Lerp(start, end, t);
                _rt.sizeDelta = new Vector2(size, _rt.sizeDelta.y);
                yield return null;
            }
            _rt.sizeDelta = new Vector2(end, _rt.sizeDelta.y);
        }
    }
}
