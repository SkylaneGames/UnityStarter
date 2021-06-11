using System;
using System.Collections;
using CoreSystems.Music.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace CoreSystems.Music.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioClip))]
    public class MusicTrack : MonoBehaviour, IMusicTrack
    {
        [FormerlySerializedAs("Identifier")] [Tooltip("Id used to reference the music track (multiple tracks can share an Id and be controlled together).")]
        public MusicTrackIdentifier identifier;

        public MusicTrackIdentifier Id => identifier;

        [FormerlySerializedAs("FadeTime")] [Tooltip("Number of seconds to reach max/min volume when started/stopped.")]
        public float fadeTime = 3f;

        [FormerlySerializedAs("SynchronisedStart")] [Tooltip("Will start playing the clip when the game starts. (Volume will be zero until faded in)")]
        public bool synchronisedStart;

        private AudioSource _audioSource;

        private float _originalVolume;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            _originalVolume = _audioSource.volume;

            _audioSource.volume = 0;
        }

        private void Start()
        {
            if (synchronisedStart)
            {
                _audioSource.Play();
            }
        }

        public void FadeIn(Action callback = null) => FadeIn(_originalVolume, fadeTime, callback);
        public void FadeIn(float targetVolume, Action callback = null) => FadeIn(targetVolume, fadeTime, callback);
        public void FadeIn(float targetVolume, float secondsToVolume, Action callback = null) => Fade(true, targetVolume, secondsToVolume, callback);

        public void FadeOut(Action callback = null) => FadeOut(_originalVolume, fadeTime, callback);
        public void FadeOut(float targetVolume, Action callback = null) => FadeOut(targetVolume, fadeTime, callback);
        public void FadeOut(float targetVolume, float secondsToVolume, Action callback = null) => Fade(false, targetVolume, secondsToVolume, callback);

        public void Fade(bool fadeIn, float targetVolume, float secondsToVolume, Action callback = null)
        {
            if (fadeIn && !synchronisedStart)
            {
                _audioSource.Play();
            }

            StopAllCoroutines();
            StartCoroutine(FadeTrack(fadeIn, targetVolume, secondsToVolume, callback));
        }

        private IEnumerator FadeTrack(bool fadeIn, float targetVolume, float secondsToVolume, Action callback = null)
        {
            if (fadeIn)
            {
                for (float volume = _audioSource.volume; volume < targetVolume; volume += Time.deltaTime / secondsToVolume)
                {
                    _audioSource.volume = volume;
                    yield return null;
                }
            }
            else
            {
                for (float volume = _audioSource.volume; volume > targetVolume; volume -= Time.deltaTime / secondsToVolume)
                {
                    _audioSource.volume = volume;
                    yield return null;
                }
            }

            _audioSource.volume = targetVolume;
            callback?.Invoke();
        }
    }
}