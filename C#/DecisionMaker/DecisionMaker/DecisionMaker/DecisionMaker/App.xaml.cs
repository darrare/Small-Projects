using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DecisionMaker.Services;
using DecisionMaker.Views;

namespace DecisionMaker
{
    public partial class App : Application
    {
        public static App Instance;
        public App()
        {
            InitializeComponent();

            Instance = this;

            DependencyService.Register<DataStore>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
