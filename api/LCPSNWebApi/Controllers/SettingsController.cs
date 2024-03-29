using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/settings")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class SettingsController : ControllerBase
    {
        private readonly IHostEnvironment _hostEnvironment;
        public SettingsController(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetSettingsInfo()
        {
            var rawJson = await System.IO.File.ReadAllTextAsync(GetFileName());
            var jsonObj = JsonNode.Parse(rawJson);
            return Ok(jsonObj!);
        }

        [HttpPost]
        public async Task<IActionResult> PostSettingsInfo([FromBody] AppSettingsModelCT asmct)
        {
            var rawJson = await System.IO.File.ReadAllTextAsync(GetFileName());
            var jsonObj = JsonNode.Parse(rawJson);
            jsonObj!["DBMode"] = asmct.DBMode!.Value.ToString();
            using (var file = System.IO.File.CreateText(GetFileName()))
            {
                await file.WriteAsync(JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
            }
            return Ok(jsonObj);
        }

        private string GetFileName()
        {
            //var prefixEnv = _hostEnvironment.IsDevelopment() == true ? "Development" : "";
            //return $"appsettings.{prefixEnv}.json";
            return $"appsettings.{_hostEnvironment.EnvironmentName}.json";
        }

        public class AppSettingsModelCT
        {
            public DBModeEnum? DBMode { get; set; } = DBModeEnum.SQLServer;
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum DBModeEnum
        {
            [Description("SQLServer")] SQLServer,
            [Description("SQLite")] SQLite,
            [Description("MySQL")] MySQL,
            [Description("PostgreSQL")] PostgreSQL
        }
    }
}