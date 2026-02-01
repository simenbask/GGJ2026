using System;
using UnityEngine;
using UnityEngine.UI;

namespace simenbask.GGJBit.Engine
{
    public class Level : MonoBehaviour
    {
        public bool Completed { get; private set; }

        [SerializeField]
        private Button _previousLevelButton, _nextLevelButton;

        public void SetupButtons(Action prev, Action next)
        {
            if (_previousLevelButton != null)
                _previousLevelButton.onClick.AddListener(() => { prev.Invoke(); });
            if (_nextLevelButton != null)
                _nextLevelButton.onClick.AddListener(() => { next.Invoke(); });
        }

        public void CompleteLevel()
        {
            if (Completed)
                return;

            _nextLevelButton.interactable = true;
        }
    }
}
