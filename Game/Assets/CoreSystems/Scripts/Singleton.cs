using UnityEngine;

namespace CoreSystems.Scripts
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly object MLock = new object();
        private static T _instance;

        public static T Instance
        {
            get
            {
                lock (MLock){
                    if (_instance != null) return _instance;
                    _instance = FindObjectOfType<T>();

                    if (_instance != null) return _instance;

                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T) + " (Singleton)";
                    Debug.Log($"Creating new instance of {typeof(T).Name}");
                    DontDestroyOnLoad(singletonObject);

                    return _instance;
                }
            }
        }
    }
}