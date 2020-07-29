using System;
using System.Collections;
using UnityEngine;

namespace CoreSystems.MusicSystem
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioClip))]
    public class MusicTrack : MonoBehaviour, IMusicTrack
    {
        [Tooltip("Id used to reference the music track (multiple tracks can share an Id and be controlled together).")]
        public MusicTrackIdentifier Identifier;

        public MusicTrackIdentifier Id
        {
            get
            {
                return Identifier;
            }
        }

        [Tooltip("Number of seconds to reach max/min volume when started/stopped.")]
        public float FadeTime = 3f;

        [Tooltip("Will start playing the clip when the game starts. (Volume will be zero until faded in)")]
        public bool SynchronisedStart = false;

        private AudioSource audioSource;
        private AudioClip audioClip;

        private float originalVolume;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioClip = GetComponent<AudioClip>();

            originalVolume = audioSource.volume;

            audioSource.volume = 0;
        }

        private void Start()
        {
            if (SynchronisedStart)
            {
                audioSource.Play();
            }
        }

        public void FadeIn(Action callback = null) => FadeIn(originalVolume, FadeTime, callback);
        public void FadeIn(float targetVolume, Action callback = null) => FadeIn(targetVolume, FadeTime, callback);
        public void FadeIn(float targetVolume, float secondsToVolume, Action callback = null) => Fade(true, targetVolume, secondsToVolume, callback);

        public void FadeOut(Action callback = null) => FadeOut(originalVolume, FadeTime, callback);
        public void FadeOut(float targetVolume, Action callback = null) => FadeOut(targetVolume, FadeTime, callback);
        public void FadeOut(float targetVolume, float secondsToVolume, Action callback = null) => Fade(false, targetVolume, secondsToVolume, callback);

        public void Fade(bool fadeIn, float targetVolume, float secondsToVolume, Action callback = null)
        {
            if (fadeIn && !SynchronisedStart)
            {
                audioSource.Play();
            }

            StopAllCoroutines();
            StartCoroutine(FadeTrack(fadeIn, targetVolume, secondsToVolume, callback));
        }

        private IEnumerator FadeTrack(bool fadeIn, float targetVolume, float secondsToVolume, Action callback = null)
        {
            if (fadeIn)
            {
                for (float volume = audioSource.volume; volume < targetVolume; volume += Time.deltaTime / secondsToVolume)
                {
                    audioSource.volume = volume;
                    yield return null;
                }
            }
            else
            {
                for (float volume = audioSource.volume; volume > targetVolume; volume -= Time.deltaTime / secondsToVolume)
                {
                    audioSource.volume = volume;
                    yield return null;
                }
            }

            audioSource.volume = targetVolume;
            callback?.Invoke();
        }
    }
}