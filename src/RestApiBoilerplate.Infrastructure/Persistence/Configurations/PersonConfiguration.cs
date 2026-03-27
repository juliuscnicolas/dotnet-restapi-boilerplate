using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiBoilerplate.Domain.Entities;

namespace RestApiBoilerplate.Infrastructure.Persistence.Configurations;

public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("People");

        builder.HasKey(person => person.Id);

        builder.Property(person => person.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(person => person.BirthDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(person => person.Gender)
            .HasConversion<int>()
            .IsRequired();
    }
}