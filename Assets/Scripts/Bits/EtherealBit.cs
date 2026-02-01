using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    [CreateAssetMenu(menuName = "Content/Bits/Create new EtherealBit", fileName = "EtherealBit")]
    public class EtherealBit : Bit
    {
        public override void SetBit(bool value, BitItem item)
        {
            item.gameObject.layer = value ? 
                (int)Mathf.Log(BitManager.Constants.EtherealLayer, 2) : 
                (int)Mathf.Log(BitManager.Constants.DefaultLayer, 2);
        }
    }
}
