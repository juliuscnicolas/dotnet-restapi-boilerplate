using RestApiBoilerplate.Application.Abstractions;
using RestApiBoilerplate.Application.Contracts;
using RestApiBoilerplate.Domain.Entities;

namespace RestApiBoilerplate.Application.Services;

public sealed class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<IReadOnlyCollection<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var people = await _personRepository.GetAllAsync(cancellationToken);

        return people
            .Select(MapToDto)
            .ToArray();
    }

    public async Task<PersonDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var person = await _personRepository.GetByIdAsync(id, cancellationToken);
        return person is null ? null : MapToDto(person);
    }

    public async Task<PersonDto> CreateAsync(CreatePersonRequest request, CancellationToken cancellationToken = default)
    {
        var person = new Person(Guid.NewGuid(), request.Name, request.BirthDate, request.Gender);
        await _personRepository.AddAsync(person, cancellationToken);
        return MapToDto(person);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken = default)
    {
        var person = await _personRepository.GetByIdAsync(id, cancellationToken);

        if (person is null)
        {
            return false;
        }

        person.Update(request.Name, request.BirthDate, request.Gender);
        await _personRepository.UpdateAsync(person, cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var person = await _personRepository.GetByIdAsync(id, cancellationToken);

        if (person is null)
        {
            return false;
        }

        await _personRepository.DeleteAsync(id, cancellationToken);

        return true;
    }

    private static PersonDto MapToDto(Person person)
        => new(person.Id, person.Name, person.BirthDate, person.Gender);
}