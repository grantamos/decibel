﻿using Byteopia.Music.GoogleMusicAPI;
using Music8.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Music8.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class NowPlaying : Music8.Common.LayoutAwarePage
    {
        public NowPlaying()
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
            this.nowPlayingList.SetBinding(ListView.ItemsSourceProperty, new Binding() { Source = App.SongQueue.GetQueue() });

            App.SongQueue.SongChanged += songQueue_SongChanged;
            this.nowPlayingList.SelectedIndex = App.SongQueue.currentIndex;
        }

        private void songQueue_SongChanged(GoogleMusicSongInstance songInstance, int index)
        {
            GoogleMusicSong song = songInstance.song;
            if (song != null && song.ArtURL != null)
                artistBackground.Source = new BitmapImage(new Uri(song.ArtURL, UriKind.Absolute));

            this.nowPlayingList.SelectedIndex = index;
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

        private void Remove_Song(object sender, RoutedEventArgs e)
        {
            GoogleMusicSongInstance song = (sender as Button).DataContext as GoogleMusicSongInstance;

            App.SongQueue.RemoveSong((nowPlayingList.ItemsSource as ObservableCollection<GoogleMusicSongInstance>).IndexOf(song));
        }

        private void nowPlayingList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            App.SongQueue.ChangeSong(this.nowPlayingList.SelectedIndex);
        }

        private void nowPlayingList_Drop_1(object sender, DragEventArgs e)
        {

        }
    }
}
