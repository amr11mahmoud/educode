using Educode.Core.Interfaces.Users;
using Educode.Domain.Shared;
using MediatR;

namespace Educode.Application.Services.Abstract
{
    public interface IBaseCommandQuery<T> : IRequest<Result<T>>, IUserId
    {

    }
}
