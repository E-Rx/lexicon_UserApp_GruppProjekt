namespace UsersApp.Application.Dtos;

public record ResultDto(string? ErrorMessage)
{
    public bool Succeeded => ErrorMessage == null;
}
