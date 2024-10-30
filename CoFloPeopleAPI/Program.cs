using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// dbContext "PeopleInMemoryDb" is the name of the in-memory database
builder.Services.AddDbContext<CoFloPeopleAPIContext>(options =>
    options.UseInMemoryDatabase("PeopleInMemoryDb"));

// add services for DI
builder.Services.AddScoped<IPeopleManagement, PeopleManagement>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Define a CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5575")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfilePersonModel));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) ALWAYS USE SWAGGER
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

// Apply the CORS policy
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
