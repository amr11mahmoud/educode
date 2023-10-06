using Educode.Domain.Enums;
using Educode.Domain.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Domain.Queries
{
    public class CourseEnrollResult : CourseEnrollmentBase
    {
        public CourseEnrollmentResultCode Code { get; set; }
        public int? CourseEnrollmentId { get; set; }
    }
}
