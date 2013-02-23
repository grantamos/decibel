using Music8.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.Security.Credentials.UI;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string userName, password;
            bool saveCredentials = false;
            PasswordCredential credential = null;

            AnimationLibrary.AnimateOpacity(this.background, 1.0, 2.0);
            AnimationLibrary.AnimateX(this.logo, -275, 0.5);

            try
            {
                var passwordVault = new PasswordVault();
                credential = passwordVault.FindAllByResource("Music8")[0];
                credential = passwordVault.Retrieve("Music8", credential.UserName);
            }
            catch (Exception exception) { }

            if (credential == null)
            {
                CredentialPickerOptions credPickerOptions = new CredentialPickerOptions();
                credPickerOptions.Message = "Enter your Google Music credentials";
                credPickerOptions.Caption = "Login to Music8";
                credPickerOptions.TargetName = ".";
                credPickerOptions.AlwaysDisplayDialog = true;
                credPickerOptions.AuthenticationProtocol = AuthenticationProtocol.Basic;
                var credPickerResults = await CredentialPicker.PickAsync(credPickerOptions);

                userName = credPickerResults.CredentialUserName;
                password = credPickerResults.CredentialPassword;
                saveCredentials = credPickerResults.CredentialSaveOption == CredentialSaveOption.Selected;
            }
            else
            {
                userName = credential.UserName;
                password = credential.Password;
            }

            DoLogin(userName, password, saveCredentials);
        }

        public async void DoLogin(string userName, string password, bool saveCredentials)
        {
            bool loginSuccess = await App.googleAPI.Login(userName, password);

            if (loginSuccess)
            {
                if (saveCredentials)
                {
                    var passwordVault = new PasswordVault();
                    var credential = new PasswordCredential("Music8", userName, password);
                    passwordVault.Add(credential);
                }

                App.googleAPI.GetUserPlaylists();
                App.googleAPI.GetAllSongs(1);

                if (!this.Frame.Navigate(typeof(Pages.MainPage)))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
        }
    }
}
