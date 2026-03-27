using System.Collections.Concurrent;
using RestApiBoilerplate.Application.Abstractions;
using RestApiBoilerplate.Domain.Entities;

namespace RestApiBoilerplate.Infrastructure.Repositories;

public sealed class InMemoryPersonRepository : IPersonRepository
{
    private readonly ConcurrentDictionary<Guid, Person> _store = new();

    public Task<IReadOnlyCollection<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<Person> people = _store.Values
            .OrderBy(person => person.Name)
            .ToArray();

        return Task.FromResult(people);
    }

    public Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _store.TryGetValue(id, out var person);
        return Task.FromResult(person);
    }

    public Task AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        _store[person.Id] = person;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        _store[person.Id] = person;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _store.TryRemove(id, out _);
        return Task.CompletedTask;
    }
}