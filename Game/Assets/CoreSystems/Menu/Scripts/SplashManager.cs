using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreSystems.MusicSystem;
using CoreSystems.TransitionSystem;

public class SplashManager : MonoBehaviour
{
    [Range(0, 10)]
    public float Duration = 2f;

    private float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = Duration;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            MusicManager.Instance.FadeIn(MusicTrackIdentifier.MainTrack);
            LevelLoader.Instance.LoadNextLevel();
        }
    }
}
