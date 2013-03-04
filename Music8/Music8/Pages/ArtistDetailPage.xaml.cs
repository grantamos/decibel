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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Music8.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArtistDetailPage : Page
    {
        public ArtistDetailPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GoogleMusicSong song = e.Parameter as GoogleMusicSong;

            IEnumerable<IGrouping<string, GoogleMusicSong>> groupedItems = App.collection.GetAlbums(song);
            CollectionViewSource cvsSongs = new CollectionViewSource() { Source = groupedItems, IsSourceGrouped = true };
            
            this.artistGridView.SetBinding(ListView.ItemsSourceProperty, new Binding() { Source = cvsSongs });

            this.artistTitle.Text = song.Album;

            int numAlbums = groupedItems.Count();
            int numSongs = 0;
            TimeSpan totalTime = new TimeSpan();

            foreach (var item in groupedItems)
            {
                foreach (var songItem in item)
                {
                    numSongs++;
                    totalTime += TimeSpan.FromMilliseconds((songItem as GoogleMusicSong).Duration);
                }
            }

            this.artistDetails.Text = groupedItems.Count() + " Album" + (numAlbums > 1 ? "s, " : ", ") + numSongs + " Songs, " + totalTime.ToString();
            //this.artistGridView.DataContext = song;
        }

        private void Play_Artist(object sender, RoutedEventArgs e)
        {
            App.songQueue.PlaySongs(App.collection.GetArtistSongs((sender as Button).DataContext as GoogleMusicSong));
        }

        private void Add_Artist(object sender, RoutedEventArgs e)
        {
            App.songQueue.AddSongs(App.collection.GetArtistSongs((sender as Button).DataContext as GoogleMusicSong));
        }

        private void Play_Album(object sender, RoutedEventArgs e)
        {
            App.songQueue.PlaySongs(App.collection.GetAlbumSongs((sender as Button).DataContext as GoogleMusicSong));
        }

        private void Add_Album(object sender, RoutedEventArgs e)
        {
            App.songQueue.AddSongs(App.collection.GetAlbumSongs((sender as Button).DataContext as GoogleMusicSong));
        }

        private void Play_Song(object sender, RoutedEventArgs e)
        {
            App.songQueue.PlaySong((sender as Button).DataContext as GoogleMusicSong);
        }

        private void Add_Song(object sender, RoutedEventArgs e)
        {
            App.songQueue.AddSong((sender as Button).DataContext as GoogleMusicSong);
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            // Use the navigation frame to return to the previous page
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }
    }
}
