using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Folkin.Models
{
    public class Kort_og_Type
    {
        public int KortId { get; set; }
        [ForeignKey("KortId")]
        public Kort Kort { get; set; }

        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public Type Type { get; set; }
    }
}
