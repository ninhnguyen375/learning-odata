using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using theliner_api.Models;

namespace theliner_api.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(e => e.Id);

            // init data
            builder.HasData(new User { Id = 1, FullName = "Nguyen Van A" });
        }
    }
}
