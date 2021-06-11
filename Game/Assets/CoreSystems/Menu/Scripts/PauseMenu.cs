using CoreSystems.Transition.Scripts;
using UnityEngine;

namespace CoreSystems.Menu.Scripts
{
    public class PauseMenu : MonoBehaviour, IPauseMenu
    {
        public bool IsPaused { get; private set; }

        void Start()
        {
            UnpauseGame();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            IsPaused = true;
            gameObject.SetActive(true);
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1;
            IsPaused = false;
            gameObject.SetActive(false);
        }

        public void Back()
        {
            UnpauseGame();
            LevelLoader.Instance.LoadLevel(Level.Menu);
        }
    }
}
