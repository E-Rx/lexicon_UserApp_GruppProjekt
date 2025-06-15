using Moq;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces;
using UsersApp.Application.Interfaces.Users;
using UsersApp.Application.Services.Users;




namespace UsersApp.Application.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUserAsync_ShouldReturnSuccess()
        {
            // Arrange
            var mockIdentityService = new Mock<IIdentityUserService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var expectedResult = new ResultDto(null); 

            var userDto = new UserDto("user", "email@example.com", "Toto", "Titi", ""); 
            string password = "Password_123";

            mockIdentityService
                .Setup(s => s.CreateUserAsync(userDto, password))
                .ReturnsAsync(expectedResult);

            var userService = new UserService(mockIdentityService.Object, mockUnitOfWork.Object);

            // Act
            var result = await userService.CreateUserAsync(userDto, password);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.ErrorMessage);
        }


        [Fact]
        public async Task GetUserDtoById_ShouldReturnCorrectUser()
        {
            // Arrange
            var mockIdentityService = new Mock<IIdentityUserService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var userId = "id123";
            var expectedUser = new UserDto(
                UserName: "user123",
                DisplayName: "Toto",   
                Email: "user123@mail.com",
                FirstName: "Toto",
                LastName: "Titi"
            );

            mockUserRepository
                .Setup(repo => repo.GetUserDtoById(userId))
                .ReturnsAsync(expectedUser);

            mockUnitOfWork
                .Setup(uow => uow.UserRepository)
                .Returns(mockUserRepository.Object);

            var userService = new UserService(mockIdentityService.Object, mockUnitOfWork.Object);

            // Act
            var result = await userService.GetUserDtoById(userId);

            // Assert
            Assert.Equal("user123", result.UserName);
            Assert.Equal("user123@mail.com", result.Email);
            Assert.Equal("Toto", result.FirstName);
            Assert.Equal("Titi", result.LastName);

        }


        [Fact]
        public async Task GetAllWithId_ShouldReturnAllAdminUsers()
        {
            // Arrange
            var mockIdentityService = new Mock<IIdentityUserService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var expectedUsers = new[]
            {
        new AdminUserDto("id1", "user1", "User One", "user1@mail.com", "First1", "Last1", DateTime.Now, DateTime.Now),
        new AdminUserDto("id2", "user2", "User Two", "user2@mail.com", "First2", "Last2", DateTime.Now, DateTime.Now)
    };

            mockUserRepository
                .Setup(repo => repo.GetAllWithId())
                .ReturnsAsync(expectedUsers);

            mockUnitOfWork
                .Setup(uow => uow.UserRepository)
                .Returns(mockUserRepository.Object);

            var userService = new UserService(mockIdentityService.Object, mockUnitOfWork.Object);

            // Act
            var result = await userService.GetAllWithId();

            // Assert
            Assert.Equal(expectedUsers.Length, result.Length);
            Assert.Equal(expectedUsers, result);
        }

    }


}



