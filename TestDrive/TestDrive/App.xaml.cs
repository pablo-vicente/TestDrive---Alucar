using System;
using TestDrive.Models;
using TestDrive.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestDrive
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }

        protected override void OnStart()
        {
            MessagingCenter.Subscribe<Usuario>(this, "Sucesso Login", usuario =>
            {
                MainPage = new NavigationPage(new ListagemView());
            });
        }

        protected override void OnSleep()
        {
            MessagingCenter.Unsubscribe<Usuario>(this, "Sucesso Login");
        }

        protected override void OnResume()
        {
        }
    }
}
