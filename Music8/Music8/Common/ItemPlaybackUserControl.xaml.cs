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
            GoogleMusicSong song = (btn.DataContext as GoogleMusicSong);

            if(btn == null || song == null)
                return;

            if (btn.DataContext.GetType() == typeof(GoogleMusicSong))
                App.MusicLibrary.PlaySong(song);
        }

        private void AddToNowPlaying(object sender, RoutedEventArgs e)
        {
          /*  Button btn = sender as Button;
            if (btn.DataContext.GetType() == typeof(Artist))
                App.SongQueue.AddSongs(App.Collection.GetArtistSongs((btn.DataContext as Artist).song));
            else if (btn.DataContext.GetType() == typeof(Album))
                App.SongQueue.AddSongs(App.Collection.GetAlbumSongs((btn.DataContext as Album).song));
            else if (btn.DataContext.GetType() == typeof(GoogleMusicSong))
                App.SongQueue.AddSong(btn.DataContext as GoogleMusicSong);*/
        }

        private void AddToPlaylist(object sender, RoutedEventArgs e)
        {

        }
    }
}
