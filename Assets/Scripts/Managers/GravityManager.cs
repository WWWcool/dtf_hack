using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class GravityManager : MonoBehaviour
    {
        [SerializeField] private bool m_overrideGravity = false;
        [SerializeField] private Vector2 m_defaultGravity = Vector2.zero;

        private void Start()
        {
            if (m_overrideGravity)
                Physics2D.gravity = m_defaultGravity;
        }
    }
}
