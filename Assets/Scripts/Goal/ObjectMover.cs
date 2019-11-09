using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class ObjectMover : MonoBehaviour
    {
        [SerializeField] private float m_speed = 0.0f;
        [SerializeField] private GameObject m_target = null;

        private bool m_moving = false;

        public void StartMovement()
        {
            m_moving = true;
        }

        private void FixedUpdate()
        {
            if (!m_moving)
                return;

            transform.position = Vector2.MoveTowards(transform.position, m_target.transform.position, m_speed * Time.fixedDeltaTime);
        }
    }
}
