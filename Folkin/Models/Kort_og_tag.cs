using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Folkin.Models
{
    public class Kort_og_tag
    {
        public int KortId { get; set; }
        [ForeignKey("KortId")]
        public Kort Korts { get; set; }

        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag Tags { get; set; }

    }
}
