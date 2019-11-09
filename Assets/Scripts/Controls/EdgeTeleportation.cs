using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class EdgeTeleportation : MonoBehaviour
    {
        [SerializeField] private bool m_canBeEdgeTeleported = false;

        private void Update()
        {
            if (m_canBeEdgeTeleported)
            {
                if (transform.position.x>9.4f || transform.position.x < -9.4f)
                    transform.position = new Vector2(-transform.position.x,
                        transform.position.y);
            }
        }
    }
}
