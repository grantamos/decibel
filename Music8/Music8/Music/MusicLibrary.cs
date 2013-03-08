using Byteopia.Music.GoogleMusicAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music8.Music
{
    public class MusicLibrary
    {
        public int CurrentTotalTracks
        {
            get;
            set;
        }

        public int PagesToLoad
        {
            get;
            set;
        }

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
                    if(albums == null)
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
                    if(songs == null)
                        songs = App.GoogleAPI.Tracks.Where(z => z.AlbumArtist == ArtistName).ToList();

                    return songs;
                }
            }
        }

        List<GoogleMusicSong> baseTracks;
        List<Artist> baseArtist;
        List<Album> baseAlbum;

        public MusicLibrary()
        {
            PagesToLoad = 1;
        }

        public List<GoogleMusicSong> Tracks
        {
            get
            {
                if(baseTracks == null)
                    baseTracks = App.GoogleAPI.Tracks.OrderBy(g => g.Title).ToList(); ;

                return baseTracks;
            }
        }

        public List<Artist> Artists
        {
            get
            {
                if (baseArtist == null)
                    baseArtist = App.GoogleAPI.Tracks.Select(g => new Artist
                    {
                        ArtistName = g.AlbumArtist,
                    }).GroupBy(g => g.ArtistName).Select(g => g.First()).OrderBy(g => g.ArtistName).ToList();

                return baseArtist;
            }
        }

        public List<Album> Albums
        {
            get
            {
                if (baseAlbum == null)

                    baseAlbum = App.GoogleAPI.Tracks.Select(g => new Album
                    {
                        AlbumName = g.Album,
                        ArtistName = g.AlbumArtist,
                        ArtURL = g.ArtURL
                    }).GroupBy(g => g.ArtistName).Select(g => g.First()).OrderBy(g => g.ArtistName).ToList();

                return baseAlbum;
            }
        }

        public async void FetchFullLibrary()
        {
            await App.GoogleAPI.GetUserPlaylists();

            App.GoogleAPI.GetAllSongs(PagesToLoad);
        }
    }
}
