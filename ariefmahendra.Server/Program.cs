using ariefmahendra.Repositories;
using ariefmahendra.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer("Server=WIN-Q81ECAQ6D6Q;Database=apotek_db; Trusted_Connection=True; Encrypt=False;"));

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IPersistence, Persistence>();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IPurchaseService, PurchaseService>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
   options.AddDefaultPolicy(policyBuilder =>
   {
       policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
   });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

