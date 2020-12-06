using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionMaker.Models
{
    public enum MenuItemType
    {
        Home,
        ManageItems
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
