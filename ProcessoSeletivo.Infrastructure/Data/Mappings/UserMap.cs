using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessoSeletivo.Domain.Models;
using ProcessoSeletivo.Domain.Enums;

namespace ProcessoSeletivo.Infrastructure.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("Users");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .UseIdentityColumn();

            builder
                .HasIndex(u => u.Name)
                .IsUnique();

            builder
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(u => u.Role)
                .IsRequired()
                .HasConversion(
                   v => (int)v,
                   v => (Role)v 
               );
        }
    }
}
