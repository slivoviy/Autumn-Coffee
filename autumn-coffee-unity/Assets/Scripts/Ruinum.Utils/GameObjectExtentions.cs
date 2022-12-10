using UnityEngine;

namespace Ruinum.Utils
{
    public static class GameObjectExtentions
    {
        public static T GetComponentInObject<T>(this GameObject gameObject) where T : Component
        {
            T result = null;
            if (!gameObject) return null;
            if (gameObject.GetComponent<T>())
            {
                result = gameObject.GetComponent<T>();
                return result;
            }

            if (gameObject.GetComponentInChildren<T>())
            {
                result = gameObject.GetComponentInChildren<T>();
                return result;
            }

            if (gameObject.GetComponentInParent<T>())
            {
                result = gameObject.GetComponentInParent<T>();
                return result;
            }

            return null;
        }
    }
}
