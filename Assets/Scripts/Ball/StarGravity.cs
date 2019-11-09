using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class StarGravity : MonoBehaviour
    {
        [SerializeField] private GameObject m_star;
        [SerializeField] private float m_strength = 1.0f;

        private void FixedUpdate()
        {
            if (m_star == null)
                return;

            var body = this.GetCachedComponent<Rigidbody2D>();
            var dPos = m_star.transform.position - transform.position;
            var d2 = dPos.sqrMagnitude;

            var force = m_strength * body.mass / d2 * dPos.normalized * body.gravityScale;

            body.AddForce(force);
        }
    }
}
