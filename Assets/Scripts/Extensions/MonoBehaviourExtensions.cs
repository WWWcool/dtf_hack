using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public static class MonoBehaviourExtensions
    {
        public static T GetCachedComponent<T>(this MonoBehaviour behaviour, bool throwIfNotFound = true) where T : Component
        {
            return behaviour.gameObject.GetCachedComponent<T>(throwIfNotFound);
        }

        public static T GetCachedComponentInChildren<T>(this MonoBehaviour behaviour, bool throwIfNotFound = true) where T : Component
        {
            return behaviour.gameObject.GetCachedComponentInChildren<T>(throwIfNotFound);
        }

        public static T GetCachedComponentInParent<T>(this MonoBehaviour behaviour, bool throwIfNotFound = true) where T : Component
        {
            return behaviour.gameObject.GetCachedComponentInParent<T>(throwIfNotFound);
        }
    }
}
