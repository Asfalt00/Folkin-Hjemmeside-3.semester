using Folkin.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Folkin.Models
{
    public class Kort : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string SpillerId { get; set; }
        public string Titel { get; set; }
        public string Beskivelse { get; set; }

        public bool Abstract { get; set; }

        //Relationships Kort og tag
        public List<Kort_og_tag> Korts_og_tags { get; set; }

        //Relationships kort og type
        public List<Kort_og_Type> Korts_og_Types { get; set; }

        // Foreign key Sillhuuett
        [Display(Name = "Sillhouette")]
        public int? SillhouetteId { get; set; }
        [ForeignKey("SillhouetteId")]
        public Sillhouette Sillhouette { get; set; }

    }
}
