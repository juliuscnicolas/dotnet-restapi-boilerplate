using RestApiBoilerplate.Application.Contracts;

namespace RestApiBoilerplate.Application.Abstractions;

public interface IPersonService
{
    Task<IReadOnlyCollection<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PersonDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PersonDto> CreateAsync(CreatePersonRequest request, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}