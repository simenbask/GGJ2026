using com.simenbask.sod.Runtime;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace simenbask.GGJBit.UI
{
    public class HoverColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Reference<float> _fadeTime;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private Color _toColor;

        private Color _baseColor;

        private Coroutine _fadeCoroutine;

        private void Reset()
        {
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            if (_image != null)
                _baseColor = _image.color;
            else if (_spriteRenderer != null)
                _baseColor = _spriteRenderer.color;
        }

        public void SetBaseColor(Color baseColor)
        {
            _baseColor = baseColor;
        }

        public void OnMouseEnter()
        {
            if (!enabled)
                return;
            if (_fadeCoroutine != null)
                StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = StartCoroutine(FadeCoroutine(true));
        }

        public void OnMouseExit()
        {
            if (!enabled)
                return;
            if (_fadeCoroutine != null)
                StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = StartCoroutine(FadeCoroutine(false));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!enabled)
                return;
            if (_fadeCoroutine != null)
                StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = StartCoroutine(FadeCoroutine(true));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!enabled)
                return;
            if (_fadeCoroutine != null)
                StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = StartCoroutine(FadeCoroutine(false));
        }

        private IEnumerator FadeCoroutine(bool active)
        {
            Color start;
            if (_image != null)
                start = _image.color;
            else if (_spriteRenderer != null)
                start = _spriteRenderer.color;
            else
                start = _baseColor;
            Color target = active ? _toColor : _baseColor;

            for (float time = 0f; time < _fadeTime.Value; time += Time.deltaTime)
            {
                float t = time / _fadeTime.Value;
                t = t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;
                if (_image != null)
                    _image.color = Color.Lerp(start, target, t);
                if (_spriteRenderer != null)
                    _spriteRenderer.color = Color.Lerp(start, target, t);
                yield return null;
            }
            if (_image != null)
                _image.color = target;
            if (_spriteRenderer != null)
                _spriteRenderer.color = target;
        }
    }
}
