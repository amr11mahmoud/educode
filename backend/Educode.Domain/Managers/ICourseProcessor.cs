using Educode.Domain.Queries;

namespace Educode.Domain.Managers
{
    public interface ICourseProcessor
    {
        CourseEnrollResult Enroll(CourseEnrollRequest request);
    }
}