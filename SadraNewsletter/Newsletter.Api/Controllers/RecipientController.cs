using Microsoft.AspNetCore.Mvc;

namespace Newsletter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipientController : ControllerBase
    {
        private readonly INewsletterService _newsletterService;
        private readonly IRecipientService _recipientService;

        public RecipientController(INewsletterService newsletterService,IRecipientService recipientService)
        {
            this._newsletterService = newsletterService;
            this._recipientService = recipientService;
        }

        [HttpPost]
        //[Authorize(Policy = Policies.Admin)]
        public virtual async Task<IActionResult> Post(SendToRecipientDto recipient)
        {
            var result = await _recipientService.CreateAsync(recipient);
            return Ok(result);
        }

        [HttpPost("Bulk")]
        //[Authorize(Policy = Policies.Admin)]
        public virtual async Task<IActionResult> Bulk(List<SendToRecipientDto> recipients)
        {
            var result = await _recipientService.CreateRangeAsync(recipients);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize(Policy = Policies.Admin)]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _recipientService.GetAsync(id);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        //[Authorize(Policy = Policies.Admin)]
        public virtual async Task<IActionResult> GetAll(string email)
        {
            var result = await _recipientService.GetAllAsync(email);
            return Ok(result);
        }
    }
}