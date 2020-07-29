using UnityEngine;

namespace CoreSystems
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool shuttingDown = false;
        private static object m_Lock = new object();
        private static T instance;

        public static T Instance
        {
            get
            {
                lock (m_Lock){
                    if (instance == null){
                        instance = FindObjectOfType<T>();

                        if (instance == null)
                        {
                            var singletonObject = new GameObject();
                            instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";
                            Debug.Log($"Creating new instance of {typeof(T).Name}");
                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return instance;
                }
            }
        }

        private void OnApplicationQuit() {
            shuttingDown = true;
        }

        private void OnDestroy() {
            shuttingDown = true;
        }
    }
}