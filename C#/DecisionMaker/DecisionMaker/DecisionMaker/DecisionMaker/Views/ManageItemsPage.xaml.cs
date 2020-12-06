using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DecisionMaker.Models;
using DecisionMaker.Views;
using DecisionMaker.ViewModels;

namespace DecisionMaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageItemsPage : ContentPage
    {
        ManageItemsModel viewModel;

        public ManageItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ManageItemsModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}