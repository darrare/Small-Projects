using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DecisionMaker.Models;
using DecisionMaker.ViewModels;

namespace DecisionMaker.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            MessagingCenter.Send(this, "UpdateItem", viewModel.Item);
            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Send(this, "UpdateItem", viewModel.Item);
            base.OnDisappearing();
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}