using Educode.Domain.Managers;
using Educode.Domain.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Educode.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private ICourseProcessor _courseProcessor;

        public CoursesController(ICourseProcessor courseProcessor)
        {
            _courseProcessor = courseProcessor;
        }

        [HttpPost]
        public IActionResult Enroll(CourseEnrollRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = _courseProcessor.Enroll(request);

                if (result.Code == Domain.Enums.CourseEnrollmentResultCode.NoCourseAvailable)
                {
                    ModelState.AddModelError("request.TimeStamep", "NotAvailable");

                    return NotFound();
                }
                
                return Ok();
            }

            return BadRequest();
        }
    }
}
