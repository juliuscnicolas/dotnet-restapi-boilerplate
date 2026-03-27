using RestApiBoilerplate.Domain.Enums;

namespace RestApiBoilerplate.Application.Contracts;

public sealed record PersonDto(Guid Id, string Name, DateOnly BirthDate, Gender Gender);