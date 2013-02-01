using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Byteopia.Music.GoogleMusicAPI;
using System.Collections.ObjectModel;

namespace Music8.Common
{
    public class Collection
    {
        ObservableCollection<GoogleMusicSong> songs;

        public delegate void NotifyCollectionChanged(ObservableCollection<GoogleMusicSong> songs);
        public event NotifyCollectionChanged CollectionChanged;

        public Collection(ObservableCollection<GoogleMusicSong> songs)
        {
            this.songs = songs;
            this.songs.CollectionChanged += songs_CollectionChanged;
        }

        private void songs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(CollectionChanged != null)
                CollectionChanged.Invoke(this.songs);
        }

        public List<GoogleMusicSong> GetAlbum(GoogleMusicSong song)
        {
            return songs.Where(s => s.ArtistAlbum == song.ArtistAlbum).OrderBy(s => s.Track).ToList();
        }

        public List<GoogleMusicSong> GetArtist(GoogleMusicSong song)
        {
            return songs.Where(s => s.Artist == song.Artist).OrderBy(s => s.AlbumArtist).ThenBy(s => s.Track).ToList();
        }

        public List<GoogleMusicSong> GetArtists()
        {
            return songs.GroupBy(song => song.Artist)
                .Select(g => g.First())
                .OrderBy(g => g.AlbumArtist)
                .ToList();
        }

        public List<GoogleMusicSong> GetAlbums()
        {
            return songs.GroupBy(song => song.Album)
                .Select(g => g.First())
                .OrderBy(g => g.Album)
                .ToList();
        }

        public List<GoogleMusicSong> GetSongs()
        {
            return songs.ToList();
        }
    }
}
