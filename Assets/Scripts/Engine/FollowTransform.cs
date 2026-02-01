using UnityEngine;

namespace simenbask.GGJBit.Engine
{
    public class FollowTransform : MonoBehaviour
    {
        [SerializeField]
        private bool _freezeX, _freezeY, _freezeZ;

        [SerializeField]
        private Transform _transform;

        private void Update()
        {
            if (_transform == null)
                return;

            transform.position = new Vector3(
                _freezeX ? transform.position.x : _transform.position.x,
                _freezeY ? transform.position.y : _transform.position.y,
                _freezeZ ? transform.position.z : _transform.position.z
                );
        }

        public void SetTransform(Transform t)
        {
            _transform = t;
        }
    }
}
