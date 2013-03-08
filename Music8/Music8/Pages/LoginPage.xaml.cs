using Music8.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.Security.Credentials.UI;
using Windows.UI.Popups;
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

            this.Loaded += LoginPage_Loaded;
        }

        void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            Login();
        }

        public async void Login()
        {
            PasswordCredential credential = null;

            try
            {
                var passwordVault = new PasswordVault();
                credential = passwordVault.FindAllByResource(App.APP_NAME)[0];
                credential = passwordVault.Retrieve(App.APP_NAME, credential.UserName);
            }
            catch
            {

            }

            if (credential != null)
            {
                btnLogin.IsEnabled = false;

                tbUserEmail.Text = credential.UserName;
                tbUserPassword.Password = credential.Password;

                await LoginAndLoad(credential.UserName, credential.Password);
            }
            else
            {
                if (App.GoogleAPI.HasSession())
                {
                    await LoginAndLoad("", "");
                }
            }
        }


        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLogin.IsEnabled = false;

            String userName = tbUserEmail.Text;
            String userPassword = tbUserPassword.Password;

            if (cbSaveCreds.IsChecked.Value)
            {
                var passwordVault = new PasswordVault();
                var credential = new PasswordCredential(App.APP_NAME, userName, userPassword);
                passwordVault.Add(credential);
            }

            await LoginAndLoad(userName, userPassword);
        }

        private async Task LoginAndLoad(String userName, String userPassword)
        {
            prLoading.IsActive = true;
            if (await DoLoginWork(userName, userPassword))
            {
                prLoading.IsActive = false;
                Frame.Navigate(typeof(Pages.MainPage));
            }
            else
            {
                MessageDialog dia = new MessageDialog("error logging in, please try again.", "login error");
                await dia.ShowAsync();
                btnLogin.IsEnabled = true;
                prLoading.IsActive = false;
            }
        }

        private async Task<bool> DoLoginWork(String userName = "", String userPassword = "")
        {
            return await App.GoogleAPI.Login(userName, userPassword);
        }
    }
}
