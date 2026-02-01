using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    [CreateAssetMenu(menuName = "Content/Bits/Create new GravityBit", fileName = "GravityBit")]
    public class GravityBit : Bit
    {
        public override void SetBit(bool value, BitItem item)
        {
            item.Rigidbody.gravityScale = value ? 1f : 0f;
        }
    }
}
