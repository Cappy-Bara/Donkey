using Donkey.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Donkey.Infrastructure.Database.Configurations
{
    public class BlogsConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(x => x.Name);

            builder.Property(x => x.OwnerEmail)
                .IsRequired();

            builder.HasMany(x => x.Posts).WithOne().HasForeignKey(x => x.BlogName);
        }
    }
}
