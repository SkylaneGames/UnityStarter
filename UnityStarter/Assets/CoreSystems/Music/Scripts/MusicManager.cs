using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSystems.MusicSystem
{
    public class MusicManager : Singleton<MusicManager>
    {
        private IEnumerable<IMusicTrack> tracks;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            tracks = GetComponentsInChildren<IMusicTrack>();
        }

        public void FadeIn(MusicTrackIdentifier id, Action callback = null)
        {
            var selectedTracks = tracks.Where(p => p.Id == id);

            foreach (var track in selectedTracks)
            {
                track.FadeIn(callback);

                // So it is only called once.
                callback = null;
            }
        }

        public void FadeOut(MusicTrackIdentifier id, Action callback = null)
        {
            var selectedTracks = tracks.Where(p => p.Id == id);

            foreach (var track in selectedTracks)
            {
                track.FadeOut(callback);

                // So it is only called once.
                callback = null;
            }
        }
    }
}