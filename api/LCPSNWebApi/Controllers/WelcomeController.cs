using LCPSNWebApi.Library.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WelcomeController : ControllerBase
    {
        private readonly IStringLocalizer<MyResources> _localizer;

        public WelcomeController(IStringLocalizer<MyResources> localizer)
        {
            _localizer = localizer;
        }

        /// <summary>
        /// This endpoint retrives a welcome message!
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetWelcome([FromQuery] string? culture = "en")
        {
            if(!string.IsNullOrEmpty(culture)) {
                Response.Headers.Append("Accept-Language", culture);
                Response.Headers.Append("Content-Language", culture);
                Response.Headers.Append("X-Language", culture);
            }

            return Ok(new { Msg = _localizer.GetString("Welcome").Value });
        }
    }
}
