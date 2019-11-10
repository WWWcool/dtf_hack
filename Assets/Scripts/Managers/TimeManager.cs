using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class TimeManager : MonoBehaviour
    {
        private float m_defaultFixedDeltaTime;
        [SerializeField] private bool m_setTimer;
        [SerializeField] private float m_timerDuration;
        private float m_currentDuration;
        private bool m_isRunning = false;
        private bool m_needReset = false;

        private void Start()
        {
            m_defaultFixedDeltaTime = Time.fixedDeltaTime;
            m_currentDuration = m_timerDuration; 
        }

        public void ScaleTime(float scale)
        {
            Time.timeScale = scale;
            Time.fixedDeltaTime = m_defaultFixedDeltaTime * scale;
        }

        public void OnTimerSrarted()
        {
            if (!m_isRunning)
            {
                m_isRunning = true;
                StartCoroutine(Timer());
            }
        }

        private IEnumerator Timer()
        {
            if (m_needReset)
            {
                m_currentDuration = m_timerDuration;
                m_isRunning = false;
                yield break;
            }

            while (m_currentDuration - Time.deltaTime > 0)
            {
                m_currentDuration -= Time.deltaTime;
                yield return null;
                EventBus.Instance.Raise(new GameEvents.TimerEnded());
            }
        }

        public void OnTimerReset()
        {
            m_needReset = true;
        }

    }
}
