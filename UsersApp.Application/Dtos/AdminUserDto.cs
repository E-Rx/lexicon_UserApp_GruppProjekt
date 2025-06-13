namespace UsersApp.Application.Dtos;

public record AdminUserDto(string Id, string UserName, string DisplayName, string Email, string FirstName, string LastName, DateTime LastLogin, DateTime DateOfCreation);

