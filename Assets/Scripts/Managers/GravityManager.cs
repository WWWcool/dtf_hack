using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class GravityManager : MonoBehaviour
    {
        [SerializeField] private bool m_overrideGravity = false;
        [SerializeField] private Vector2 m_defaultGravity = Vector2.zero;

        private Vector2 m_originalGravity;
        public Vector2 originalGravity => m_originalGravity;

        private void Start()
        {
            m_originalGravity = Physics2D.gravity;

            if (m_overrideGravity)
                Physics2D.gravity = m_defaultGravity;
        }
    }
}
