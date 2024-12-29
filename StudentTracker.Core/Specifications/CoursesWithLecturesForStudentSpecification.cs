using StudentTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Specifications
{
    public class CoursesWithLecturesForStudentSpecification : BaseSpecifications<Course>
    {
        public CoursesWithLecturesForStudentSpecification(string id)
        { //
            Includes.Add(c => c.Students); 
            Includes.Add(c => c.Lectures);
            Criteria = c => c.Students.Any(s => s.NationalId == id);

        }
    }
}
