using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class SlowmoManager : MonoBehaviour
    {
        [SerializeField] private float m_inputTimeScale = 0.1f;

        private void OnEnable()
        {
            EventBus.Instance.AddListener<GameEvents.InputStarted>(OnInputStarted);
            EventBus.Instance.AddListener<GameEvents.InputFinished>(OnInputFinished);
        }

        private void OnDisable()
        {
            EventBus.Instance.RemoveListener<GameEvents.InputStarted>(OnInputStarted);
            EventBus.Instance.RemoveListener<GameEvents.InputFinished>(OnInputFinished);
        }

        public void OnInputStarted(GameEvents.InputStarted e)
        {
            ServiceLocator.Get<TimeManager>().ScaleTime(m_inputTimeScale);
        }
        
        public void OnInputFinished(GameEvents.InputFinished e)
        {
            ServiceLocator.Get<TimeManager>().ScaleTime(1.0f);
        }
    }
}
