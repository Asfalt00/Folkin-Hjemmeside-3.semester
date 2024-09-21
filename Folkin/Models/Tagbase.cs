using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Folkin.Models
{
    public class Tagbase
    {
        [Key]
        public int Id { get; set; }
        public string Titel { get; set; }
        public string IconURL { get; set; }
    }
}
