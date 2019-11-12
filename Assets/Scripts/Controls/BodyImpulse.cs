using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class BodyImpulse : MonoBehaviour
    {
        [SerializeField] private float m_torque = 0.0f;
        [SerializeField] private bool m_resetVelocity = false;

        private void OnEnable()
        {
            EventBus.Instance.AddListener<GameEvents.InputFinished>(OnInputFinished);
        }

        private void OnDisable()
        {
            EventBus.Instance.RemoveListener<GameEvents.InputFinished>(OnInputFinished);
        }

        private void OnInputFinished(GameEvents.InputFinished e)
        {
            ApplyImpulse(e.impulse);
        }

        public void ApplyImpulse(Vector2 impulse)
        {
            var body = this.GetCachedComponent<Rigidbody2D>();

            if (m_resetVelocity)
                body.velocity = Vector2.zero;
            body.AddForce(impulse, ForceMode2D.Impulse);

            body.AddTorque(m_torque);

            EventBus.Instance.Raise(new GameEvents.ImpulseGiven());
        }
    }
}
