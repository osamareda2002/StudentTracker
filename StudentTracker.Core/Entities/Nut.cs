using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Entities
{
    public class Nut:BaseEntity
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int SignalStrength { get; set; }

        //Foreign key
        public int HallId { get; set; }

        //Navigation Properties
        public virtual Hall Hall { get; set; }
    }

}
