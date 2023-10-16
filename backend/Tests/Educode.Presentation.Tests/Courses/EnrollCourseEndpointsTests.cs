//using Educode.Domain.Enums;
//using Educode.Domain.Managers;
//using Educode.Domain.Queries;
//using Moq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Infrastructure;

//namespace Educode.Presentation.Tests.Courses
//{
//    public class EnrollCourseEndpointsTests
//    {
//        private readonly Mock<ICourseProcessor> _courseProcessorMock;
//        private readonly CoursesController _courseController;
//        private CourseEnrollRequest _request;
//        private CourseEnrollResult _result;

//        public EnrollCourseEndpointsTests()
//        {
//            _courseProcessorMock = new Mock<ICourseProcessor>();
//            _courseController = new CoursesController(_courseProcessorMock.Object);
//            _request = new CourseEnrollRequest();

//            _result = new CourseEnrollResult { Code = Domain.Enums.CourseEnrollmentResultCode.Success };

//            _courseProcessorMock.Setup(x => x.Enroll(_request)).Returns(_result);
//        }

//        [Theory]
//        [InlineData(1, true)]
//        [InlineData(0, false)]
//        public void ShouldCallEnrollCourseMethodOfProcessorIfModelIsValid(int expectedEnrollCall, bool isModelValid)
//        {
//            Arrange

//            Act
//            if (!isModelValid)
//            {
//                _courseController.ModelState.AddModelError("key", "error message");
//            }

//            _courseController.Enroll(_request);

//            Assert
//            _courseProcessorMock.Verify(x => x.Enroll(_request), Times.Exactly(expectedEnrollCall));
//        }

//        [Fact]
//        public void ShouldAddModelErrorIfNoCourseAvailable()
//        {
//            Arrange
//            _result.Code = Domain.Enums.CourseEnrollmentResultCode.NoCourseAvailable;

//            Act
//            _courseController.Enroll(_request);

//            Assert
//            var modelStateEntry = Assert.Contains("request.TimeStamep", _courseController.ModelState);
//            var modelError = Assert.Single(modelStateEntry.Errors);
//            Assert.Equal("NotAvailable", modelError.ErrorMessage);
//        }

//        [Fact]
//        public void ShouldNotAddModelErrorIfCourseIsAvailable()
//        {
//            Arrange

//            Act
//            _courseController.Enroll(_request);

//            Assert
//            Assert.DoesNotContain("request.TimeStamep", _courseController.ModelState);
//        }

//        [Theory]
//        [InlineData(StatusCodes.Status400BadRequest, false, null)]
//        [InlineData(StatusCodes.Status404NotFound, true, CourseEnrollmentResultCode.NoCourseAvailable)]
//        [InlineData(StatusCodes.Status200OK, true, CourseEnrollmentResultCode.Success)]
//        public void ShouldReturnExpectedActionResult(int expectedCode, bool isModelValid, CourseEnrollmentResultCode? resultCode)
//        {

//            Arrange
//            if (!isModelValid)
//            {
//                _courseController.ModelState.AddModelError("key", "error message");
//            }

//            if (resultCode.HasValue)
//            {
//                _result.Code = resultCode.Value;
//            }

//            Act
//            IActionResult response = _courseController.Enroll(_request);

//            var statusCodeResult = (IStatusCodeActionResult)response;

//            Assert
//            Assert.Equal(expectedCode, statusCodeResult.StatusCode);
//        }
//    }
//}
