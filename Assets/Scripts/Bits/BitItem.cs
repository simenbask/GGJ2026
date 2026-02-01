using System;
using System.Linq;
using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    public class BitItem : MonoBehaviour
    {
        [SerializeField]
        private bool _isActive = true;
        [SerializeField]
        private Bit[] _setBits;
        [SerializeField]
        private BaseItem _baseItem;
        [field: SerializeField]
        public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField]
        public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField]
        public BoxCollider2D Collider { get; private set; }

        public event Action OnValueChanged;
        public event Action<bool> OnDragged;
        public event Action<bool> OnPlaced;
        public event Action<bool> OnBecomeDoor;

        public int BitValue { get; private set; }
        private int _initialBitValue;
        public int ItemValue { get; private set; }
        private int _initialItemValue;

        public bool Magnetic { get; private set; }
        public bool Conductive { get; private set; }
        public bool Door { get; private set; }

        private LayerMask _lastLayer;

        private void OnEnable()
        {
            if (_isActive)
                BitManager.OnResetValues += ResetToInitialValues;
        }

        private void OnDisable()
        {
            if (_isActive)
                BitManager.OnResetValues -= ResetToInitialValues;
        }

        public void Initialize()
        {
            if (_isActive)
            {
                ResetValues();
                SetBits(_setBits, _baseItem);
            }
            BitValue = GetInitialBitValue();
            ItemValue = GetInitialItemValue();
            OnValueChanged?.Invoke();
            _initialBitValue = BitValue;
            _initialItemValue = ItemValue;
        }

        public void SetBits(Bit[] setBits, BaseItem item)
        {
            foreach (var bit in BitManager.ActiveBitParent.Bits)
            {
                bit.SetBit(_setBits.Contains(bit), this);
            }
            item.SetItem(this);
        }

        public void SetBits(int value, int item)
        {
            BitValue = value;
            for (int i = 0; i < BitManager.ActiveBitParent.Bits.Length; i++)
                BitManager.ActiveBitParent.Bits[i].SetBit(((1 << i) & value) != 0, this);
            ItemValue = item;
            BitManager.ActiveBitParent.Items[item].SetItem(this);
            OnValueChanged?.Invoke();
        }

        private int GetInitialBitValue()
        {
            int value = 0;
            foreach (var bit in _setBits)
            {
                value |= (1 << BitManager.ActiveBitParent.GetBitIndex(bit));
            }
            return value;
        }

        private int GetInitialItemValue()
        {
            return BitManager.ActiveBitParent.GetItemIndex(_baseItem);
        }

        private void ResetValues()
        {
            SpriteRenderer.color = Color.black;
        }

        public void ResetToInitialValues()
        {
            ResetValues();
            SetBits(_setBits, _baseItem);
            BitValue = _initialBitValue;
            ItemValue = _initialItemValue;
            OnValueChanged?.Invoke();
        }

        public void SetMagnetic(bool magnetic)
        {
            Magnetic = magnetic;
        }

        public void SetConductive(bool conductive)
        {
            Conductive = conductive;
        }

        public void Drag(bool drag)
        {
            Placed(false);
            SpriteRenderer.sortingOrder = drag ? 12 : 1;
            OnDragged?.Invoke(drag);
        }

        public void Placed(bool placed)
        {
            Rigidbody.constraints = placed ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;
            SpriteRenderer.sortingOrder = placed ? 11 : 1;
            OnPlaced?.Invoke(placed);
            if (placed)
            {
                _lastLayer = gameObject.layer;
                gameObject.layer = (int)Mathf.Log(BitManager.Constants.UILayer, 2);
            }
            else
                gameObject.layer = _lastLayer;
        }

        public void BecomeDoor(bool door)
        {
            Door = door;
            OnBecomeDoor?.Invoke(door);
        }
    }
}
