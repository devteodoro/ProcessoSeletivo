using Microsoft.EntityFrameworkCore;
using ProcessoSeletivo.Domain.Models;

namespace ProcessoSeletivo.Infrastructure.Data.Mappings
{
    public class PhotoMap : IEntityTypeConfiguration<Photo>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Photo> builder)
        {
            builder
                .ToTable("Photos");

            builder
                .HasKey(ph => ph.Id);

            builder
                 .Property(p => p.Id)
                 .UseIdentityColumn();

            builder
                .Property(ph => ph.Image)
                .IsRequired();

            builder
                .Property(ph => ph.Current)
                .IsRequired();

            builder
                .HasOne(ph => ph.Person)
                .WithMany(p => p.Photos)
                .HasForeignKey(ph => ph.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
