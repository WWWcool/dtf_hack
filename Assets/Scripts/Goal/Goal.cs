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

        [SerializeField] private ObjectDetector m_detector = null;
        [SerializeField] private DirectionType m_directionType = DirectionType.Top;

        private bool m_ballPassed = false;

        private void OnBallDetected(Collider2D collider, Vector2 direction)
        {
            if (m_ballPassed)
                return;

            var directionType = direction.y > 0.0f ? DirectionType.Bottom : DirectionType.Top;
            OnBallPassed(directionType);
        }

        private void OnBallPassed(DirectionType type)
        {
            if (m_directionType != DirectionType.Any)
            {
                if (type != m_directionType)
                    return;
            }

            Debug.Log("Ball reached the goal");
            EventBus.Instance.Raise(new GameEvents.RuleTriggered { type = RuleType.GoalCount, value = 1 });
            m_ballPassed = true;
        }

        private void Start()
        {
            m_detector.onObjectDetected += OnBallDetected;
        }
    }
}
