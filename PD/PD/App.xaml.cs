using PD.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PD
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "dating.db";

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
