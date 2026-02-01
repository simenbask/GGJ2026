using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    [CreateAssetMenu(menuName = "Content/Bits/Create new MagneticBit", fileName = "MagneticBit")]
    public class MagneticBit : Bit
    {
        public override void SetBit(bool value, BitItem item)
        {
            item.SetMagnetic(value);
        }
    }
}
