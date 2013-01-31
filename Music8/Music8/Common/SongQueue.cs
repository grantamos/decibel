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
        private ObservableCollection<GoogleMusicSong> queue = new ObservableCollection<GoogleMusicSong>();
        private int currentIndex = -1;
        private bool shuffle = false;
        private bool repeat = false;
        private MediaElement mediaElement;
        private bool seeked = false;

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

        public GoogleMusicSong GetCurrentSong()
        {
            return queue.ElementAt(currentIndex);
        }

        public async void ChangeSong(int index)
        {
            mediaElement.Stop();

            if (!repeat)
            {
                currentIndex = index;
                currentIndex = mod(currentIndex, queue.Count);
            }
            else if (shuffle)
            {
                int previousIndex = currentIndex;
                currentIndex = new Random().Next(queue.Count);

                if (currentIndex == previousIndex)
                    currentIndex++;
            }

            GoogleMusicSong song = GetCurrentSong();
            mediaElement.DataContext = song;
            mediaElement.Source = new Uri(await App.googleAPI.GetStreamURL(song));
        }

        public void NextSong()
        {
            ChangeSong(currentIndex + 1);

            Play();
        }

        public void PreviousSong()
        {
            ChangeSong(currentIndex - 1);

            Play();
        }

        public void Clear()
        {
            queue.Clear();
            currentIndex = -1;
        }

        public async Task<bool> Play()
        {
            if (queue.Count > 0 && currentIndex < 0)
            {
                NextSong();
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

        public void AddSong(GoogleMusicSong song)
        {
            queue.Add(song);
        }

        public void AddSongs(List<GoogleMusicSong> songs)
        {
            foreach(GoogleMusicSong song in songs)
                queue.Add(song);
        }

        public void RemoveSong(GoogleMusicSong song)
        {
            queue.Remove(song);
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

            Play();
        }

        public void PlaySongs(List<GoogleMusicSong> songs)
        {
            Clear();
            AddSongs(songs);
            NextSong();
        }

        int mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }
    }
}
