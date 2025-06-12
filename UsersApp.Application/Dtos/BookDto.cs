using UsersApp.Domain.Enums.Entities;

namespace UsersApp.Application.Dtos;

public record BookDto(string isbn, string title, string author, BookStatus status, BookCondition condition, BookGenre genre);
