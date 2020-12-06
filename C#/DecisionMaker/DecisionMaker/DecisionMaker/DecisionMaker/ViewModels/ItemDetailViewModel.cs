using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using DecisionMaker.Models;
using DecisionMaker.Views;

namespace DecisionMaker.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;

            Delete = new Command(() => 
            { 
                MessagingCenter.Send(this, "DeleteItem", Item);
            });
        }
        public ICommand Delete { get; }
    }
}
