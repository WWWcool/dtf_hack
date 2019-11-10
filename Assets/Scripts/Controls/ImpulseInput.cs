using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityPrototype
{
    public class ImpulseInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float m_maxImpulse;
        [SerializeField] private float m_impulseScale;
        [SerializeField] private UnityEventVector2 m_onInput;

        private Vector2 m_startPoint = Vector2.zero;
        private Vector2 m_endPoint = Vector2.zero;
        private bool m_inputInProgress = false;
        private Camera m_camera;

        private bool m_inputBlocked = false;

        private void OnEnable(){
            EventBus.Instance.AddListener<GameEvents.InputBlocked>(OnInputBlocked);
        }

        private void Start()
        {
            m_camera = Camera.main;
        }

        private Vector2 GetEventWorldPosition(PointerEventData eventData)
        {
            return m_camera.ScreenToWorldPoint(eventData.position);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(m_inputBlocked)
                return;

            m_startPoint = GetEventWorldPosition(eventData);
            m_inputInProgress = true;

            EventBus.Instance.Raise(new GameEvents.InputStarted());
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            m_endPoint = GetEventWorldPosition(eventData);

            EventBus.Instance.Raise(new GameEvents.InputUpdated { impulse = CalculateImpulse() });
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(m_inputBlocked)
                return;

            m_endPoint = GetEventWorldPosition(eventData);
            m_inputInProgress = false;

            EventBus.Instance.Raise(new GameEvents.InputFinished { impulse = CalculateImpulse() });
            EventBus.Instance.Raise(new GameEvents.RuleTriggered { type = RuleType.TurnCount, value = 1 });
        }

        private Vector2 CalculateImpulse()
        {
            var dPos = m_startPoint - m_endPoint;
            return Vector2.ClampMagnitude(dPos * m_impulseScale, m_maxImpulse);
        }

        private void OnDrawGizmos()
        {
            if (!m_inputInProgress)
                return;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(m_startPoint, m_endPoint);
        }

        private void OnInputBlocked(GameEvents.InputBlocked e)
        {
            m_inputBlocked = e.on;
        }
    }
}
