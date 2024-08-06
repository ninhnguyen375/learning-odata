// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using theliner_api.Models;

// namespace theliner_api.Configurations
// {
//     public class ProductConfiguration : IEntityTypeConfiguration<Product>
//     {
//         public void Configure(EntityTypeBuilder<Product> builder)
//         {
//             builder.ToTable("Products");
//             builder.HasKey(e => e.Id);

//             // init data
//             builder.HasData(new Product { Id = 1, Name = "Nguyen Van A" });
//         }
//     }
// }
