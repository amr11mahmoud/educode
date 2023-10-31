using Educode.Application.Services.Users.Commands.CreateUser;
using Educode.Core.Dtos.Users;
using Educode.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Educode.Presentation.Controllers.Users
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsersController : ApplicationController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="request">The user data for creation.</param>
        /// <response code="200">user ID.</response>
        /// <response code="400">bad request.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponseDto), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<ActionResult> Create(CreateUserCommand request)
        {
            var userCreatedResult = await _mediator.Send(request);

            if (userCreatedResult.IsFailure)
            {
                return BadRequest(userCreatedResult.Error);
            }

            var response = new CreateUserResponseDto { UserId = userCreatedResult.Value };

            return Created(userCreatedResult.Value.ToString(), response);
        }
    }
}
