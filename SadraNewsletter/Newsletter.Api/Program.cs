using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Newsletter.Api.Auth;
using Newsletter.Api.Mappings;
using Sadra.Newsletter.Application.IDatabaseContexts;
using Sadra.Newsletter.Application.Services;
using Sadra.Newsletter.Persistence.DatabaseContexts;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<INewsletterDbContext, NewsletterDbContext>();
builder.Services.AddScoped<INewsletterService, NewsletterService>();
string connection = builder.Configuration.GetSection("SqlConnection:newsletterDb").Value;
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<NewsletterDbContext>(

    option => option.UseSqlServer(connection)
    ); ;
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string sqlConnectionStr = builder.Configuration.GetValue<string>("SqlConnection:newsletterDb");
bool authEnabled = builder.Configuration.GetValue<bool>("Auth:Enabled");

var authConfig = builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

if (authEnabled)
    authConfig.AddJwtAuth(builder.Configuration);
else
    authConfig.AddBypassAuth();

builder.Services.AddPolicies();

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<APIProfile>();
});
builder.Services.AddSingleton<IMapper>(x => new Mapper(config));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
