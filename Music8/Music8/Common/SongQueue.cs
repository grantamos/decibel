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

        private ObservableCollection<GoogleMusicSong> queue = new ObservableCollection<GoogleMusicSong>();
        private bool shuffle = false;
        private bool repeat = false;
        private MediaElement mediaElement;

        public delegate void NotifySongChanged(GoogleMusicSong song, int index);
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

            GoogleMusicSong song = queue.ElementAt(index);

            mediaElement.DataContext = song;
            mediaElement.Source = new Uri(await App.googleAPI.GetStreamURL(song));

            if (SongChanged != null)
                SongChanged.Invoke(song, index);

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
            queue.Add(song);
        }

        public void AddSongs(List<GoogleMusicSong> songs)
        {
            foreach (GoogleMusicSong song in songs)
                queue.Add(song);
        }

        public void RemoveSong(int index)
        {
            if (index == currentIndex)
                NextSong();
            queue.RemoveAt(index);
        }

        public void PlaySong(GoogleMusicSong song)
        {
            int index = queue.IndexOf(song);

            if (index < 0)
            {
                queue.Insert(currentIndex + 1, song);
                currentIndex++;
            }
            else
            {
                currentIndex = index;
            }
        }

        public void PlaySongs(List<GoogleMusicSong> songs)
        {
            Clear();
            AddSongs(songs);
            NextSong();
        }

        public ObservableCollection<GoogleMusicSong> GetQueue()
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
