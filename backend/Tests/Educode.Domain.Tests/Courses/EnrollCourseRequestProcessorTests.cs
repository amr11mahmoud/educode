using Educode.Domain.Enums;
using Educode.Domain.Interfaces;
using Educode.Domain.Managers;
using Educode.Domain.Queries;
using Moq;

namespace Educode.Domain.Courses
{
    public class EnrollCourseRequestProcessorTests
    {
        private List<Course> _availableCourses;
        private readonly Mock<ICourseRepository> _courseRepoMock;
        private readonly CourseProcessor _processor;
        private readonly CourseEnrollRequest _request;

        public EnrollCourseRequestProcessorTests()
        {
            _request = new CourseEnrollRequest
            {
                CourseId = 10,
                TimeStamep = DateTime.Now,
                UserId = 1,
                FirstName = "Amr",
                LastName = "Mahmoud"
            };

            _availableCourses = new List<Course> { new Course { CourseId = 10 } };

            _courseRepoMock = new Mock<ICourseRepository>();

            _courseRepoMock.Setup(x => x.GetAvailableCourses(_request.TimeStamep)).Returns(_availableCourses);

            _processor = new CourseProcessor(_courseRepoMock.Object);
        }

        [Fact]
        public void ShouldReturnUserEnrolledToCourseResultWithCourseDetails()
        {
            // Act
            CourseEnrollResult courseResult = _processor.Enroll(_request);

            //Assert
            Assert.NotNull(courseResult);
            Assert.Equal(_request.FirstName, courseResult.FirstName);
            Assert.Equal(_request.LastName, courseResult.LastName);
            Assert.Equal(_request.TimeStamep, courseResult.TimeStamep);
            Assert.Equal(_request.CourseId, courseResult.CourseId);
            Assert.Equal(_request.UserId, courseResult.UserId);
        }


        [Fact]
        public void ShouldThrowArgumentExceptionIfNoArgumentsPassed()
        {
            //Act, Assert
            var exception = Assert.Throws<ArgumentNullException>(() => { _processor.Enroll(null); });

            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveCourseEnrollment()
        {
            //Arrange
            CourseEnrollment? courseEnrollment = null;
            _courseRepoMock.Setup(x => x.SaveToDb(It.IsAny<CourseEnrollment>())).Callback<CourseEnrollment>(courseEnroll =>
            {
                courseEnrollment = courseEnroll;
            });

            //Act
            _processor.Enroll(_request);
            _courseRepoMock.Verify(x => x.SaveToDb(It.IsAny<CourseEnrollment>()), Times.Once);

            //Assert
            Assert.NotNull(courseEnrollment);
            Assert.Equal(_request.FirstName, courseEnrollment.FirstName);
            Assert.Equal(_request.LastName, courseEnrollment.LastName);
            Assert.Equal(_request.TimeStamep, courseEnrollment.TimeStamep);
            Assert.Equal(_request.CourseId, courseEnrollment.CourseId);
            Assert.Equal(_request.UserId, courseEnrollment.UserId);
            Assert.Equal(_availableCourses.First().CourseId, courseEnrollment.CourseId);
        }

        [Fact]
        public void ShouldNotSaveCourseEnrollmentIfCourseIsNotExist()
        {
            _availableCourses.Clear();
            _processor.Enroll(_request);
            _courseRepoMock.Verify(x => x.SaveToDb(It.IsAny<CourseEnrollment>()), Times.Never);
        }

        [Fact]
        public void ShouldNotSaveCourseEnrollmentIfNoCourseIdPresentedInRequest()
        {
            _request.CourseId = 0;
            _processor.Enroll(_request);
            _courseRepoMock.Verify(x => x.SaveToDb(It.IsAny<CourseEnrollment>()), Times.Never);
        }

        [Theory]
        [InlineData(CourseEnrollmentResultCode.Success, true)]
        [InlineData(CourseEnrollmentResultCode.NoCourseAvailable, false)]
        public void ShouldReturnExpectedResultCode(CourseEnrollmentResultCode expectedResultCode, bool isCourseAvailable)
        {
            if (!isCourseAvailable)
            {
                _availableCourses.Clear();
            }

            var result = _processor.Enroll(_request);

            Assert.Equal(expectedResultCode, result.Code);
        }

        [Theory]
        [InlineData(5, true)]
        [InlineData(null, false)]
        public void ShouldReturnExpectedCourseEnrollmentIdCode(int? expectedCourseEnrollmentId, bool isCourseAvailable)
        {
            if (!isCourseAvailable)
            {
                _availableCourses.Clear();
            }
            else
            {
                _courseRepoMock.Setup(x => x.SaveToDb(It.IsAny<CourseEnrollment>())).Callback<CourseEnrollment>(courseEnrollment =>
                {
                    courseEnrollment.Id = expectedCourseEnrollmentId.Value;
                });
            }

            var result = _processor.Enroll(_request);

            Assert.Equal(expectedCourseEnrollmentId, result.CourseEnrollmentId);
        }
    }
}
