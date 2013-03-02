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
using Byteopia.Music.GoogleMusicAPI;
using System.Collections.ObjectModel;
using Music8.Common;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Music8.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class CollectionPage : Music8.Common.LayoutAwarePage
    {
        public string filterString = "";
        MainPage parent;

        public CollectionPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            this.parent = navigationParameter as MainPage;
            //App.collection.CollectionChanged += songs_CollectionChanged;
            App.googleAPI.ChunkAdded += new API.NotifyChunkAdded(this.songs_CollectionChanged);
            songs_CollectionChanged(App.googleAPI.Tracks);

            App.googleAPI.Tracks.CollectionChanged += Tracks_CollectionChanged;
        }

        //TO DO - Different Solution
        void Tracks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            artistsGridView.ItemsSource = Filter(App.collection.GetArtists());

            albumsGridView.ItemsSource = Filter(App.collection.GetAlbums());

            songsGridView.ItemsSource = Filter(App.collection.GetSongs());
        }

        public void songs_CollectionChanged(ObservableCollection<GoogleMusicSong> songs)
        {
            artistsGridView.ItemsSource = Filter(App.collection.GetArtists());

            albumsGridView.ItemsSource = Filter(App.collection.GetAlbums());

            songsGridView.ItemsSource = Filter(App.collection.GetSongs());

            /*
            playlistsGridView.ItemsSource = App.googleAPI.Tracks.GroupBy(song => song.Artist)
                .Select(g => g.First())
                .ToList();

            genresGridView.ItemsSource = App.googleAPI.Tracks.GroupBy(song => song.Artist)
                .Select(g => g.First())
                .ToList();
            */
        }

        public List<GoogleMusicSong> Filter(List<GoogleMusicSong> songs)
        {
            return songs.Where(s => s.Album.IndexOf(filterString, StringComparison.OrdinalIgnoreCase) >= 0
                || s.Artist.IndexOf(filterString, StringComparison.OrdinalIgnoreCase) >= 0
                || s.Title.IndexOf(filterString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.Content == null)
                return;

            RadioButton radioBtn = sender as RadioButton;

            artistsGridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            albumsGridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            songsGridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            playlistsGridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            genresGridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (radioBtn.Name == "artistsRadioButton")
                artistsGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (radioBtn.Name == "albumsRadioButton")
                albumsGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (radioBtn.Name == "songsRadioButton")
                songsGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (radioBtn.Name == "playlistsRadioButton")
                playlistsGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (radioBtn.Name == "genresRadioButton")
                genresGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void artistsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //this.parent.showDetailFlyout(e.ClickedItem as GoogleMusicSong);
            this.parent.NavigateContentFrame(typeof(ArtistDetailPage), e.ClickedItem);
        }

        private void Play_Artist(object sender, RoutedEventArgs e)
        {
            App.songQueue.PlaySongs(App.collection.GetArtist((sender as Button).DataContext as GoogleMusicSong));
        }

        private void Add_Artist(object sender, RoutedEventArgs e)
        {
            App.songQueue.AddSongs(App.collection.GetArtist((sender as Button).DataContext as GoogleMusicSong));
        }

        private void Play_Album(object sender, RoutedEventArgs e)
        {
            App.songQueue.PlaySongs(App.collection.GetAlbum((sender as Button).DataContext as GoogleMusicSong));
        }

        private void Add_Album(object sender, RoutedEventArgs e)
        {
            App.songQueue.AddSongs(App.collection.GetAlbum((sender as Button).DataContext as GoogleMusicSong));
        }

        private void Play_Song(object sender, RoutedEventArgs e)
        {
            App.songQueue.PlaySong((sender as Button).DataContext as GoogleMusicSong);
        }

        private void Add_Song(object sender, RoutedEventArgs e)
        {
            App.songQueue.AddSong((sender as Button).DataContext as GoogleMusicSong);
        }
    }
}
