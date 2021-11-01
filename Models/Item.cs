using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarsReminder.Models
{
    public class Item
    {
        [Display(Name ="Lp")]
        public int Id { get; set; }
        [Display(Name ="Marka")]
        public string Mark { get; set; }
        [Display(Name ="Model")]
        public string Model { get; set; }
        [Display(Name ="Cena")]
        public string Price { get; set; }
        [Display(Name ="Przebieg")]
        public string Distance { get; set; }
        [Display(Name ="Opis")]
        public string Description { get; set; }
        
        [Display(Name="Foto")]
        public string PhotoUrl { get; set; }

    }
}
