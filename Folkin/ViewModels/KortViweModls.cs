using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Folkin.ViewModels
{
    public class KortViweModls
    {
        public int Id { get; set; }


        [Display(Name = "Card Type")]
        [Required(ErrorMessage = "Card type is required")]
        public bool Abstract { get; set; }


        [Display(Name = "Kort Titel")]
        [Required(ErrorMessage = "Titel is required")]
        public string Titel { get; set; }

        [Display(Name = "Kort Beskivelse")]
        [Required(ErrorMessage = "Beskivelse is required")]
        public string Beskivelse { get; set; } 

        //Relationships Tag
        [Display(Name = "Select Tag(s)")]
        public List<int> TagIds { get; set; }


        //Relationships Type
        [Display(Name = "Select Type(s)")]
        public List<int> TypeIds { get; set; }

        //Relationships Sillhouette
        [Display(Name = "Select a Sillhouette")]
        public int? SillhouetteId { get; set; }

    }
}
