using com.simenbask.sod.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace simenbask.GGJBit.Engine
{
    public class LevelManager : MonoBehaviour
    {
        private const float HorizontalOffsetPerLevel = 20f;

        [SerializeField]
        private Reference<float> _cameraMoveTime;

        [SerializeField]
        private Level[] _levelPrefabs;

        public static Action OnPressItemDoor;
        public static Action OnLeaveLevel;

        public List<Level> InstantiatedLevels { get; private set; }

        public int Level { 
            get { return _level; }
            private set
            {
                if (value >= _levelPrefabs.Length || value < 0 || _loading || _moving)
                    return;

                if (value >= InstantiatedLevels.Count)
                    StartCoroutine(LoadLevel(value));

                if (_cameraCoroutine != null)
                    StopCoroutine(_cameraCoroutine);
                _cameraCoroutine = StartCoroutine(MoveCameraToLevel(value, _level));
                _level = value;
            }
        }

        private int _level;
        private Coroutine _cameraCoroutine;
        private bool _loading = false;
        private bool _moving = false;

        private void Awake()
        {
            OnPressItemDoor += NextLevel;
        }

        private void Start()
        {
            InstantiatedLevels = new List<Level>();
            Level = 0;
        }

        private IEnumerator LoadLevel(int level)
        {
            if (level < InstantiatedLevels.Count)
                yield break;
            Debug.Log($"Loading level {level}");
            _loading = true;
            AsyncInstantiateOperation async = InstantiateAsync(
                _levelPrefabs[level], 
                Vector3.right * HorizontalOffsetPerLevel * level, 
                Quaternion.identity);
            while (!async.isDone)
                yield return null;
            InstantiatedLevels.Add(async.Result[0] as Level);
            InstantiatedLevels[level].SetupButtons(PreviousLevel, NextLevel);
            Debug.Log($"Done loading level.");
            _loading = false;
        }

        public void NextLevel()
        {
            if (_moving)
                return;
            Debug.Log("Next level");
            Level += 1;
        }

        public void PreviousLevel()
        {
            if (_moving)
                return;
            Debug.Log("Previous level");
            Level -= 1;
        }

        private IEnumerator MoveCameraToLevel(int level, int toDisable)
        {
            while (_loading)
                yield return null;

            OnLeaveLevel?.Invoke();

            _moving = true;
            InstantiatedLevels[level].gameObject.SetActive(true);

            Vector3 start = GlobalManager.Camera.transform.position;
            Vector3 end = level * HorizontalOffsetPerLevel * Vector3.right + Vector3.forward * -10;

            for (float time = 0f; time < _cameraMoveTime.Value; time += Time.deltaTime)
            {
                float t = time / _cameraMoveTime.Value;
                t = t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;
                GlobalManager.Camera.transform.position = Vector3.Lerp(start, end, t);
                yield return null;
            }
            GlobalManager.Camera.transform.position = end;

            if (level != toDisable)
                InstantiatedLevels[toDisable].gameObject.SetActive(false);
            _moving = false;
        }
    }
}
