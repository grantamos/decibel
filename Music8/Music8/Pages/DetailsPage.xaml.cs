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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Music8.Common
{
    public sealed partial class DetailsPage : UserControl
    {
        GoogleMusicSong selectedAlbum = null;

        List<GoogleMusicSong> artistSongs;

        public GoogleMusicSong SelectedAlbum
        {
            get { return selectedAlbum; }
            set { selectedAlbum = value; }
        }
       
        public DetailsPage(GoogleMusicSong selectedSong)
        {
            this.InitializeComponent();

            albumListView.Tapped += albumListView_Tapped;

            List<GoogleMusicSong> artistAlbums = App.googleAPI.Tracks.Where(c => c.Artist == selectedSong.Artist)
                .OrderBy(c => c.Album).Distinct(new DistinctItemComparers.DistinctItemComparerAlbum()).ToList();

            artistSongs = App.googleAPI.Tracks.Where(song => song.AlbumArtist == selectedSong.AlbumArtist).ToList();

            albumListView.ItemsSource = artistAlbums;

            GoogleMusicSong firstAlbum = albumListView.Items.First() as GoogleMusicSong;

            FillAlbumSongs(firstAlbum);

            if (albumListView.Items.Count > 0)
            {
                albumListView.SelectedIndex = 0;
            }

            this.DataContext = SelectedAlbum;

            GetArtistImage();

            this.AristDetails.Text = String.Format("{0} albums, {1} songs", artistAlbums.Count, artistSongs.Count());

            this.ArtistGenere.Text = SelectedAlbum.Genre;
        }

        private async void GetArtistImage()
        {
            String path = await App.lastAPI.GetArtistImage(selectedAlbum.Artist, Byteopia.Music.Lastfm.Models.ImageSize.mega);
            if (!path.Equals(String.Empty))
            {
                BitmapImage newImage = new BitmapImage();
                newImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                
                newImage.UriSource = new Uri(path);

                ArtistImage.Source = newImage;
            }
        }

        private void FillAlbumSongs(GoogleMusicSong album)
        {
            if (album != null)
            {
                songListView.ItemsSource = artistSongs.Where(song => song.AlbumNorm == album.AlbumNorm).OrderBy(song => song.Track);
                selectedAlbum = album;
                this.DataContext = album;
                this.AlbumTextBlock.Text = String.Format("{0} songs in \"{1}\"", songListView.Items.Count, selectedAlbum.Album);
            }
        }

        void albumListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FillAlbumSongs((sender as ListView).SelectedItem as GoogleMusicSong);
        }
    }
}
