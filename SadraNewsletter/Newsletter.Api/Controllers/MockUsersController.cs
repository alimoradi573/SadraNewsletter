using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Newsletter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockUsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public MockUsersController(
            IConfiguration configuration,
            IMapper mapper)
        {
            _configuration = configuration;
            
            _mapper = mapper;
        }
        [HttpGet("GetMockUsers")]
        public ActionResult<List<MockUsers>> GetMockUsers()
        {
            return Ok(_mockUsers);
        }

        [HttpGet("GenerateToken")]
        public string GenerateToken(string email)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:PublicKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string policy = SetPolicy(email);

            var claims = new[]
            {
                 new Claim("email",email),
                 new Claim("scope",policy)
            };
            var token = new JwtSecurityToken(_configuration["Auth:Issuer"],
                _configuration["Auth:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        #region Privates
        private string SetPolicy(string email)
        {
            string PolicyKey = _mockUsers.SingleOrDefault(c => c.Email.ToLower().Equals(email.ToLower()))?.PolicyKey;
            string policy = PolicyKey switch
            {
                Policies.Admin => Permissions.Admin,
                Policies.Author => Permissions.Author,
                Policies.Viewer => Permissions.Viewer,
                _ => throw new Exception("email is not valid")
            };
            return policy;
        }

        private readonly List<MockUsers> _mockUsers = new List<MockUsers>
        {
            new MockUsers { Email = "Admin", PolicyKey = Policies.Admin},
            new MockUsers { Email = "Author", PolicyKey =Policies.Author },
            new MockUsers { Email = "Viewer", PolicyKey =  Policies.Viewer},
        };
        #endregion


    }

    public static class Policies
    {
        public const string Admin = "admin";
        public const string Viewer = "viewer";
        public const string Author = "author";
    }
    public static class Permissions
    {
        public const string Admin = "newsletter:admin";
        public const string Viewer = "newsletter:viewer";
        public const string Author = "newsletter:author";
    }
    public class MockUsers
    {
        public string Email { get; set; }
        public string PolicyKey { get; set; }
    }
}
