using RestApiBoilerplate.Domain.Enums;

namespace RestApiBoilerplate.Application.Contracts;

public sealed record CreatePersonRequest(string Name, DateOnly BirthDate, Gender Gender);