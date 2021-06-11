using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreSystems.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CoreSystems.Transition.Scripts
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

        private Transition _currentTransition;
        private IEnumerable<Transition> _transitions;

        public bool LoadingLevel { get; private set; }

        void Awake()
        {
            _transitions = GetComponentsInChildren<Transition>();
            SetTransition(transitionIn);
        }

        private void SetTransition(TransitionType type)
        {
            _currentTransition = _transitions.FirstOrDefault(p => p.transitionType == type);
            var disabledTransitions = _transitions.Where(p => p != _currentTransition);

            if (_currentTransition == null)
            {
                Debug.LogError($"Transition '{type.ToString()}' does not exist.");
            }
            else
            {
                _currentTransition.gameObject.SetActive(true);
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

            _currentTransition.TransitionOut();

            yield return new WaitForSeconds(_currentTransition.transitionTime);

            LoadingLevel = false;
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
