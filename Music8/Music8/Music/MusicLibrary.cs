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

        List<GoogleMusicSong> baseTracks;
        List<Artist> baseArtist;
        List<Album> baseAlbum;

        public MusicLibrary()
        {
            PagesToLoad = 2;
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
