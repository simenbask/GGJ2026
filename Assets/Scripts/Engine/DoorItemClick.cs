using UnityEngine;

namespace simenbask.GGJBit.Engine
{
    public class DoorItemClick : MonoBehaviour
    {
        private bool _isIn;
        private bool _clickedIn;

        public void OnMouseEnter()
        {
            if (!enabled)
                return;
            _isIn = true;
        }

        public void OnMouseExit()
        {
            if (!enabled)
                return;

            _isIn = false;
            _clickedIn = false;
        }

        public void OnMouseDown()
        {
            if (!enabled)
                return;

            if (!_isIn)
                return;

            _clickedIn = true;
        }

        public void OnMouseUp()
        {
            if (!enabled)
                return;

            if (!_clickedIn)
                return;
            _clickedIn = false;

            if (!_isIn)
                return;

            Debug.Log("Click door!");
            LevelManager.OnPressItemDoor?.Invoke();
        }
    }
}
