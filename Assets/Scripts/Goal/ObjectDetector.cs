using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class ObjectDetector : MonoBehaviour
    {
        public event System.Action<Collider2D, Vector2> onObjectDetected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var posDelta = transform.position - other.transform.position;
            onObjectDetected?.Invoke(other, posDelta.normalized);
        }
    }
}
