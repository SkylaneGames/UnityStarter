using System;
using System.Collections.Generic;
using System.Linq;
using CoreSystems.Music.Scripts.Interfaces;
using CoreSystems.Scripts;

namespace CoreSystems.Music.Scripts
{
    public class MusicManager : Singleton<MusicManager>
    {
        private IEnumerable<IMusicTrack> _tracks;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _tracks = GetComponentsInChildren<IMusicTrack>();
        }

        public void FadeIn(MusicTrackIdentifier id, Action callback = null)
        {
            var selectedTracks = _tracks.Where(p => p.Id == id);

            foreach (var track in selectedTracks)
            {
                track.FadeIn(callback);

                // So it is only called once.
                callback = null;
            }
        }

        public void FadeOut(MusicTrackIdentifier id, Action callback = null)
        {
            var selectedTracks = _tracks.Where(p => p.Id == id);

            foreach (var track in selectedTracks)
            {
                track.FadeOut(callback);

                // So it is only called once.
                callback = null;
            }
        }
    }
}