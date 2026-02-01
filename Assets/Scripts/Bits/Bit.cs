using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    public abstract class Bit : ScriptableObject
    {
        public abstract void SetBit(bool value, BitItem item);
    }
}
