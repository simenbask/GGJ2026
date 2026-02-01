using com.simenbask.sod.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace simenbask.GGJBit.UI
{
    public class BitMaskMenuSlide : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Reference<float> _slideTime;

        [SerializeField]
        private RectTransform _rectTransform;

        private Vector2 _basePosition;

        private Coroutine _moveCoroutine;

        private void Reset()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Awake()
        {
            _basePosition = _rectTransform.anchoredPosition;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(MoveCoroutine(true));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(MoveCoroutine(false));
        }

        private IEnumerator MoveCoroutine(bool active)
        {
            Vector2 start = _rectTransform.anchoredPosition;
            Vector2 target = active ? Vector2.zero : _basePosition;

            for (float time = 0f; time < _slideTime.Value; time += Time.deltaTime)
            {
                float t = time / _slideTime.Value;
                t = t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;
                _rectTransform.anchoredPosition = Vector2.Lerp(start, target, t);
                yield return null;
            }
            _rectTransform.anchoredPosition = target;
        }
    }
}
