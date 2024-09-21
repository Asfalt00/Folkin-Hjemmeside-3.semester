using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Folkin.Models
{
    public class Type 
    {
        [Key]
        public int Id { get; set; }
        public string Titel { get; set; }
        //Relationships
        public List<Kort_og_Type> Korts_og_Types { get; set; }
    }
}
