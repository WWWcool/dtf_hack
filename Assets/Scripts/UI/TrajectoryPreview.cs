using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class TrajectoryPreview : MonoBehaviour
    {
        [SerializeField] LineRenderer m_line;
        [SerializeField] float m_maxTime;
        [SerializeField] float m_timeStep;

        private GameObject m_target => GetTarget();
        
        private bool m_inputActive = false;
        private Vector2 m_impulse = Vector2.zero;

        private GameObject m_cachedTarget;
        private GameObject GetTarget()
        {
            if (m_cachedTarget == null)
                m_cachedTarget = FindTarget();
            return m_cachedTarget;
        }

        private GameObject FindTarget()
        {
            return FindObjectOfType<BodyImpulse>()?.gameObject;
        }

        private void OnEnable()
        {
            EventBus.Instance.AddListener<GameEvents.InputStarted>(OnInputStarted);
            EventBus.Instance.AddListener<GameEvents.InputUpdated>(OnInputUpdated);
            EventBus.Instance.AddListener<GameEvents.InputFinished>(OnInputFinished);
        }

        private void OnDisable()
        {
            EventBus.Instance.RemoveListener<GameEvents.InputStarted>(OnInputStarted);
            EventBus.Instance.RemoveListener<GameEvents.InputUpdated>(OnInputUpdated);
            EventBus.Instance.RemoveListener<GameEvents.InputFinished>(OnInputFinished);
        }

        private void OnInputStarted(GameEvents.InputStarted e)
        {
            m_line.gameObject.SetActive(true);
            m_inputActive = true;
        }

        private void OnInputUpdated(GameEvents.InputUpdated e)
        {
            m_impulse = e.impulse;
            UpdatePreview();
        }

        private void OnInputFinished(GameEvents.InputFinished e)
        {
            m_line.gameObject.SetActive(false);
            m_inputActive = false;
        }

        private void Update() {
            if (m_inputActive)
                UpdatePreview();
        }

        private void UpdatePreview()
        {
            var targetBody = m_target.GetCachedComponent<Rigidbody2D>();
            var velocity = m_impulse / targetBody.mass;
            var gravity = ServiceLocator.Get<GravityManager>().originalGravity;
            var points = CalculateTrajectory(targetBody.transform.position, velocity, gravity);

            m_line.positionCount = points.Count;
            m_line.SetPositions(points.ToArray());
        }

        private List<Vector3> CalculateTrajectory(Vector2 position, Vector2 velocity, Vector2 acceleration)
        {
            var points = new List<Vector3>();

            var time = 0.0f;
            while (time <= m_maxTime)
            {
                var p = position + velocity * time + 0.5f * acceleration * time * time;
                points.Add(p);

                time += m_timeStep;
            }

            return points;
        }
    }
}
