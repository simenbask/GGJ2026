using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    [CreateAssetMenu(menuName = "Content/Bits/Create BitRuleConstants", fileName = "BitRuleConstants")]
    public class BitRuleConstants : ScriptableObject
    {
        public LayerMask DefaultLayer;
        public LayerMask EtherealLayer;
        public LayerMask UILayer;
        public PhysicsMaterial2D DefaultMaterial;
        public PhysicsMaterial2D SlipperyMaterial;
    }
}
