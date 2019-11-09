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
            BallReachedGoal,
            ImpulseGiven,
            InputStarted,
            InputFinished,
        }

        [SerializeField] private EventType m_eventType;
        [SerializeField] private UnityEvent m_onEvent;

        private void OnEnable()
        {
            EventBus.Instance.AddListener<GameEvents.BallReachedGoal>(OnBalLReachedGoal);
            EventBus.Instance.AddListener<GameEvents.ImpulseGiven>(OnImpulseGiven);
            EventBus.Instance.AddListener<GameEvents.InputStarted>(OnInputStarted);
            EventBus.Instance.AddListener<GameEvents.InputFinished>(OnInputFinished);
        }

        private void OnDisable()
        {
            EventBus.Instance.RemoveListener<GameEvents.BallReachedGoal>(OnBalLReachedGoal);
            EventBus.Instance.RemoveListener<GameEvents.ImpulseGiven>(OnImpulseGiven);
            EventBus.Instance.RemoveListener<GameEvents.InputStarted>(OnInputStarted);
            EventBus.Instance.RemoveListener<GameEvents.InputFinished>(OnInputFinished);
        }

        private void OnBalLReachedGoal(GameEvents.BallReachedGoal e)
        {
            OnEvent(EventType.BallReachedGoal);
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

        private void OnEvent(EventType eventType)
        {
            if (eventType == m_eventType)
                m_onEvent?.Invoke();
        }
    }
}
