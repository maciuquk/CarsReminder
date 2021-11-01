using CarsReminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsReminder.ModelView
{
    public class IndexContextModelView
    {
        public List<Item> ItemList{ get; set; }
        public Dictionary<string, string> Groups { get; set; }
        public bool IsFilteredList { get; set; }
    }
}
