using Byteopia.Music.GoogleMusicAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music8.Music
{
    public class Album
    {
        public String ArtistName { get; set; }
        public String AlbumName { get; set; }
        public String ArtURL { get; set; }

        private List<GoogleMusicSong> songs = null;

        public TimeSpan Duration
        {
            get
            {
                return TimeSpan.FromMilliseconds(Songs.Sum(g => g.Duration));
            }
            set
            {

            }
        }


        public List<GoogleMusicSong> Songs
        {
            get
            {
                if (songs == null)
                    songs = App.GoogleAPI.Tracks.Where(z => z.AlbumArtist == ArtistName && z.Album == AlbumName).ToList();

                return songs;
            }
        }
    }
}
