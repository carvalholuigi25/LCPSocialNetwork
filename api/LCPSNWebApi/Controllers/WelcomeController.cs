using System.Text.Json;
using LCPSNLibrary.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WelcomeController : ControllerBase
    {
        private readonly IStringLocalizer<LCPSNLibrary.Resources.MyResources> _localizer;

        public WelcomeController(IStringLocalizer<LCPSNLibrary.Resources.MyResources> localizer)
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

        /// <summary>
        /// This endpoint gets language list
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("langlist")]
        public async Task<ActionResult<List<LanguagesCl>>> GetLanguageList()
        {
            var pthlib = Path.GetDirectoryName(Directory.GetCurrentDirectory())!.ToString().Replace(@"\api", @"\LCPSNLibrary\Data");
            using FileStream stream = System.IO.File.OpenRead(pthlib + @"\languages.json");
            var datares = await JsonSerializer.DeserializeAsync<List<LanguagesCl>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return Ok(datares!.OrderBy(x => x.Name).ToList());
        }
    }
}
