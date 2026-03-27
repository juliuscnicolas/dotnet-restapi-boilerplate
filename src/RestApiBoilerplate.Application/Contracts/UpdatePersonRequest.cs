using RestApiBoilerplate.Domain.Enums;

namespace RestApiBoilerplate.Application.Contracts;

public sealed record UpdatePersonRequest(string Name, DateOnly BirthDate, Gender Gender);