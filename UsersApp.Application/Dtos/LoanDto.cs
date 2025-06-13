using UsersApp.Domain.Enums.Entities;

namespace UsersApp.Application.Dtos;

public record LoanDto(Guid id, string ISBN, string Title, DateTime dueDate);
