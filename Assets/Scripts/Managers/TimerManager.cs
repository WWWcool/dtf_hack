using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{  
    [SerializeField] private float m_timerDuration;
    private float m_currentDuration;
    private bool m_isRunning = false;
    private bool m_needReset = false;

    private void Start()
    {
        m_currentDuration = m_timerDuration;
    }

    public void TimerSrart()
    {
        if (!m_isRunning)
        {
            m_isRunning = true;
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        while (m_currentDuration > 0)
        {
            m_currentDuration -= Time.deltaTime;
            if (m_needReset)
            {
                m_needReset = false;
                m_currentDuration = m_timerDuration;
                m_isRunning = false;
                yield break;
            }
            Debug.Log(m_currentDuration);
            yield return null;
        }
        EventBus.Instance.Raise(new GameEvents.TimerEnded());
    }

    public void TimerReset()
    {
        m_needReset = true;
    }
}

