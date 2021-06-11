using System;

namespace CoreSystems.Music.Scripts.Interfaces
{
    public interface IMusicTrack
    {
        MusicTrackIdentifier Id { get; }

        void FadeIn(Action callback = null);
        void FadeIn(float targetVolume, Action callback = null);
        void FadeIn(float targetVolume, float secondsToVolume, Action callback = null);

        void FadeOut(Action callback = null);
        void FadeOut(float targetVolume, Action callback = null);
        void FadeOut(float targetVolume, float secondsToVolume, Action callback = null);
    }
}
