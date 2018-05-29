using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.Models
{
    public class SideNavigationItem
    {
        public string Name { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }

        public SideNavigationItem(string name, string controller, string action)
        {
            Name = name;
            Controller = controller;
            Action = action;
        }
    }
}
