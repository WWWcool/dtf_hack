using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class BodyImpulse : MonoBehaviour
    {
        [SerializeField] private bool m_resetVelocity = false;

        public void ApplyImpulse(Vector2 impulse)
        {
            var body = this.GetCachedComponent<Rigidbody2D>();

            if (m_resetVelocity)
                body.velocity = Vector2.zero;
            body.AddForce(impulse, ForceMode2D.Impulse);

            EventBus.Instance.Raise(new GameEvents.ImpulseGiven());
        }
    }
}
