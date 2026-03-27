using RestApiBoilerplate.Domain.Entities;

namespace RestApiBoilerplate.Application.Abstractions;

public interface IPersonRepository
{
    Task<IReadOnlyCollection<Person>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Person person, CancellationToken cancellationToken = default);

    Task UpdateAsync(Person person, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}