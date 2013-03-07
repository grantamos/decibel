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
        public ObservableCollection<GoogleMusicSong> songs;
        public ObservableCollection<Album> albums = new ObservableCollection<Album>();
        public ObservableCollection<Artist> artists = new ObservableCollection<Artist>();

        public delegate void NotifyCollectionChanged(ObservableCollection<GoogleMusicSong> songs);
        public event NotifyCollectionChanged CollectionChanged;

        public Collection(ObservableCollection<GoogleMusicSong> songs)
        {
            this.songs = songs;
            this.songs.CollectionChanged += songs_CollectionChanged;

            var albumList = this.GetAlbums();
            foreach (var item in albumList)
                this.albums.Add(new Album(item));

            var artistList = this.GetArtists();
            foreach (var item in artistList)
                this.artists.Add(new Artist(item));
        }

        private void songs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(CollectionChanged != null)
                CollectionChanged.Invoke(this.songs);

            foreach (GoogleMusicSong item in e.NewItems)
            {
                this.InsertIntoAlbums(new Album(item));
                this.InsertIntoArtists(new Artist(item));
            }
        }

        public List<GoogleMusicSong> GetAlbumSongs(GoogleMusicSong song)
        {
            return songs.Where(s => s.ArtistAlbum == song.ArtistAlbum).OrderBy(s => s.Track).ToList();
        }

        public List<GoogleMusicSong> GetArtistSongs(GoogleMusicSong song)
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

        public IEnumerable<IGrouping <string, GoogleMusicSong>> GetAlbums(GoogleMusicSong song)
        {
            return songs.Where(s => s.AlbumArtist == song.AlbumArtist)
                .OrderBy(s => s.Album)
                .GroupBy(s => s.Album);
        }

        public List<GoogleMusicSong> GetSongs()
        {
            return songs.OrderBy(g => g.Title).ToList();
        }

        public void InsertIntoAlbums(Album album)
        {
            int max = this.albums.Count();
            int min = 1;

            if (max == 0)
            {
                this.albums.Add(album);
                return;
            }

            int mid = 0, compareValue = 0;
            while (min <= max)
            {
                mid = (max - min) / 2 + min;
                compareValue = this.albums.ElementAt(mid-1).AlbumName.CompareTo(album.AlbumName);
                if (compareValue < 0)
                    min = mid + 1;
                else if (compareValue > 0)
                    max = mid - 1;
                else
                    return;
            }

            if (compareValue < 0)
                this.albums.Insert(mid, album);
            else
                this.albums.Insert(mid - 1, album);
            
            return;
        }

        public void InsertIntoArtists(Artist artist)
        {
            int max = this.artists.Count();
            int min = 1;

            if (max == 0)
            {
                this.artists.Add(artist);
                return;
            }

            int mid = 0, compareValue = 0;
            while (min <= max)
            {
                mid = (max - min) / 2 + min;
                compareValue = this.artists.ElementAt(mid - 1).ArtistName.CompareTo(artist.ArtistName);
                if (compareValue < 0)
                    min = mid + 1;
                else if (compareValue > 0)
                    max = mid - 1;
                else
                    return;
            }

            if(compareValue < 0)
                this.artists.Insert(mid, artist);
            else
                this.artists.Insert(mid - 1, artist);
            return;
        }
    }

    public class Artist
    {
        public String ArtistName { get; set; }
        public String Details { get; set; }
        public GoogleMusicSong song { get; set; }
        public List<Album> albums = new List<Album>();
        public List<GoogleMusicSong> songs = new List<GoogleMusicSong>();

        public Artist(GoogleMusicSong song)
        {
            this.song = song;
            this.ArtistName = song.Artist;
            this.songs = App.Collection.GetArtistSongs(song);
            
            //var albumList = App.collection.albums.Where(album => album.ArtistName == song.Artist);

            TimeSpan duration = new TimeSpan();
            foreach (GoogleMusicSong songItem in this.songs)
                duration += TimeSpan.FromMilliseconds(songItem.Duration);

            this.Details = this.songs.Count + " Song" + (this.songs.Count > 1 ? "s" : "") + ", " + duration;
        }
    }

    public class Album
    {
        public String ArtistName { get; set; }
        public String AlbumName { get; set; }
        public String Details { get; set; }
        public GoogleMusicSong song { get; set; }
        public List<GoogleMusicSong> songs;

        public Album(GoogleMusicSong song)
        {
            this.song = song;
            this.ArtistName = song.Artist;
            this.AlbumName = song.Album;
            this.songs = App.Collection.GetAlbumSongs(song);

            TimeSpan duration = new TimeSpan();
            foreach(GoogleMusicSong songItem in this.songs)
                duration += TimeSpan.FromMilliseconds(songItem.Duration);

            this.Details = this.songs.Count + " Song" + (this.songs.Count > 1 ? "s" : "") + ", " + duration;
        }
    }

    public class GoogleMusicSongInstance
    {
        public GoogleMusicSong song { get; set; }

        public GoogleMusicSongInstance(GoogleMusicSong song)
        {
            this.song = song;
        }
    }
}
