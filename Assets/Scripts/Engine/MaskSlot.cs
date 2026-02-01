using com.simenbask.sod.Runtime;
using simenbask.GGJBit.Bits;
using System;
using UnityEngine;

namespace simenbask.GGJBit.Engine
{
    public class MaskSlot : MonoBehaviour, IPlaceItem
    {
        private const float LowestYWhenDropped = -2.9f;

        [SerializeField]
        private StringVariable _placedItemBitMask;

        public BitItem PlacedItem { get; private set; }

        private void Awake()
        {
            _placedItemBitMask.Value = "";
        }

        private void OnChangedValue()
        {
            _placedItemBitMask.Value = BitManager.PrintBits(PlacedItem, false);
        }

        private void OnLeaveLevel()
        {
            RemovePlaced(false);
        }

        public void PlaceItem(BitItem item)
        {
            if (PlacedItem != null)
                PlacedItem.Placed(false);

            PlacedItem = item;
            PlacedItem.Placed(true);
            PlacedItem.OnPlaced += RemovePlaced;
            _placedItemBitMask.Value = BitManager.PrintBits(PlacedItem, false);
            PlacedItem.GetComponent<FollowTransform>().SetTransform(transform);
            PlacedItem.OnValueChanged += OnChangedValue;
            LevelManager.OnLeaveLevel += OnLeaveLevel;
        }

        private void RemovePlaced(bool placed)
        {
            if (placed)
                return;

            PlacedItem.OnPlaced -= RemovePlaced;
            PlacedItem.Placed(false);
            PlacedItem.GetComponent<FollowTransform>().SetTransform(null);
            if (PlacedItem.transform.position.y < LowestYWhenDropped)
                PlacedItem.transform.position = new Vector3(
                    PlacedItem.transform.position.x, 
                    LowestYWhenDropped, 
                    PlacedItem.transform.position.z);
            PlacedItem.OnValueChanged -= OnChangedValue;
            PlacedItem = null;
            _placedItemBitMask.Value = "";
            LevelManager.OnLeaveLevel -= OnLeaveLevel;
        }
    }
}
