using RestApiBoilerplate.Domain.Enums;

namespace RestApiBoilerplate.Domain.Entities;

public sealed class Person
{
    private Person()
    {
        Name = string.Empty;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public Gender Gender { get; private set; }

    public Person(Guid id, string name, DateOnly birthDate, Gender gender)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        Id = id;
        Name = name.Trim();
        BirthDate = birthDate;
        Gender = gender;
    }

    public void Update(string name, DateOnly birthDate, Gender gender)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        Name = name.Trim();
        BirthDate = birthDate;
        Gender = gender;
    }
}