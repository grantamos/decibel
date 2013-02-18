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
    public sealed partial class MainPage : Music8.Common.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
            
            App.songQueue = new SongQueue(playbackControl.GetMediaElement());
            App.collection = new Collection(App.googleAPI.Tracks);

            Window.Current.SizeChanged += Window_SizeChanged;

            NavigateContentFrame(typeof(Pages.CollectionPage));

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (searchTextBox.FocusState == Windows.UI.Xaml.FocusState.Keyboard)
                return;
            OnSearchTextBoxGotFocus(null, null);
            searchTextBox.Focus(FocusState.Keyboard);
        }

        void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            VisualStateManager.GoToState(playbackControl, Windows.UI.ViewManagement.ApplicationView.Value.ToString(), true);
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
            RadioButton radioBtn = sender as RadioButton;
            if (radioBtn.Name == "collectionRadioButton")
                NavigateContentFrame(typeof(Pages.CollectionPage));
            else if (radioBtn.Name == "eventsRadioButton")
                NavigateContentFrame(typeof(Pages.EventsPage));
            else if (radioBtn.Name == "nowPlayingRadioButton")
            {
                NavigateContentFrame(typeof(Pages.NowPlaying));
                //AnimationLibrary.AnimateOpacity(DarkBackground, .75, 1);
                //playbackControl.Expand();
            }
        }

        private void NavigateContentFrame(Type pageType)
        {
            if (ContentFrame == null)
                return;
            if (!ContentFrame.Navigate(pageType, this))
                throw new Exception("Failed to create the page");
        }

        private void nowPlayingRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            playbackControl.Collapse();
        }

        private void OnSearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (searchTextBox.Text.Equals("Search...", StringComparison.OrdinalIgnoreCase))
            {
                searchTextBox.Text = string.Empty;
            }
        }

        private void OnSearchTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                searchTextBox.Text = "Search...";
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Type type = ContentFrame.CurrentSourcePageType;
            if (type == typeof(Pages.CollectionPage) && !searchTextBox.Text.Equals("Search...", StringComparison.OrdinalIgnoreCase))
            {
                (ContentFrame.Content as CollectionPage).filterString = searchTextBox.Text;
                (ContentFrame.Content as CollectionPage).songs_CollectionChanged(null);
            }
        }

        public void showDetailFlyout(GoogleMusicSong song)
        {
            Flyout flyout = new Flyout();
            flyout.FlyoutWidth = (int) Window.Current.Bounds.Width / 2;
            flyout.FlyoutHeight = (int)(Window.Current.Bounds.Height * .8);
            flyout.FlyoutLeft = (int)Window.Current.Bounds.Width / 4;
            flyout.FlyoutTop = (int)(Window.Current.Bounds.Height / 10);
            flyout.ShowFlyout(new DetailsPage(song));
        }
    }
}
