using Educode.Domain.Courses;
using Educode.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Domain.Abstractions
{
    public interface ICourseRepository
    {
        void SaveToDb(CourseEnrollment courseEnrollment);

        IEnumerable<Course> GetAvailableCourses(DateTime dateTime);
    }
}
