using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Folkin.Models
{
    public class Tag: Tagbase
    {
        public string Type { get; set; }

        //Relationships
        public List<Kort_og_tag> Korts_og_tags { get; set; }

    }
}
