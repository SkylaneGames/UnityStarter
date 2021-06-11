using CoreSystems.Music.Scripts;
using CoreSystems.Transition.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class SplashManager : MonoBehaviour
{
    [FormerlySerializedAs("Duration")] [Range(0, 10)]
    public float duration = 2f;

    private float _timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        _timeRemaining = duration;
    }

    // Update is called once per frame
    void Update()
    {
        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0)
        {
            MusicManager.Instance.FadeIn(MusicTrackIdentifier.MainTrack);
            LevelLoader.Instance.LoadNextLevel();
        }
    }
}
