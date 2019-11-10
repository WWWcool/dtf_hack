using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_states;
        [SerializeField] private float m_delay = 1.0f;

        private int m_index = 0;

        public void SwitchState()
        {
            m_index++;
            UpdateState();
        }

        private void Start()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            for (var i = 0; i < m_states.Length; i++)
            {
                var enabled = i == m_index;
                m_states[i].SetActive(enabled);
            }
        }

        public void OnImpulseGiven()
        {
            StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation()
        {
            SwitchState();
            yield return new WaitForSeconds(m_delay);
            SwitchState();
        }
    }
}
