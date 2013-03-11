using Byteopia.Music.GoogleMusicAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music8.Music
{
    public class NowPlayingQueue
    {
        List<GoogleMusicSong> queue;

        public List<GoogleMusicSong> Songs
        {
            get { return queue; }
            set { queue = value; }
        }
        int index;

        public int CurrentIndex
        {
            get { return index; }
            set { index = value; }
        }

        public GoogleMusicSong CurrentSong
        {
            get 
            {
                if (queue == null || queue.Count == 0)
                    return null;

                return queue[index]; 
            }
        }

        public NowPlayingQueue()
        {
            queue = new List<GoogleMusicSong>();
            index = 0;
        }

        public void Add(GoogleMusicSong song)
        {
            queue.Add(song);
        }

        public void Add(List<GoogleMusicSong> songs)
        {
            foreach (GoogleMusicSong song in songs)
                queue.Add(song);
        }

        public void Add(Artist artist)
        {
            foreach (Album album in artist.Albums)
                this.Add(album);
        }

        public void Add(Album album)
        {
            foreach (GoogleMusicSong song in album.Songs)
                queue.Add(song);
        }

        public GoogleMusicSong NextTrack()
        {
            if (index == queue.Count - 1)
                index = 0;
            else
                index++;

            return queue[index];
        }

        public GoogleMusicSong PrevTrack()
        {
            if (index == 0)
                index = queue.Count - 1;
            else
                index--;

            return queue[index];
        }
    }
}
