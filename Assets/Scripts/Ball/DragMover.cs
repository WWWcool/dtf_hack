using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityPrototype
{
    public class DragMover : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        private Camera m_camera;
        private Vector2 m_offset = Vector2.zero;

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
            m_offset = (Vector2)transform.position - GetEventWorldPosition(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = GetEventWorldPosition(eventData) + m_offset;
        }
    }
}
