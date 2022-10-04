using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Qna.DAL.DBContext;
using Qna.DAL.Generic;
using Qna.DAL.Repos;
using Qna.DAL.Repos_Interfaces;
using QnA.BAL.Contract;
using QnA.BAL.Implementation;
using QnA.BAL.Services;
using QnA.Extensions;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
    );

StartupExtensions.AddQnAAuthentication(builder.Services, builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
StartupExtensions.AddQnASwagger(builder.Services, "QnA", "V1", $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
StartupExtensions.AddQnADependencies(builder.Services);


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
