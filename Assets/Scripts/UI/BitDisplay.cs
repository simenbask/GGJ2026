using com.simenbask.sod.Runtime;
using simenbask.GGJBit.Bits;
using System;
using TMPro;
using UnityEngine;

namespace simenbask.GGJBit.UI
{
    public class BitDisplay : MonoBehaviour
    {
        [SerializeField]
        private BitItem _bitItem;

        [SerializeField]
        private Reference<bool> _lensActive;

        [SerializeField]
        private GameObject _textParent;

        [SerializeField]
        private TMP_Text _text;

        private bool _subscribed;
        private bool _active;
        private bool _placed;

        private Transform _defaultParent, _dragParent;

        public void Initialize(BitItem item)
        {
            _bitItem = item;
            _bitItem.OnValueChanged += UpdateBits;
            _bitItem.OnDragged += Drag;
            _bitItem.OnPlaced += SetPlaced;
            _lensActive.RegisterListener(ShowBits);
            _subscribed = true;
            _placed = false;
            ShowBits(_lensActive.Value);
        }

        private void OnDisable()
        {
            if (_subscribed)
            {
                _lensActive.UnregisterListener(ShowBits);
                _bitItem.OnValueChanged -= UpdateBits;
                _bitItem.OnDragged -= Drag;
                _bitItem.OnPlaced -= SetPlaced;
                _subscribed = false;
            }
        }

        private void Update()
        {
            if (!_active)
                return;
            JumpToItem();
        }

        public void SetParents(Transform defaultParent, Transform dragParent)
        {
            _defaultParent = defaultParent;
            _dragParent = dragParent;
        }

        private void Drag(bool drag)
        {
            transform.SetParent(drag ? _dragParent : _defaultParent, true);
        }

        private void ShowBits(bool show)
        {
            _active = show;
            if (show)
                JumpToItem();
            if (show)
                UpdateBits();
            _textParent.SetActive(show && !_placed);
        }

        private void SetPlaced(bool placed)
        {
            _placed = placed;
            ShowBits(_active);
        }

        private void UpdateBits()
        {
            _text.text = BitManager.PrintBits(_bitItem, true);
        }

        private void JumpToItem()
        {
            transform.position = new Vector3(
                _bitItem.transform.position.x,
                _bitItem.transform.position.y,
                0);
        }
    }
}
