using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Byteopia.Music.GoogleMusicAPI;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;

namespace Music8.Common
{
    public class SongQueue
    {
        public int currentIndex = -1;

        private ObservableCollection<GoogleMusicSongInstance> queue = new ObservableCollection<GoogleMusicSongInstance>();
        private bool shuffle = false;
        private bool repeat = false;
        private MediaElement mediaElement;

        public delegate void NotifySongChanged(GoogleMusicSongInstance song, int index);
        public event NotifySongChanged SongChanged;

        public SongQueue(MediaElement mediaElement)
        {
            this.mediaElement = mediaElement;
            this.mediaElement.MediaEnded += mediaElement_MediaEnded;
        }

        private void mediaElement_MediaEnded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(currentIndex < queue.Count || repeat)
                this.NextSong();
        }

        public void NextSong()
        {
            ChangeSong(currentIndex + 1);
        }

        public void PreviousSong()
        {
            ChangeSong(currentIndex - 1);
        }

        public async void ChangeSong(int index)
        {
            mediaElement.Stop();

            if (shuffle)
            {
                int previousIndex = currentIndex;
                index = new Random().Next(queue.Count);

                if (index == previousIndex)
                    index++;
            }

            index = mod(index, queue.Count);

            GoogleMusicSongInstance songInstance = queue.ElementAt(index);

            mediaElement.DataContext = songInstance;
            String url = await App.GoogleAPI.GetStreamURL(songInstance.song);
            if (url == String.Empty)
                return;

            mediaElement.Source = new Uri(url);

            if (SongChanged != null)
                SongChanged.Invoke(songInstance, index);

            currentIndex = index;
        }

        public async Task<bool> Play()
        {
            if (queue.Count > 0 && currentIndex < 0)
            {
                NextSong();
                return false;
            }
            else if (queue.Count == 0)
            {
                return false;
            }
            else
            {
                mediaElement.Play();
                return true;
            }
        }

        public void Pause()
        {
            mediaElement.Pause();
        }

        public void Clear()
        {
            queue.Clear();
            currentIndex = -1;
        }

        public void AddSong(GoogleMusicSong song)
        {
            queue.Add(new GoogleMusicSongInstance(song));
        }

        public void AddSongs(List<GoogleMusicSong> songs)
        {
            foreach (GoogleMusicSong song in songs)
                queue.Add(new GoogleMusicSongInstance(song));
        }

        public void RemoveSong(int index)
        {
            if (index == currentIndex)
                NextSong();
            queue.RemoveAt(index);
        }

        public void PlaySong(GoogleMusicSong song)
        {
            Clear();
            AddSong(song);
            NextSong();
        }

        public void PlaySongs(List<GoogleMusicSong> songs)
        {
            Clear();
            AddSongs(songs);
            NextSong();
        }

        public ObservableCollection<GoogleMusicSongInstance> GetQueue()
        {
            return this.queue;
        }

        int mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }
    }
}
