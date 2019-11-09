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
            m_startPoint = GetEventWorldPosition(eventData);
            m_inputInProgress = true;

            EventBus.Instance.Raise(new GameEvents.InputStarted());
        }

        public void OnDrag(PointerEventData eventData)
        {
            m_endPoint = GetEventWorldPosition(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            m_endPoint = GetEventWorldPosition(eventData);
            m_inputInProgress = false;

            var dPos = m_startPoint - m_endPoint;
            var impulse = Vector2.ClampMagnitude(dPos * m_impulseScale, m_maxImpulse);

            EventBus.Instance.Raise(new GameEvents.InputFinished { impulse = impulse });
        }

        private void OnDrawGizmos()
        {
            if (!m_inputInProgress)
                return;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(m_startPoint, m_endPoint);
        }
    }
}
