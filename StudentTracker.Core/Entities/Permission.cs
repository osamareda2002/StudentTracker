using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Entities
{
    public class Permission: BaseEntity
    {
        public string Name { get; set; }
        //Navigation Properties
        public virtual ICollection<PortalUser> PortalUsers { get; set; } = new HashSet<PortalUser>();
    }
}
