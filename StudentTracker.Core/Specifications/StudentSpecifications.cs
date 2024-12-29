using StudentTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Specifications
{
    public class StudentSpecifications : BaseSpecifications<Student>
    {
        public StudentSpecifications(string id)
            : base(s => s.NationalId == id)
        {
            Includes.Add(s => s.Courses);
        }
    }
}
