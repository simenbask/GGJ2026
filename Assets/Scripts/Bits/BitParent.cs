using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    [CreateAssetMenu(menuName = "Content/Bits/Create BitParent", fileName = "BitParent")]
    public class BitParent : ScriptableObject
    {
        public Bit[] Bits;
        public BaseItem[] Items;

        public Bit GetBit(int index)
        {
            return Bits[index];
        }

        public int GetBitIndex(Bit bit)
        {
            for (int i = 0; i < Bits.Length; i++)
            {
                if (Bits[i] == bit)
                    return i;
            }
            throw new System.Exception($"Bit {bit.name} not in BitParent!");
        }

        public int GetItemIndex(BaseItem item)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == item)
                    return i;
            }
            throw new System.Exception($"Item {item.name} not in BitParent!");
        }
    }
}
