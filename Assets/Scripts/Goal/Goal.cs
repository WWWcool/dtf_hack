using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class Goal : MonoBehaviour
    {
        private enum DirectionType
        {
            Top,
            Bottom,
            Any
        }

        [SerializeField] private ObjectDetector m_topDetector = null;
        [SerializeField] private ObjectDetector m_bottomDetector = null;
        [SerializeField] private DirectionType m_directionType = DirectionType.Top;
        [SerializeField] private float m_detectionPeriod = 0.5f;

        public bool m_ballPassed = false;
        private float m_resetDelay = 0.0f;

        private DirectionType? m_pendingDirection = null;
        private bool m_topDetectorTripped = false;
        private bool m_bottomDetectorTripped = false;

        private void UpdatePendingDirection()
        {
            if (m_pendingDirection.HasValue)
                return;

            var direction = GetCurrentDirection();
            if (!direction.HasValue)
                return;

            m_pendingDirection = direction.Value;
        }

        private DirectionType? GetCurrentDirection()
        {
            if (m_topDetectorTripped && !m_bottomDetectorTripped)
                return DirectionType.Top;
            else if (!m_topDetectorTripped && m_bottomDetectorTripped)
                return DirectionType.Bottom;

            return null;
        }

        private void OnBallDetected(Collider2D collider, Vector2 direction)
        {
            m_resetDelay = m_detectionPeriod;
            UpdatePendingDirection();

            if (m_ballPassed)
                return;

            if (m_topDetectorTripped && m_bottomDetectorTripped && m_pendingDirection.HasValue)
                OnBallPassed(m_pendingDirection.Value);
        }

        private void OnBallPassed(DirectionType type)
        {
            ResetDetectors();

            if (m_directionType != DirectionType.Any)
            {
                if (type != m_directionType)
                {
                    Debug.Log("Nah, wrong side noob");
                    return;
                }
            }

            Debug.Log("Ball reached the goal");
            EventBus.Instance.Raise(new GameEvents.RuleTriggered { type = RuleType.GoalCount, value = 1 });
            EventBus.Instance.Raise(new SoundEvents.SoundEvent { type = SoundEvents.SoundType.BallPassedThroughGoal });
            m_ballPassed = true;
        }

        private void Start()
        {
            m_topDetector.onObjectDetected += (Collider2D collider, Vector2 direction) =>
            {
                m_topDetectorTripped = true;
                OnBallDetected(collider, direction);
            };
            m_bottomDetector.onObjectDetected += (Collider2D collider, Vector2 direction) =>
            {
                m_bottomDetectorTripped = true;
                OnBallDetected(collider, direction);
            };
        }

        private void Update()
        {
            m_resetDelay -= Time.deltaTime;
            if (m_resetDelay < 0.0f)
                ResetDetectors();
        }

        private void ResetDetectors()
        {
            m_topDetectorTripped = false;
            m_bottomDetectorTripped = false;
            m_pendingDirection = null;
        }
    }
}
