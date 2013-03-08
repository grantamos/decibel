using Byteopia.Music.GoogleMusicAPI;
using Music8.Common;
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
            AllData();

            this.ArtistListView.SelectedIndex = -1;
            this.AlbumListView.SelectedIndex = -1;

        }

        private void AllData()
        {
            this.ArtistListView.ItemsSource = App.MusicLibrary.Artists;
            this.AlbumListView.ItemsSource = App.MusicLibrary.Albums;
            this.SongListView.ItemsSource = App.MusicLibrary.Tracks;

            this.AlbumListHeader.Text = "all artists";
            this.SongListHeader.Text = "all songs";
        }


        private void ArtistListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                var z = this.AlbumListView.SelectedItems;
                Music8.Music.MusicLibrary.Artist artist = e.AddedItems.First() as Music8.Music.MusicLibrary.Artist;
                String artistName = artist.ArtistName;
                this.AlbumListHeader.Text = artistName;
                this.SongListHeader.Text = "songs by " + artistName;

                
                this.SongListView.ItemsSource = e.AddedItems.SelectMany(c => (c as Music8.Music.MusicLibrary.Artist).Songs);

                this.AlbumListView.ItemsSource = e.AddedItems.SelectMany(c => (c as Music8.Music.MusicLibrary.Artist).Albums);
            }
            else
                AllData();

            this.AlbumListView.SelectedIndex = -1;
            this.SongListView.SelectedIndex = -1;
        }

        private void AlbumListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if(e.AddedItems.Count() > 0)
            {
                String albumName = (e.AddedItems.First() as Album).AlbumName;
                this.SongListHeader.Text = albumName;
                cvsSongs.Source = App.Collection.songs.Where(s => s.Album == albumName).OrderBy(s => s.Title);
            }
            else if (this.ArtistListView.SelectedIndex >= 0)
            {
                String artistName = (e.RemovedItems.First() as Album).ArtistName;
                this.SongListHeader.Text = "Songs by " + artistName;
                cvsSongs.Source = App.Collection.songs.Where(s => s.Artist == artistName).OrderBy(s => s.Title);
            }
            else
            {
                this.SongListHeader.Text = "All Songs";
                this.cvsSongs.Source = App.Collection.songs.OrderBy(s => s.Title);
            }

            this.SongListView.SelectedIndex = -1;*/
        }
    }
}
