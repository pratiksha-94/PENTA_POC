using FreshMvvm;
using PUC.PageModels;
using PUC.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PUC
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            GoToLogin();

        }

        public void GoToLogin()
        {
            var page = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;
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
