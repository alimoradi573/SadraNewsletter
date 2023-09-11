using Microsoft.AspNetCore.Mvc;

namespace Newsletter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsletterController : ControllerBase
    {
        private readonly INewsletterService _newsletterService;

        public NewsletterController(INewsletterService newsletterService)
        {
            this._newsletterService = newsletterService;
        }
        [HttpGet]
        [Route("[action]")]
        public virtual string Alive() => "I Alive ! Be happy my dear  :)  ";

        [HttpPost]
        //[Authorize(Policy = Policies.Admin)]
        public virtual async Task<IActionResult> Post(NewsLetterDTO newsLetterDTO)
        {
            var result = await _newsletterService.CreateAsync(newsLetterDTO);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize(Policy = Policies.Admin)]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _newsletterService.GetAsync(id);
            return Ok(result);
        }


    }
}