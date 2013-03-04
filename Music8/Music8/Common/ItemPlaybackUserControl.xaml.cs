using Byteopia.Music.GoogleMusicAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Music8.Common
{
    public sealed partial class ItemPlaybackUserControl : UserControl
    {
        public ItemPlaybackUserControl()
        {
            this.InitializeComponent();
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.DataContext.GetType() == typeof(Artist))
                App.songQueue.PlaySongs(App.collection.GetArtistSongs((btn.DataContext as Artist).song));
            else if (btn.DataContext.GetType() == typeof(Album))
                App.songQueue.PlaySongs(App.collection.GetAlbumSongs((btn.DataContext as Album).song));
            else if (btn.DataContext.GetType() == typeof(GoogleMusicSong))
                App.songQueue.PlaySong(btn.DataContext as GoogleMusicSong);
        }

        private void AddToNowPlaying(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.DataContext.GetType() == typeof(Artist))
                App.songQueue.AddSongs(App.collection.GetArtistSongs((btn.DataContext as Artist).song));
            else if (btn.DataContext.GetType() == typeof(Album))
                App.songQueue.AddSongs(App.collection.GetAlbumSongs((btn.DataContext as Album).song));
            else if (btn.DataContext.GetType() == typeof(GoogleMusicSong))
                App.songQueue.AddSong(btn.DataContext as GoogleMusicSong);
        }

        private void AddToPlaylist(object sender, RoutedEventArgs e)
        {

        }
    }
}
