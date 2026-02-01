using com.simenbask.sod.Runtime;
using UnityEngine;

namespace simenbask.GGJBit.Engine
{
    public class BitMaskManager : MonoBehaviour
    {
        [SerializeField]
        private BoolVariable _and;

        private void Awake()
        {
            _and.Value = false;
        }

        public void SetAnd(bool and)
        {
            _and.Value = and;
        }
    }
}
