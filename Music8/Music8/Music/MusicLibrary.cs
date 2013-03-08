using Byteopia.Music.GoogleMusicAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

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

        List<GoogleMusicSong> baseTracks;
        List<Artist> baseArtist;
        List<Album> baseAlbum;
        NowPlayingQueue queue;
        private MediaElement mediaElement;

        public MediaElement MediaElement
        {
            get { return mediaElement; }
            set { mediaElement = value; }
        }

        public MusicLibrary()
        {
            PagesToLoad = 2;

            queue = new NowPlayingQueue();
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

        public bool Shuffle
        {
            get;
            set;
        }

        public enum REPEAT_MODE : int
        {
            NONE = 0,
            ONCE = 1,
            ALL = 1
        };

        public REPEAT_MODE Repeat
        {
            get;
            set;
        }

        public NowPlayingQueue Queue
        {
            get
            {
                return queue;
            }
            set
            {
                queue = value;
            }
        }

        public async Task<bool> FetchFullLibrary()
        {
            await App.GoogleAPI.GetUserPlaylists();

            App.GoogleAPI.GetAllSongs(PagesToLoad);

            return true;
        }

        public void NextTrack()
        {
            PlaySong(Queue.NextTrack());
        }

        public void PrevTrack()
        {
            PlaySong(Queue.PrevTrack());
        }

        public async void PlaySong(GoogleMusicSong song)
        {
            Queue.Add(song);
            String streamURL = await App.GoogleAPI.GetStreamURL(song);
            if (!streamURL.Equals(string.Empty) && App.GoogleAPI.Client.LastStatusCode != System.Net.HttpStatusCode.Forbidden)
            {
                mediaElement.DataContext = song;
                mediaElement.Source = new Uri(streamURL);
            }
        }
    }
}
