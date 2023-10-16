using Educode.Domain.Courses;
using Educode.Domain.Abstractions;
using Educode.Domain.Queries;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Domain.Managers
{
    public class CourseProcessor : ICourseProcessor
    {
        private ICourseRepository _courseRepository;

        public CourseProcessor(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public CourseEnrollResult Enroll(CourseEnrollRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var availableCourses = _courseRepository.GetAvailableCourses(request.TimeStamep);

            var result = Create<CourseEnrollResult>(request);

            if (availableCourses.Any(x => x.CourseId == request.CourseId))
            {
                var courseEnrollment = Create<CourseEnrollment>(request);
                courseEnrollment.CourseId = request.CourseId;

                _courseRepository.SaveToDb(courseEnrollment);

                result.Code = Enums.CourseEnrollmentResultCode.Success;
                result.CourseEnrollmentId = courseEnrollment.Id;
            }
            else
            {
                result.Code = Enums.CourseEnrollmentResultCode.NoCourseAvailable;
                result.CourseEnrollmentId = null;
            }

            return result;
        }

        private static T Create<T>(CourseEnrollRequest request) where T : CourseEnrollmentBase, new()
        {
            return new T
            {
                CourseId = request.CourseId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                TimeStamep = request.TimeStamep,
                UserId = request.UserId,
            };
        }
    }
}
