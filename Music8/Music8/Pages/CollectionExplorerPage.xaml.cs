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
        CollectionViewSource cvsArtists, cvsAlbums, cvsSongs;

        public CollectionExplorerPage()
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
            cvsArtists = new CollectionViewSource { Source = App.Collection.artists };
            cvsAlbums = new CollectionViewSource { Source = App.Collection.albums };
            cvsSongs = new CollectionViewSource { Source = App.Collection.songs };

            this.ArtistListView.SetBinding(ListView.ItemsSourceProperty, new Binding() { Source = cvsArtists });
            this.AlbumListView.SetBinding(ListView.ItemsSourceProperty, new Binding() { Source = cvsAlbums });
            this.SongListView.SetBinding(ListView.ItemsSourceProperty, new Binding() { Source = cvsSongs });

            this.ArtistListView.SelectedIndex = -1;
            this.AlbumListView.SelectedIndex = -1;

            //this.AlbumListHeader.Text = "Albums";
            //this.SongListHeader.Text = "Songs";
        }

        private void ArtistListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                Artist artist = e.AddedItems.First() as Artist;
                object item = ArtistListView.ItemContainerGenerator.ContainerFromItem(artist);

                String artistName = artist.ArtistName;
                this.AlbumListHeader.Text = artistName;
                this.SongListHeader.Text = "Songs by " + artistName;
                cvsAlbums.Source = App.Collection.albums.Where(s => s.ArtistName == artistName).OrderBy(s => s.AlbumName);
                cvsSongs.Source = App.Collection.songs.Where(s => s.Artist == artistName).OrderBy(s => s.Title);
            }
            else
            {
                this.AlbumListHeader.Text = "All Albums";
                cvsAlbums.Source = App.Collection.albums.OrderBy(s => s.AlbumName);

                this.SongListHeader.Text = "All Songs";
                this.cvsSongs.Source = App.Collection.songs.OrderBy(s => s.Title);
            }

            this.AlbumListView.SelectedIndex = -1;
            this.SongListView.SelectedIndex = -1;
        }

        private void AlbumListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count() > 0)
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

            this.SongListView.SelectedIndex = -1;
        }
    }
}
