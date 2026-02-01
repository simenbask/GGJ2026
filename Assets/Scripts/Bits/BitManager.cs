using System;
using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    public class BitManager : MonoBehaviour
    {
        [field: SerializeField]
        public BitParent BitParent { get; private set; }
        [SerializeField]
        private BitRuleConstants _constants;

        public static BitParent ActiveBitParent;
        public static BitRuleConstants Constants;
        public static Action OnResetValues;

        private void Awake()
        {
            ActiveBitParent = BitParent;
            Constants = _constants;
        }

        public void ResetValues()
        {
            OnResetValues?.Invoke();
        }

        public static string PrintBits(BitItem item, bool newLine)
        {
            return $"{Convert.ToString(item.BitValue, toBase: 2).PadLeft(ActiveBitParent.Bits.Length, '0')}{(newLine ? "\n" : " ")}" +
                $"{Convert.ToString(item.ItemValue, toBase: 2).PadLeft((int)Mathf.Log(ActiveBitParent.Items.Length, 2) + 1, '0')}";
        }
    }
}
