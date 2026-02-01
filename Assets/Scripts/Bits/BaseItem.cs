using UnityEngine;

namespace simenbask.GGJBit.Bits
{
    [CreateAssetMenu(menuName = "Content/Items/Create new BaseItem", fileName = "BaseItem")]
    public class BaseItem : ScriptableObject
    {
        public Sprite Sprite;
        public Vector2 ColliderSize;

        public virtual void SetItem(BitItem item)
        {
            if (item.Door)
                item.BecomeDoor(false);
            item.SpriteRenderer.sprite = Sprite;
            item.Collider.size = ColliderSize;
        }
    }
}
