using Microsoft.EntityFrameworkCore;
using TaskBoard.API;
using TaskBoard.API.Contracts;
using TaskBoard.API.Database;
using TaskBoard.API.Dtos;
using TaskBoard.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<TaskBoardContext>(op =>
    {
        op.UseNpgsql(builder.Configuration.GetConnectionString("localPg"));
    });
}

builder.Services.AddScoped<IBaseEntityService<CardDto>, CardsService>();
builder.Services.AddScoped<ListService>();
builder.Services.AddScoped<LogService>();
builder.Services.AddScoped<IBaseEntityService<BoardDto>, BoardsService>();


builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors("AllowAngularDev");

app.UseHttpsRedirection();

app.Run();