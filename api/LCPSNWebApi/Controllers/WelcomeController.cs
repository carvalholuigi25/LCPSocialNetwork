using LCPSNWebApi.Resource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WelcomeController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _shResLoc;

        public WelcomeController(IStringLocalizer<SharedResource> shResLoc)
        {
            _shResLoc = shResLoc;
        }

        /// <summary>
        /// This endpoint retrives a welcome message!
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetWelcome()
        {
            return Ok(new { Msg = _shResLoc.GetString("Welcome").Value });
        }

        /// <summary>
        /// This endpoint changes the current language
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        [HttpGet("changelang")]
        public IActionResult ChangeLang(string? culture = "en") {
            if(!string.IsNullOrEmpty(culture)) {
                Response.Headers.Append("Accept-Language", culture);
                Response.Headers.Append("Content-Language", culture);
                Response.Headers.Append("X-Language", culture);
            }

            return Ok(new { MsgLang = "Changed language to " + culture, Msg = _shResLoc.GetString("Welcome").Value });
        }
    }
}