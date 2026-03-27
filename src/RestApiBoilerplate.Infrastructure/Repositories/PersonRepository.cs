using Microsoft.EntityFrameworkCore;
using RestApiBoilerplate.Application.Abstractions;
using RestApiBoilerplate.Domain.Entities;
using RestApiBoilerplate.Infrastructure.Persistence;

namespace RestApiBoilerplate.Infrastructure.Repositories;

public sealed class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _dbContext;

    public PersonRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.People
            .AsNoTracking()
            .OrderBy(person => person.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.People
            .FirstOrDefaultAsync(person => person.Id == id, cancellationToken);
    }

    public async Task AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        await _dbContext.People.AddAsync(person, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        _dbContext.People.Update(person);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var person = await _dbContext.People.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (person is null)
        {
            return;
        }

        _dbContext.People.Remove(person);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}