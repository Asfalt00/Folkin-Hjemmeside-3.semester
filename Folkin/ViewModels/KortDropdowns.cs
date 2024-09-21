using Folkin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Folkin.ViewModels
{
    public class KortDropdowns
    {
        public List<Tag> Tags { get; set; }
        public List<Type> Types { get; set; }
        public List<Sillhouette> Sillhouettes { get; set; }
        public KortDropdowns()
        {
            Tags = new List<Tag>();
            Types = new List<Type>();
            Sillhouettes = new List<Sillhouette>();
        }
    }
}
