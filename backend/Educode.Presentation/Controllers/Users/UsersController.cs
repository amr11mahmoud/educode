using AutoMapper;
using Educode.Application.Extensions;
using Educode.Application.Services.Users.Commands.CreateUser;
using Educode.Application.Services.Users.Commands.LoginUser;
using Educode.Application.Services.Users.Commands.LogoutUser;
using Educode.Application.Services.Users.Commands.UpdateUser;
using Educode.Application.Services.Users.Queries.ReadUser;
using Educode.Core.Dtos.Users;
using Educode.Domain.Shared;
using Educode.Domain.Users.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educode.Presentation.Controllers.Users
{

    [ApiVersion("1.0")]
    public class UsersController : ApplicationController
    {
        public UsersController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }


        [HttpPost]
        [ProducesResponseType(typeof(ReadUserResponseDto), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<ActionResult<ReadUserResponseDto>> Get()
        {
            ActionResult<ReadUserResponseDto> result = await base.Read<ReadUserQuery, ReadUserResponseDto, User>(new ReadUserQuery(), true);
            return result;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateUserResponseDto), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<ActionResult<CreateUserResponseDto>> Create(CreateUserCommand request)
        {
            ActionResult<CreateUserResponseDto> result = await base.Create<CreateUserCommand, CreateUserResponseDto, Guid>(request);
            return result;
        }


        [HttpPut]
        [ProducesResponseType(typeof(UpdateUserResponseDto), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<ActionResult<UpdateUserResponseDto>> Update(UpdateUserCommand request)
        {
            ActionResult<UpdateUserResponseDto> result = await base.Update<UpdateUserCommand, UpdateUserResponseDto, Guid>(request);
            return result;
        }


        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginUserResponseDto), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<ActionResult> Login(LoginUserCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var response = new LoginUserResponseDto { Tokens = result.Value };

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<ActionResult> Logout(LogoutUserCommand request)
        {
            request.AddHttpContext(HttpContext);

            var result = await _mediator.Send(request);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
