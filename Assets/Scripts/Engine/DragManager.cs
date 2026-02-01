using com.simenbask.sod.Runtime;
using simenbask.GGJBit.Bits;
using simenbask.GGJBit.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace simenbask.GGJBit.Engine
{
    public class DragManager : MonoBehaviour
    {
        [SerializeField]
        private Reference<bool> _lens;

        [SerializeField]
        private Reference<float> _dragDistance, _dragVelocity, _dragDamping;

        [SerializeField]
        private LayerMask _placeItemLayer;

        [SerializeField]
        private LayerMask _dragLayers;

        private Vector2 _screenPosition;

        private BitItem _dragging;

        private void OnEnable()
        {
            _lens.RegisterListener(ReleaseOnLens);
        }

        private void OnDisable()
        {
            _lens.UnregisterListener(ReleaseOnLens);
        }

        private void Start()
        {
            GlobalManager.Input.Interact.PointerPosition.performed += SetScreenPosition;
            GlobalManager.Input.Interact.Press.started += Press;
            GlobalManager.Input.Interact.Press.canceled += Release;
        }

        private void FixedUpdate()
        {
            if (_dragging == null)
                return;

            //string message = "";
            Vector2 initialVelocity = _dragging.Rigidbody.linearVelocity;
            //message += $"Initial velocity: ({initialVelocity.x}, {initialVelocity.y})\n";
            Vector2 delta = GlobalManager.Camera.ScreenToWorldPoint(_screenPosition) - _dragging.transform.position;
            //message += $"delta: ({delta.x}, {delta.y})\n";
            if (delta.magnitude > _dragDistance.Value)
            {
                _dragging = null;
                return;
            }
            float dot = Vector2.Dot(initialVelocity.normalized, delta.normalized);
            //message += $"dot: {dot}\n";
            initialVelocity = initialVelocity.normalized * Mathf.Lerp(
                _dragDamping.Value,
                1f,
                Mathf.InverseLerp(-1f, 1f, dot)
                );
            //message += $"damping value: {Mathf.InverseLerp(-1f, 1f, dot)}\n";
            Vector2 newVelocity = _dragVelocity.Value * delta.normalized + initialVelocity;
            //message += $"new velocity: ({newVelocity.x}, {newVelocity.y})";
            newVelocity *= delta.magnitude / _dragDistance.Value;
            _dragging.Rigidbody.linearVelocity = newVelocity;
            _dragging.Rigidbody.angularDamping *= _dragDamping.Value;
            //Debug.Log(message);
        }

        private void Press(InputAction.CallbackContext context)
        {
            if (_lens.Value)
                return;

            RaycastHit2D hit = Physics2D.Raycast(GlobalManager.Camera.ScreenToWorldPoint(_screenPosition), Vector2.zero, 150f, _dragLayers);

            if (hit)
                _dragging = hit.rigidbody.GetComponent<BitItem>();
            if (_dragging != null)
                _dragging.Drag(true);
        }

        private void ReleaseOnLens(bool active)
        {
            if (!active)
                return;
            if (_dragging == null)
                return;

            _dragging.Drag(false);
            _dragging = null;
        }

        private void Release(InputAction.CallbackContext context)
        {
            if (_dragging != null)
                _dragging.Drag(false);
            else
                return;

            RaycastHit2D hit = Physics2D.Raycast(GlobalManager.Camera.ScreenToWorldPoint(_screenPosition),
                Vector2.zero,
                150f,
                _placeItemLayer
                );

            if (hit)
            {
                if (hit.collider.GetComponent<IPlaceItem>() != null)
                {
                    hit.collider.GetComponent<IPlaceItem>().PlaceItem(_dragging);
                }
            }

            _dragging = null;
        }

        private void SetScreenPosition(InputAction.CallbackContext context)
        {
            _screenPosition = context.ReadValue<Vector2>();
        }
    }
}
