using MGM.Blog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MGM.Blog.Infrastructure.Persistence
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(m => m.FirstName)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(m => m.LastName)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(m => m.Password)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(m => m.Email)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(m => m.TaxDocument)
               .IsRequired()
               .HasMaxLength(11);

            builder.Property(m => m.BirthDate)
               .IsRequired();

            builder.Property(m => m.LastModified)
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Property(m => m.Created)
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.HasMany(m => m.Posts).WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
