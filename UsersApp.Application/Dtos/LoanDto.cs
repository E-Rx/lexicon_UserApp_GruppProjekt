using UsersApp.Domain.Enums.Entities;

namespace UsersApp.Application.Dtos;

public record LoanDto(Guid id, Guid bookId, DateTime dueDate);
