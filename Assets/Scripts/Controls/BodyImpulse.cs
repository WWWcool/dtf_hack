using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class BodyImpulse : MonoBehaviour
    {
        public void ApplyImpulse(Vector2 impulse)
        {
            this.GetCachedComponent<Rigidbody2D>().AddForce(impulse, ForceMode2D.Impulse);
        }
    }
}
