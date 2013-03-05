using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Byteopia.Music.GoogleMusicAPI;

namespace Music8.Common
{
    class Library
    {
        private API googleAPI;
        private List<GoogleMusicSong> queue = new List<GoogleMusicSong>();

        public Library(API googleAPI)
        {
            this.googleAPI = googleAPI;
        }

        public void Enqueue(GoogleMusicSong song)
        {
            queue.Add(song);
        }

        public void Dequeue(GoogleMusicSong song)
        {
            queue.Remove(song);
        }
    }
}
