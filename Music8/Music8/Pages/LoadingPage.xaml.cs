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
    public sealed partial class LoadingPage : Page
    {
        public int RemoteTrackCount
        {
            get;
            set;
        }

        public double LoadProgress
        {
            get
            {
                if (App.MusicLibrary.CurrentTotalTracks == 0 || RemoteTrackCount == 0)
                    return 0;

                return ((double)App.MusicLibrary.CurrentTotalTracks / (double)RemoteTrackCount) * 100.0;
            }
        }

        public LoadingPage()
        {
            this.InitializeComponent();

            pbLoading.Value = 0;
            prLoading.IsActive = true;

            this.Loaded += LoadingPage_Loaded;

            App.GoogleAPI.GetAllSongsChunkAdded += GoogleAPI_ChunkAdded;
            App.GoogleAPI.GetAllSongsComplete += GoogleAPI_GetAllSongsComplete;
        }

        void LoadingPage_Loaded(object sender, RoutedEventArgs e)
        {
            DoLoad();
        }

        void GoogleAPI_GetAllSongsComplete(object sender, EventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        void GoogleAPI_ChunkAdded(IEnumerable<Byteopia.Music.GoogleMusicAPI.GoogleMusicSong> songs)
        {
            App.MusicLibrary.CurrentTotalTracks += songs.Count();

            UpdateProgressIndicators();
        }

        private async void DoLoad()
        {
            tbLoading.Text = "fetching song total";
            RemoteTrackCount = await App.GoogleAPI.GetTrackCount();
            tbLoading.Text = "fetching playlists";

            App.MusicLibrary.FetchFullLibrary();
        }

        private void UpdateProgressIndicators()
        {
            tbLoading.Text = String.Format("fetched {0}/{1} songs", App.MusicLibrary.CurrentTotalTracks, RemoteTrackCount);
            pbLoading.Value = LoadProgress;
        }
    }
}
