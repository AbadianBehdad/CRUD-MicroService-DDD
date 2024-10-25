using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation;
using ShopManagement.Application;
using ShopManagement.Application.AutoMapper;
using ShopManagement.Application.CQRS.ProductCommandQuary.Command;
using ShopManagement.Domain.ProductAgg.Contracts;
using ShopManagenment.EFCore;
using ShopManagenment.EFCore.Repository;
using ShopManagment.Contracts.Product;
using UserManagement.Application.CQRS.UserCommandQuary.Command;
using UserManagement.Domain.UserAgg.Contracts;
using UserManagenet.EFCore;
using UserManagenet.EFCore.Repository;
using UserMangement.Utility.Encryption;
using UserMangement.Utility.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOptions();
builder.Services.Configure<Configs>(builder.Configuration.GetSection("Configs"));


// Add services to the container.
builder.Services.AddSwagger();
builder.Services.AddJWT();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IProductRepository ,ProductRepository>();
builder.Services.AddTransient<IProductApplication, ProductApplication>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddSingleton<EncryptionUtility>();
var connectionStringShop = builder.Configuration.GetConnectionString("CRUDT1");
var connectionStringUser = builder.Configuration.GetConnectionString("CRUDT2");
builder.Services.AddDbContext<ShopContext>(x=> x.UseSqlServer(connectionStringShop));
builder.Services.AddDbContext<UserContext>(x => x.UseSqlServer(connectionStringUser));
var config = new AutoMapper.MapperConfiguration(x =>
{
    x.AddProfile(new ProductMapper());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(SaveProductCommand).Assembly);
    options.RegisterServicesFromAssemblies(typeof(SaveUserCommand).Assembly);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
