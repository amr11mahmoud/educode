using AutoMapper;
using Educode.Application.Extensions;
using Educode.Application.Services.Abstract;
using Educode.Application.Services.Abstract.Commands;
using Educode.Application.Services.Abstract.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Educode.Presentation.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;
        public ApplicationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        protected virtual async Task<ActionResult<T1>> BaseActionMethod<T, T1, T2>(T request, bool requireMapping = false) where T : IBaseCommandQuery<T2> where T1 : class
        {
            request.AddUserId(HttpContext);

            var result = await _mediator.Send(request);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            T1? response;

            if (requireMapping)
            {
                //auto map from T2 (command/query handler return result generic type) to T1
                response = _mapper.Map<T1>(result.Value);
            }
            else
            {
                response = (T1?)Activator.CreateInstance(typeof(T1), result.Value);
            }

            return Ok(response);
        }

        /// <summary>
        /// base generic method to handle CREATE action methods
        /// - append userId to the request body
        /// - call appropriate handler using mediator
        /// - return proper status code and payload
        /// </summary>
        /// <typeparam name="T">create command type eg.createUserCommand </typeparam>
        /// <typeparam name="T1">response type eg.createUserResponseDto </typeparam>
        /// <typeparam name="T2">mediator result generic type eg.guid result<Guid> </typeparam>
        /// <param name="request">create command object</param>
        /// <returns></returns>
        protected virtual async Task<ActionResult<T1>> Create<T, T1, T2>(T request) where T : ICreateCommand<T2> where T1 : class
        {
            return await BaseActionMethod<T, T1, T2>(request);
        }

        /// <summary>
        /// base generic method to handle UPDATE action methods
        /// - append userId to the request body
        /// - call appropriate handler using mediator
        /// - return proper status code and payload
        /// </summary>
        /// <typeparam name="T">update command type eg.updateUserCommand </typeparam>
        /// <typeparam name="T1">response type eg.updateUserResponseDto </typeparam>
        /// <typeparam name="T2">mediator result generic type eg.guid result<Guid> </typeparam>
        /// <param name="request">update command object</param>
        /// <returns></returns>
        protected virtual async Task<ActionResult<T1>> Update<T, T1, T2>(T request) where T : IUpdateCommand<T2> where T1 : class
        {
            return await BaseActionMethod<T, T1, T2>(request);
        }

        /// <summary>
        /// base generic method to handle READ action methods
        /// - append userId to the request body
        /// - call appropriate handler using mediator
        /// - return proper status code and payload
        /// </summary>
        /// <typeparam name="T">read command type eg.readUserCommand </typeparam>
        /// <typeparam name="T1">response type eg.readUserResponseDto </typeparam>
        /// <typeparam name="T2">mediator result generic type eg.user dto result<userDto> </typeparam>
        /// <param name="request">read command object</param>
        /// <returns></returns>
        protected virtual async Task<ActionResult<T1>> Read<T, T1, T2>(T request, bool requireMapping = false) where T : IReadQuery<T2> where T1 : class
        {
            return await BaseActionMethod<T, T1, T2>(request, requireMapping);
        }
    }
}
