using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Entities
{
    public class Hall : BaseEntity
    {
        public string Name { get; set; }
        public string Building { get; set; }
        
        //Navigation Properties
        public virtual ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();
        public virtual ICollection<Nut> Nuts { get; set; } = new HashSet<Nut>();

    }
}
