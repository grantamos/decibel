using Byteopia.Music.GoogleMusicAPI;
using Music8.Common;
using Music8.Music;
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
    public sealed partial class CollectionExplorerPage : Page
    {
        public CollectionExplorerPage()
        {
            this.InitializeComponent();

            this.Loaded += CollectionExplorerPage_Loaded;
        }

        void CollectionExplorerPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ArtistListView.ItemsSource = App.MusicLibrary.Artists;
            this.AlbumListView.ItemsSource = App.MusicLibrary.Albums;
            this.SongListView.ItemsSource = App.MusicLibrary.Tracks;

            this.ArtistListView.SelectedIndex = -1;
            this.AlbumListView.SelectedIndex = -1;

        }

        private void ArtistListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                var z = this.AlbumListView.SelectedItems;
                Artist artist = e.AddedItems.First() as Artist;
                String artistName = artist.ArtistName;
                this.AlbumListHeader.Text = artistName;
                this.SongListHeader.Text = "songs by " + artistName;


                this.SongListView.ItemsSource = e.AddedItems.SelectMany(c => (c as Artist).Songs);

                this.AlbumListView.ItemsSource = e.AddedItems.SelectMany(c => (c as Artist).Albums);
            }
            else
            {
                this.AlbumListView.ItemsSource = App.MusicLibrary.Albums;
                this.SongListView.ItemsSource = App.MusicLibrary.Tracks;

                this.AlbumListHeader.Text = "all albums";
                this.SongListHeader.Text = "all songs";
            }

            this.AlbumListView.SelectedIndex = -1;
            this.SongListView.SelectedIndex = -1;
        }

        private void AlbumListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                String albumName = (e.AddedItems.First() as Album).AlbumName;
                this.SongListHeader.Text = albumName;

                this.SongListView.ItemsSource = e.AddedItems.SelectMany(s => (s as Album).Songs);
            }
            else if (this.ArtistListView.SelectedIndex >= 0 && e.RemovedItems.Count != 0)
            {
                String artistName = (e.RemovedItems.First() as Album).ArtistName;
                this.SongListHeader.Text = "songs by " + artistName;
                this.SongListView.ItemsSource = e.RemovedItems.SelectMany(s => (s as Album).Songs);
            }
            else
            {
                this.SongListView.ItemsSource = App.MusicLibrary.Tracks;
                this.SongListHeader.Text = "all songs";
            }

            this.SongListView.SelectedIndex = -1;
        }
    }
}
