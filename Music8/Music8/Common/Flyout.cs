using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Music8.Common
{
    public partial class Flyout
    {
        public int FlyoutWidth = 346;
        public int FlyoutHeight = 768;
        public int FlyoutLeft = 0;
        public int FlyoutTop = 0;
        private Popup popup;

        /// <summary>
        /// Shows the given control in the flyout.
        /// </summary>
        public void ShowFlyout(UserControl control)
        {
            this.popup = new Popup();
            //this.popup.Opened += OnPopupOpened;
            //this.popup.Closed += OnPopupClosed;
            this.popup.IsLightDismissEnabled = true;
            this.popup.Width = FlyoutWidth;
            this.popup.Height = Window.Current.Bounds.Height;

            control.Width = FlyoutWidth;
            control.Height = FlyoutHeight;

            this.popup.Child = control;
            this.popup.SetValue(Canvas.LeftProperty, FlyoutLeft);
            this.popup.SetValue(Canvas.TopProperty, FlyoutTop);
            this.popup.IsOpen = true;
        }
        
        /*
        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                this.popup.IsOpen = false;
            }
        }


        private void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
            OnSettingsClosed(EventArgs.Empty);
        }


        private void OnPopupOpened(object sender, object e)
        {
            OnSettingsOpened(EventArgs.Empty);
        }
        */
    }
}
