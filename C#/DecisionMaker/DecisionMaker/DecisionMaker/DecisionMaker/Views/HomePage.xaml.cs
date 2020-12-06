using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DecisionMaker.ViewModels;

namespace DecisionMaker.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        public HomeViewModel ViewModel { get; set; }

        public HomePage()
        {
            InitializeComponent();

            ViewModel = new HomeViewModel();

            BindingContext = ViewModel;
        }

        private void RandomizeButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.RandomizeItemAsync();
            OnPropertyChanged("ViewModel");
        }
    }
}