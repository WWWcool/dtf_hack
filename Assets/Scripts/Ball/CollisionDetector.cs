using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityPrototype
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onGroundCollision;
        [SerializeField] private UnityEvent m_onGoalCollision;

        private void OnCollisionEnter2D(Collision2D other)
        {
            ProcessCollision(other.collider);
        }

        private void ProcessCollision(Collider2D other)
        {
            // I hate this code, but who cares?
            if (other.tag == "Ground")
                m_onGroundCollision?.Invoke();
            else if (other.tag == "Goal")
                m_onGoalCollision?.Invoke();
        }
    }
}
