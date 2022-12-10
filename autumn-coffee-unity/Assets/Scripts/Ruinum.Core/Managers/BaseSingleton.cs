using UnityEngine;


namespace Ruinum.Core
{
    public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Singleton { get; private set; }
        protected virtual void Awake()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Singleton = this as T;
            }
        }
    }
}

