using Educode.Domain.Models.Auth;

namespace Educode.Domain.Auth
{
    public class RegisterUserProcessorTests
    {
        [Fact]
        public void ShouldReturnUserRegisterResultWithUserDetails()
        {
            // Arrange
            var user = new RegisterUser
            {
                Email = "example@test.com",
                FirstName = "John",
                LastName = "Doe",
                Password = "P@ssw0rd!"
            };

            var userDomainManager = new UserDomainManager();

            // Act
            User registeredUser = userDomainManager.RegisterUser(user);

            //Assert
            Assert.NotNull(registeredUser);
            Assert.Equal(user.FirstName, registeredUser.FirstName);
            Assert.Equal(user.LastName, registeredUser.LastName);
            Assert.Equal(user.Email, registeredUser.Email);
            Assert.Equal(user.Password, registeredUser.PasswordHash);
        }
    }
}
