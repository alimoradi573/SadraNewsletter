using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Newsletter.Api.Auth
{
    public static class AuthExtensions
    {

        public static AuthenticationBuilder AddJwtAuth(this AuthenticationBuilder builder, IConfiguration config)
        {
            // var signingKey = LoadKey(config);

            builder.AddJwtBearer(options =>
             {
                 options.IncludeErrorDetails = true;
                 options.RequireHttpsMetadata = false;

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     //IssuerSigningKey = signingKey,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Auth:PublicKey"])),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     RequireExpirationTime = false,
                 };

                 options.Validate();
             });

            return builder;
        }

        public static AuthenticationBuilder AddBypassAuth(this AuthenticationBuilder builder)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(new byte[121]);
            var sc = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("scope", $"{Permissions.Admin} " +
                    $" {Permissions.Author}" +
                    $" {Permissions.Viewer}")
                }),
                SigningCredentials = sc,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            builder.AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                };
                options.RequireHttpsMetadata = false;
                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = tokenString;
                        return Task.CompletedTask;
                    }
                };
                options.Validate();
            });

            return builder;
        }

        public static void AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Admin, policy => policy.RequireAssertion(context => context.User.Claims.Any(x => x.Type == "scope" && x.Value.Split(' ').Contains(Permissions.Admin))));
                options.AddPolicy(Policies.Author, policy => policy.RequireAssertion(context => context.User.Claims.Any(x => x.Type == "scope" && x.Value.Split(' ').Contains(Permissions.Author))));
                options.AddPolicy(Policies.Viewer, policy => policy.RequireAssertion(context => context.User.Claims.Any(x => x.Type == "scope" && x.Value.Split(' ').Contains(Permissions.Viewer))));
            });
        }

        public static SecurityKey LoadKey(IConfiguration config)
        {
            string publicKeyBase64 = config.GetSection("Auth").GetValue<string>("PublicKey");
            var publicKey = Convert.FromBase64String(publicKeyBase64);

            string algName = config.GetSection("Auth").GetValue<string>("Algorithm");

            if (algName.StartsWith("RS"))
            {
                var rsa = RSA.Create();
                try
                {
                    rsa.ImportSubjectPublicKeyInfo(publicKey, out _);
                }
                catch
                {
                    rsa.ImportRSAPublicKey(publicKey, out _);
                }
                return new RsaSecurityKey(rsa);
            }

            if (algName.StartsWith("ES"))
            {
                var e1 = ECDsa.Create();
                e1.ImportSubjectPublicKeyInfo(publicKey, out _);
                return new ECDsaSecurityKey(e1);
            }

            throw new ArgumentException("Only RSA and ECDSA algorithms are supported");
        }
    }

}
