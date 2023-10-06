using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Domain.Models.Courses
{

    public class CourseEnrollmentBase
    {
        public int CourseId { get; set; }
        public DateTime TimeStamep { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
    }
    public class CourseEnrollment: CourseEnrollmentBase
    {

    }
}
