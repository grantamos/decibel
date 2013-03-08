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

            Window.Current.SizeChanged += Window_SizeChanged;

            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            NavigateContentFrame(typeof(Pages.CollectionExplorerPage), this);
        }

        void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            VisualStateManager.GoToState(playbackControl, Windows.UI.ViewManagement.ApplicationView.Value.ToString(), true);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioBtn = sender as RadioButton;
            if (radioBtn.Name == "collectionRadioButton")
                NavigateContentFrame(typeof(Pages.CollectionExplorerPage), this);
            else if (radioBtn.Name == "eventsRadioButton")
                NavigateContentFrame(typeof(Pages.EventsPage));
            else if (radioBtn.Name == "nowPlayingRadioButton")
            {
                NavigateContentFrame(typeof(Pages.NowPlaying));
            }
        }

        public void NavigateContentFrame(Type pageType)
        {
            this.NavigateContentFrame(pageType, null);
        }

        public void NavigateContentFrame(Type pageType, object parameter)
        {
            if (ContentFrame == null)
                return;
            if (!ContentFrame.Navigate(pageType, parameter))
                throw new Exception("Failed to create the page");
        }

        private void nowPlayingRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            playbackControl.Collapse();
        }
    }
}
