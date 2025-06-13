using UsersApp.Domain.Enums.Entities;

namespace UsersApp.Application.Dtos;

public record BookDto(string ISBN, string Title, string Author, BookStatus Status, BookCondition Condition, BookGenre Genre);
