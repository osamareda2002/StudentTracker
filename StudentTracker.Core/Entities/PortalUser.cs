using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Entities
{
    public class PortalUser
    {
        [Key]
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
        //Navigation Properties
        public virtual ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();
    }
}
