using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class MiddleWallLayer : MonoBehaviour
    {
        [SerializeField] private Transform m_target = null;
        [SerializeField] private string m_leftOrder;
        [SerializeField] private string m_rightOrder;

        private void Update()
        {
            var left = m_target.position.x < 0.0f;

            this.GetCachedComponent<SpriteRenderer>().sortingLayerName = left ? m_leftOrder : m_rightOrder;
        }
    }
}
