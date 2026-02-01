using simenbask.GGJBit.Bits;
using UnityEngine;
using UnityEngine.Events;

namespace simenbask.GGJBit.Engine
{
    public class Lock : MonoBehaviour
    {
        [SerializeField]
        private Item _item;

        public UnityEvent OnUnlock;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Item>() != null)
            {
                Item key = collision.gameObject.GetComponent<Item>();
                Debug.Log($"{key.BitItem.BitValue},{key.BitItem.ItemValue} collide with {_item.BitItem.BitValue},{_item.BitItem.ItemValue}");
                if (key.BitItem.BitValue == _item.BitItem.BitValue)
                {
                    OnUnlock.Invoke();
                    key.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
