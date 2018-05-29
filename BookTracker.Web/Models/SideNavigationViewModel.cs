using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.Models
{
    public class SideNavigationViewModel
    {
        public IEnumerable<SideNavigationItem> NavigationItems { get; set; }

        public SideNavigationItem FocusedItem { get; set; }

        public SideNavigationViewModel(string focusedItemName, IEnumerable<SideNavigationItem> navigationItems)
        {
            NavigationItems = navigationItems;

            FocusedItem = navigationItems.First(x => x.Name == focusedItemName);
        }
    }
}
