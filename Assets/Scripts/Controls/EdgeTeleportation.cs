using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class EdgeTeleportation : MonoBehaviour
    {
        [SerializeField] private bool m_canBeEdgeTeleported = false;
        private bool m_teleportReset = true;

        private void Update()
        {
            if (m_canBeEdgeTeleported&&m_teleportReset)
            {
                if (transform.position.x>9.4f || transform.position.x < -9.4f)
                {
                    transform.position = new Vector2(-transform.position.x, transform.position.y);
                    StartCoroutine(TeleportResetCorutine());
                } 
            }
        }

        private IEnumerator TeleportResetCorutine()
        {
            m_teleportReset = false;
            yield return new WaitForSeconds(0.2f);
            m_teleportReset = true;
        }
    }
}
