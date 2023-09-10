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
        public string GenerateToken(string userName)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:PublicKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string policy = SetPolicy(userName);

            var claims = new[]
            {
                 new Claim("username",userName),
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
        private string SetPolicy(string userName)
        {
            string PolicyKey = _mockUsers.SingleOrDefault(c => c.UserId.ToLower().Equals(userName.ToLower()))?.PolicyKey;
            string policy = PolicyKey switch
            {
                Policies.Admin => Permissions.Admin,
                Policies.Author => Permissions.Author,
                Policies.Controller => Permissions.Controller,
                Policies.Worker => Permissions.Worker,
                Policies.Viewer => Permissions.Viewer,
                _ => throw new Exception("username is not valid")
            };
            return policy;
        }

        private readonly List<MockUsers> _mockUsers = new List<MockUsers>
        {
            new MockUsers { UserId = "Admin", PolicyKey = Policies.Admin, AssignedTasks=GenerateTask(1) },
            new MockUsers { UserId = "Author", PolicyKey =Policies.Author , AssignedTasks=GenerateTask(1) },
            new MockUsers { UserId = "Viewer", PolicyKey =  Policies.Viewer, AssignedTasks=GenerateTask(1)},
            new MockUsers { UserId = "Worker", PolicyKey = Policies.Worker ,AssignedTasks=GenerateTask(1)},
            new MockUsers { UserId = "Controller", PolicyKey = Policies.Controller, AssignedTasks=GenerateTask(1) } };

        private static List<MockTask> GenerateTask(int v)
        {
            string requestMessage = "please select yes or no ";
            List<MockTask> result = new List<MockTask>();
            while (v > 0)
            {
                result.Add(new MockTask { TaskId = v, TaskMessage = requestMessage });
                v--;
            }
            return result;
        }
        #endregion


    }

    public static class Policies
    {
        /// <summary>
        /// Adminstrative tasks
        /// </summary>
        public const string Admin = "admin";
        /// <summary>
        /// Authoring of workflow definitions and steps
        /// </summary>
        public const string Viewer = "viewer";
        /// <summary>
        /// Starting, stopping, suspending and resuming workflows
        /// </summary>
        public const string Controller = "controller";
        /// <summary>
        /// Querying the status of a workflow
        /// </summary>
        public const string Author = "author";
        /// <summary>
        /// Activity workers
        /// </summary>
        public const string Worker = "worker";
    }


    public static class Permissions
    {
        public const string Admin = "newsletter:admin";
        public const string Viewer = "newsletter:viewer";
        public const string Controller = "newsletter:controller";
        public const string Author = "newsletter:author";
        public const string Worker = "newsletter:worker";
    }
    public class MockUsers
    {
        public string UserId { get; set; }
        public string PolicyKey { get; set; }
        public List<MockTask> AssignedTasks { get; set; }
    }


    public class MockTask
    {
        public int TaskId { get; set; }
        public string TaskMessage { get; set; }
    }
}
