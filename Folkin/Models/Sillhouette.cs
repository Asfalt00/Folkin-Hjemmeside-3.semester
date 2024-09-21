using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Folkin.Models
{
    public class Sillhouette : Tagbase
    {
        //Relationships
        public List<Kort> Korts { get; set; }
    }
}
