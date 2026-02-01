using com.simenbask.sod.Runtime;
using simenbask.GGJBit.Bits;
using UnityEngine;

namespace simenbask.GGJBit.Engine
{
    public class EnterBitItem : MonoBehaviour, IPlaceItem
    {
        private const float ExitYOffset = 1.5f;
        [SerializeField]
        private Reference<float> _exitVelocity;

        [SerializeField]
        private Reference<bool> _and;

        [SerializeField]
        private MaskSlot _maskSlot;

        [SerializeField]
        private Transform _exit;

        public void PlaceItem(BitItem item)
        {
            if (_maskSlot.PlacedItem != null)
                item.SetBits(
                    _and.Value ? item.BitValue & _maskSlot.PlacedItem.BitValue : item.BitValue | _maskSlot.PlacedItem.BitValue,
                    _and.Value ? item.ItemValue & _maskSlot.PlacedItem.ItemValue : item.ItemValue | _maskSlot.PlacedItem.ItemValue
                    );

            item.transform.position = new Vector3(_exit.position.x, ExitYOffset + _exit.position.y, 0f);
            item.Rigidbody.linearVelocity = Vector2.up * _exitVelocity.Value;
        }
    }
}
