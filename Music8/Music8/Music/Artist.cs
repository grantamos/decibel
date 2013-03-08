using Byteopia.Music.GoogleMusicAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music8.Music
{
    public class Artist
    {
        public String ArtistName { get; set; }
        private List<GoogleMusicSong> songs = null;
        List<Album> albums = null;

        public String Details
        {
            get
            {
                return this.Songs.Count + " Song" + (this.Songs.Count > 1 ? "s" : "") + ", " + Duration;
            }
            set
            {

            }
        }

        public List<Album> Albums
        {
            get
            {
                if (albums == null)
                    albums = App.GoogleAPI.Tracks.Where(z => z.AlbumArtist == this.ArtistName).Select(g => new Album
                    {
                        AlbumName = g.Album,
                        ArtistName = g.AlbumArtist,
                        ArtURL = g.ArtURL
                    }).GroupBy(g => g.AlbumName).Select(g => g.First()).ToList();

                return albums;
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
                    songs = App.GoogleAPI.Tracks.Where(z => z.AlbumArtist == ArtistName).ToList();

                return songs;
            }
        }

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
    }

}
