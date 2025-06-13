using UsersApp.Domain.Enums.Entities;

namespace UsersApp.Application.Dtos;

public record LoanDto(Guid Id, string ISBN, string Title, DateTime DueDate);
