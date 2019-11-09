using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public static class GameObjectExtensions
    {
        private enum ComponentSearchMode
        {
            Self,
            Parent,
            Children,
        }

        private static Dictionary<GameObject, Dictionary<System.Type, Component>> s_selfCache = new Dictionary<GameObject, Dictionary<System.Type, Component>>();
        private static Dictionary<GameObject, Dictionary<System.Type, Component>> s_parentCache = new Dictionary<GameObject, Dictionary<System.Type, Component>>();
        private static Dictionary<GameObject, Dictionary<System.Type, Component>> s_childrenCache = new Dictionary<GameObject, Dictionary<System.Type, Component>>();

        public static T GetCachedComponent<T>(this GameObject obj, bool throwIfNotFound = true) where T : Component
        {
            return GetCachedComponentInternal<T>(obj, throwIfNotFound, ComponentSearchMode.Self);
        }

        public static T GetCachedComponentInChildren<T>(this GameObject obj, bool throwIfNotFound = true) where T : Component
        {
            return GetCachedComponentInternal<T>(obj, throwIfNotFound, ComponentSearchMode.Children);
        }

        public static T GetCachedComponentInParent<T>(this GameObject obj, bool throwIfNotFound = true) where T : Component
        {
            return GetCachedComponentInternal<T>(obj, throwIfNotFound, ComponentSearchMode.Parent);
        }

        private static T GetCachedComponentInternal<T>(GameObject obj, bool throwIfNotFound, ComponentSearchMode mode) where T : Component
        {
            var cache = GetCache(mode);

            Dictionary<System.Type, Component> behaviourCache;

            if (!cache.TryGetValue(obj, out behaviourCache))
            {
                behaviourCache = new Dictionary<System.Type, Component>();
                cache[obj] = behaviourCache;
            }

            Component comp;

            if (!behaviourCache.TryGetValue(typeof(T), out comp))
            {
                comp = FindComponent<T>(obj, mode);

                if (comp == null && throwIfNotFound)
                    throw new MissingComponentException($"Component of type '{typeof(T).Name}' not found");

                behaviourCache[typeof(T)] = comp;
            }

            return comp as T;
        }

        private static T FindComponent<T>(GameObject obj, ComponentSearchMode mode) where T : Component
        {
            switch (mode)
            {
                case ComponentSearchMode.Self:
                    return obj.GetComponent<T>();

                case ComponentSearchMode.Parent:
                    return obj.GetComponentInParent<T>();

                case ComponentSearchMode.Children:
                    return obj.GetComponentInChildren<T>();
            }

            Debug.Assert(false);
            return null;
        }

        private static Dictionary<GameObject, Dictionary<System.Type, Component>> GetCache(ComponentSearchMode mode)
        {
            switch (mode)
            {
                case ComponentSearchMode.Self:
                    return s_selfCache;

                case ComponentSearchMode.Parent:
                    return s_parentCache;

                case ComponentSearchMode.Children:
                    return s_childrenCache;
            }

            Debug.Assert(false);
            return null;
        }

        public static T GetRequiredComponent<T>(this GameObject obj) where T : Component
        {
            var component = obj.GetComponent<T>();

            if (component == null)
                Debug.LogError("Expected to find component of type " + typeof(T) + " but found none", obj);

            return component;
        }

        public static void SetLayerRecursively(this GameObject obj, int layer, string ignoreTag = "")
        {
            if (ignoreTag != "" && obj.tag == ignoreTag)
                return;

            obj.layer = layer;

            var transform = obj.transform;

            for (var i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetLayerRecursively(layer, ignoreTag);
        }
    }
}
