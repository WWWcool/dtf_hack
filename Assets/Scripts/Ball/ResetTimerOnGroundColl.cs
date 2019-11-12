using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimerOnGroundColl : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            ServiceLocator.Get<TimerManager>().TimerReset();
        }
    }
}
