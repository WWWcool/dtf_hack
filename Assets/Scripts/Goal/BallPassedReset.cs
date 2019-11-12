using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class BallPassedReset : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ball")
            {
                ServiceLocator.Get<Goal>().m_ballPassed = false;
            }
        }
    }
}