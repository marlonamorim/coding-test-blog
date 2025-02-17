using MGM.Blog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MGM.Blog.Infrastructure.Persistence
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            builder.HasKey(x => x.Id);

            builder.Property(m => m.Title)
               .IsRequired()
               .HasMaxLength(1000);

            builder.Property(m => m.Text)
               .IsRequired()
               .HasMaxLength(int.MaxValue);

            builder.Property(m => m.LastModified)
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Property(m => m.Created)
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.HasOne(m => m.User).WithMany(m => m.Posts)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
