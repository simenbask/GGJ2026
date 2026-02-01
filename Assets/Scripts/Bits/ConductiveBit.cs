using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    [CreateAssetMenu(menuName = "Content/Bits/Create new ConductiveBit", fileName = "ConductiveBit")]
    public class ConductiveBit : Bit
    {
        public override void SetBit(bool value, BitItem item)
        {
            item.SetConductive(value);
        }
    }
}
