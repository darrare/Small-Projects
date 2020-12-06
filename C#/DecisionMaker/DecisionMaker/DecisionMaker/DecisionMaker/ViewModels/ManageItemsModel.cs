using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;

using DecisionMaker.Models;
using DecisionMaker.Views;

namespace DecisionMaker.ViewModels
{
    public class ManageItemsModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ManageItemsModel()
        {
            Title = "Manage Items";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "UpdateItem", async (obj, item) =>
            {
                var oldItem = Items.FirstOrDefault(t => t.Id == (item as Item).Id);
                if (Items.Remove(oldItem))
                {
                    Items.Add(item as Item);
                    await DataStore.UpdateItemAsync(item as Item);
                }
            });

            MessagingCenter.Subscribe<ItemDetailViewModel, Item>(this, "DeleteItem", async (obj, item) =>
            {
                var oldItem = Items.FirstOrDefault(t => t.Id == (item as Item).Id);
                Items.Remove(oldItem);
                await DataStore.DeleteItemAsync((item as Item).Id);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items.OrderBy(t => t.Id))
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}