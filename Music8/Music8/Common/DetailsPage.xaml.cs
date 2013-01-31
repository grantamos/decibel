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
    public sealed partial class DetailsPage : UserControl
    {
        public DetailsPage(GoogleMusicSong selectedSong)
        {
            this.InitializeComponent();
            this.DataContext = selectedSong;
            albumListView.ItemsSource = App.googleAPI.Tracks.GroupBy(song => song.AlbumArtist).Select(song => song.First()).Where(song => song.AlbumArtist == selectedSong.AlbumArtist);
            songListView.ItemsSource = App.googleAPI.Tracks.Where(song => song.AlbumArtist == selectedSong.AlbumArtist && song.Album == selectedSong.Album).OrderBy(song => song.Track);
        }
    }
}
