using LCPSNWebApi.Library.Classes;
using LCPSNWebApi.Functions;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        public LanguageController()
        {
        }

        /// <summary>
        /// This endpoint gets language list
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<LanguagesCl>>> GetLanguageList()
        {
            return Ok(await MyFunctions.GetLanguages());
        }
    }
}
