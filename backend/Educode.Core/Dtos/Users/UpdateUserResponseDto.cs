namespace Educode.Core.Dtos.Users
{
    public class UpdateUserResponseDto : CreateUserResponseDto
    {
        public UpdateUserResponseDto(Guid userId) : base(userId) { }

    }
}
