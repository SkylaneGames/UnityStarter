using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CoreSystems.TransitionSystem
{
    public enum TransitionType
    {
        Fade,
        HorizontalSwipe,
        CircleZoom,
        CircleSwipe
    }

    public enum Level
    {
        Splash,
        Menu,
        Game
    }

    public class LevelLoader : Singleton<LevelLoader>
    {
        public TransitionType transitionIn;
        public TransitionType transitionOut;

        private Transition currentTransition;
        private IEnumerable<Transition> transitions;

        public bool LoadingLevel { get; private set; }

        void Awake()
        {
            transitions = GetComponentsInChildren<Transition>();
            SetTransition(transitionIn);
        }

        private void SetTransition(TransitionType type)
        {
            currentTransition = transitions.FirstOrDefault(p => p.TransitionType == type);
            var disabledTransitions = transitions.Where(p => p != currentTransition);

            if (currentTransition == null)
            {
                Debug.LogError($"Transition '{type.ToString()}' does not exist.");
            }
            else
            {
                currentTransition.gameObject.SetActive(true);
            }            
            
            disabledTransitions.ToList().ForEach(p => p.gameObject.SetActive(false));
        }

        public void LoadNextLevel()
        {
            LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // Start is called before the first frame update
        public void LoadLevel(int sceneBuildIndex)
        {
            StartCoroutine(LoadLevelRoutine(sceneBuildIndex));
        }

        public void LoadLevel(Level level)
        {
            LoadLevel((int)level);
        }

        IEnumerator LoadLevelRoutine(int sceneBuildIndex)
        {
            LoadingLevel = true;
            SetTransition(transitionOut);

            currentTransition.TransitionOut();

            yield return new WaitForSeconds(currentTransition.TransitionTime);

            LoadingLevel = false;
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
