using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace DecisionMaker.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Models.Item Item { get; set; }

        public HomeViewModel()
        {
            Title = "Home";

            Item = new Models.Item()
            {
                Text = "Randomize!",
                Description = "Click randomize to select a random item!",
            };
        }

        public async void RandomizeItemAsync()
        {
            Item = await DataStore.GetRandomItem();
            OnPropertyChanged("Item");
        }
    }
}