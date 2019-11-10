using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private bool m_setTimer = false;
    private int m_currentDuration = 0;
    private bool m_isRunning = false;
    private bool m_needReset = false;


    public void TimerStart()
    {
        if (!m_isRunning && m_setTimer)
        {
            m_isRunning = true;
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            m_currentDuration += 1;
            if (m_needReset)
            {
                m_needReset = false;
                EventBus.Instance.Raise(new GameEvents.RuleTriggered { type = RuleType.TimeCount, value = -m_currentDuration });
                m_currentDuration = 0;
                m_isRunning = false;
                yield break;
            }
            yield return new WaitForSeconds(1.0f);
            Debug.Log(m_currentDuration);
            EventBus.Instance.Raise(new GameEvents.RuleTriggered { type = RuleType.TimeCount, value = 1 });
        }
    }

    public void TimerReset()
    {
        m_needReset = true;
    }
}

