using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    [CreateAssetMenu(menuName = "Content/Items/Create new DoorItem", fileName = "DoorItem")]
    public class DoorItem : BaseItem
    {
        public override void SetItem(BitItem item)
        {
            base.SetItem(item);
            item.BecomeDoor(true);
        }
    }
}
