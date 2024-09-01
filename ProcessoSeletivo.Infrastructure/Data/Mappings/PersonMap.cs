using Microsoft.EntityFrameworkCore;
using ProcessoSeletivo.Domain.Enums;
using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Infrastructure.Data.Mappings
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Person> builder)
        {
            builder
                .ToTable("People");

            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .UseIdentityColumn();

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.CPF)
                .IsRequired()
                .HasMaxLength(14);

            builder
                .Property(p => p.DateOfBirth)
                .IsRequired()
                .HasColumnType("timestamp without time zone");

            builder
                .Property(p => p.Sex)
                .IsRequired()
                .HasConversion(
                   v => (int)v,
                   v => (Gender)v 
               );

            builder
                .HasMany(p => p.Photos)
                .WithOne(ph => ph.Person)
                .HasForeignKey(ph => ph.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
