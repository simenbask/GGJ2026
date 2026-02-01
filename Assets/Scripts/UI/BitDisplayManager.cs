using UnityEngine;
using UnityEngine.Pool;

namespace simenbask.GGJBit.UI
{
    public class BitDisplayManager : MonoBehaviour
    {
        [SerializeField]
        private BitDisplay _prefab;

        [SerializeField]
        private Transform _dragParent;

        public static ObjectPool<BitDisplay> BitDisplayPool;

        private void Awake()
        {
            BitDisplayPool = new ObjectPool<BitDisplay>(
                createFunc: CreateDisplay,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: DestroyItem,
                collectionCheck: true,
                maxSize: 16
                );
        }

        private BitDisplay CreateDisplay()
        {
            BitDisplay bitDisplay = Instantiate(_prefab, transform);
            bitDisplay.SetParents(transform, _dragParent);
            bitDisplay.gameObject.SetActive(false);
            return bitDisplay;
        }

        private void OnGet(BitDisplay bitDisplay)
        {
            bitDisplay.gameObject.SetActive(true);
        }

        private void OnRelease(BitDisplay bitDisplay)
        {
            bitDisplay.gameObject.SetActive(false);
        }

        private void DestroyItem(BitDisplay bitDisplay)
        {
            Destroy(bitDisplay.gameObject);
        }
    }
}
