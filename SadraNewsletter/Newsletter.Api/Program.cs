using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newsletter.Api.Auth;
using Newsletter.Api.Mappings;

var builder = WebApplication.CreateBuilder(args);

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
