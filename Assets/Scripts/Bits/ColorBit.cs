using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    [CreateAssetMenu(menuName = "Content/Bits/Create new ColorBit", fileName = "ColorBit")]
    public class ColorBit : Bit
    {
        public enum BitColor
        {
            Red = 0,
            Green = 1,
            Blue = 2
        }

        public BitColor Color;

        public override void SetBit(bool value, BitItem item)
        {
            Color oldColor = item.SpriteRenderer.color;
            float floatValue = value ? 1f : 0f;

            item.SpriteRenderer.color = Color switch
            {
                BitColor.Red => new Color(floatValue, oldColor.g, oldColor.b),
                BitColor.Green => new Color(oldColor.r, floatValue, oldColor.b),
                BitColor.Blue => new Color(oldColor.r, oldColor.g, floatValue),
                _ => throw new System.Exception($"Unknown BitColor!")
            };
        }
    }
}
