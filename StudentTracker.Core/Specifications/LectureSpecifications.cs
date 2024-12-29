using StudentTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Specifications
{
    public class LectureSpecifications: BaseSpecifications<Lecture>
    {
        public LectureSpecifications(string id, string day)
            : base()
        {
            Includes.Add(L => L.Professor);
            Includes.Add(L => L.Course);
            Includes.Add(L => L.Hall);
            Criteria = L => L.Course.Students.Any(s => s.NationalId == id ) && L.Day.Equals(day);
        }
    }
}
