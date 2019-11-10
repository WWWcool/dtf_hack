using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityPrototype
{
    public class EventListener : MonoBehaviour
    {
        private enum EventType
        {
            ImpulseGiven,
            InputStarted,
            InputFinished,
            InputBlocked,
            RuleTriggered,
            GameEnded,
            TimerEnded,
        }

        [SerializeField] private EventType m_eventType;
        [SerializeField] private float m_delay = 0.0f;
        [SerializeField] private UnityEvent m_onEvent;
        [SerializeField] private bool m_logEvents;

        private void OnEnable()
        {
            EventBus.Instance.AddListener<GameEvents.ImpulseGiven>(OnImpulseGiven);
            EventBus.Instance.AddListener<GameEvents.InputStarted>(OnInputStarted);
            EventBus.Instance.AddListener<GameEvents.InputFinished>(OnInputFinished);
            EventBus.Instance.AddListener<GameEvents.InputBlocked>(OnInputBlocked);
            EventBus.Instance.AddListener<GameEvents.RuleTriggered>(OnRuleTriggered);
            EventBus.Instance.AddListener<GameEvents.GameEnded>(OnGameEnded);
            EventBus.Instance.AddListener<GameEvents.TimerEnded>(OnTimerEnded);
        }

        private void OnDisable()
        {
            EventBus.Instance.RemoveListener<GameEvents.ImpulseGiven>(OnImpulseGiven);
            EventBus.Instance.RemoveListener<GameEvents.InputStarted>(OnInputStarted);
            EventBus.Instance.RemoveListener<GameEvents.InputFinished>(OnInputFinished);
            EventBus.Instance.RemoveListener<GameEvents.InputBlocked>(OnInputBlocked);
            EventBus.Instance.RemoveListener<GameEvents.RuleTriggered>(OnRuleTriggered);
            EventBus.Instance.RemoveListener<GameEvents.GameEnded>(OnGameEnded);
        }

        private void OnImpulseGiven(GameEvents.ImpulseGiven e)
        {
            OnEvent(EventType.ImpulseGiven);
        }

        private void OnInputStarted(GameEvents.InputStarted e)
        {
            OnEvent(EventType.InputStarted);
        }

        private void OnInputFinished(GameEvents.InputFinished e)
        {
            OnEvent(EventType.InputFinished);
        }

        private void OnInputBlocked(GameEvents.InputBlocked e)
        {
            OnEvent(EventType.InputFinished);
        }

        private void OnRuleTriggered(GameEvents.RuleTriggered e)
        {
            OnEvent(EventType.RuleTriggered);
        }

        private void OnGameEnded(GameEvents.GameEnded e)
        {
            OnEvent(EventType.GameEnded);
        }

        private void OnTimerEnded(GameEvents.TimerEnded e)
        {
            OnEvent(EventType.TimerEnded);
        }

        private void OnEvent(EventType eventType)
        {
            if (eventType == m_eventType){
                if(m_logEvents){
                    print("[EventListener][OnEvent] got event: " + eventType.ToString());
                }
                StartCoroutine(InvokeCallbackDelayed());
            }
        }

        private IEnumerator InvokeCallbackDelayed()
        {
            yield return new WaitForSeconds(m_delay);
            m_onEvent?.Invoke();
        }
    }
}
