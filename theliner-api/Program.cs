using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using theliner_api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Cấu hình dịch vụ Entity Framework Core với SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Cấu hình dịch vụ OData
builder
    .Services.AddControllers()
    .AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel()).EnableQueryFeatures(2000));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

static IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    var dbContextType = typeof(ApplicationDbContext);

    foreach (var property in dbContextType.GetProperties())
    {
        if (
            property.PropertyType.IsGenericType
            && property.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
        )
        {
            var entityType = property.PropertyType.GetGenericArguments()[0];
            var entitySetName = property.Name;
            odataBuilder
                .GetType()
                .GetMethod(nameof(ODataConventionModelBuilder.EntitySet))
                .MakeGenericMethod(entityType)
                .Invoke(odataBuilder, new object[] { entitySetName });
        }
    }

    return odataBuilder.GetEdmModel();
}
