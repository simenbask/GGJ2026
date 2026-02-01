using simenbask.GGJBit.Bits;
using simenbask.GGJBit.UI;
using UnityEngine;

namespace simenbask.GGJBit.Engine
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField]
        public BitItem BitItem;

        [SerializeField]
        private BitDisplay _bitDisplay;

        [SerializeField]
        private HoverColor _doorHover;

        [SerializeField]
        private DoorItemClick _doorClick;

        private void OnEnable()
        {
            _bitDisplay = BitDisplayManager.BitDisplayPool.Get();
            _bitDisplay.Initialize(BitItem);
            BitItem.Initialize();
            BitItem.OnBecomeDoor += BecomeDoor;
        }

        private void OnDisable()
        {
            BitDisplayManager.BitDisplayPool.Release(_bitDisplay);
            BitItem.OnBecomeDoor -= BecomeDoor;
        }

        private void BecomeDoor(bool door)
        {
            _doorHover.enabled = door;
            _doorClick.enabled = door;
        }
    }
}
