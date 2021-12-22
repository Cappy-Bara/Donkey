using Donkey.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Infrastructure.Database.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Email);

            builder.Property(x => x.Email)
                .IsRequired();
            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.HasMany(x => x.Blogs).WithOne().HasForeignKey(x => x.OwnerEmail);
        }
    }
}
