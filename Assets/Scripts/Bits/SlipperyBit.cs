using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    [CreateAssetMenu(menuName = "Content/Bits/Create new SlipperyBit", fileName = "SlipperyBit")]
    public class SlipperyBit : Bit
    {
        public override void SetBit(bool value, BitItem item)
        {
            item.Collider.sharedMaterial = value ? BitManager.Constants.SlipperyMaterial : BitManager.Constants.DefaultMaterial;
            item.Rigidbody.sharedMaterial = value ? BitManager.Constants.SlipperyMaterial : BitManager.Constants.DefaultMaterial;
        }
    }
}
