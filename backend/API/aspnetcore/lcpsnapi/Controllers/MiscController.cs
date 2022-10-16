using lcpsnapi.Classes;
using static lcpsnapi.Extensions.SizeUnits;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace lcpsnapi.Controllers
{   
    #nullable enable

    [Route("api/misc")]
    [ApiController]
    // [AuthMyRoles]
    public class MiscController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MiscController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<IActionResult> GetWeatherInfo(string city = "Braga", string country = "pt", string? state = "", string? units = "metric", string? lang = "pt")
        {
            if(string.IsNullOrEmpty(city))
            {
               return BadRequest("Error: Please write something in city field");
            }

            if (string.IsNullOrEmpty(country))
            {
               return BadRequest("Error: Please write something in country field");
            }

            if (!string.IsNullOrEmpty(state) && !country.Contains("us", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Error: The state field is only needed for USA country!");
            }

            using var client = new HttpClient();
            try
            {
                var appid = _configuration["APIOpenWeatherKey"];
                state = !string.IsNullOrEmpty(state) && country.Contains("us", StringComparison.OrdinalIgnoreCase) ? ","+state+"," : ",";
                client.BaseAddress = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={city}{state}{country}&appid={appid}&units={units}&lang={lang}");

                var response = await client.GetAsync(client.BaseAddress);
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var rawWeather = JsonConvert.DeserializeObject<WeatherRoot>(stringResult);
                return Ok(rawWeather);
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
            }
        }

        /// <summary>
        /// Get weather info from openweather api. (for more info: https://openweathermap.org/api)
        /// </summary>
        [HttpGet("weather/{city}/{country}/{units?}/{lang?}")]
        public async Task<IActionResult> GetWeather(string city = "Braga", string country = "pt", string? units = "metric", string? lang = "pt")
        {
            return await GetWeatherInfo(city, country, "", units, lang);
        }

        /// <summary>
        /// Get weather info with state from openweather api. (for more info: https://openweathermap.org/api)
        /// </summary>
        [HttpGet("weather/{city}/{country}/{units?}/{lang?}/{state?}")]
        public async Task<IActionResult> GetWeatherWithState(string city = "Los Angeles", string country = "us", string? units = "metric", string lang = "en", string? state = "ca")
        {
            return await GetWeatherInfo(city, country, state, units, lang);
        }

        /// <summary>
        /// Get all timezones searching by its term or not 
        /// </summary>
        [HttpGet("timezone")]
        public async Task<ActionResult<IList<TimeZoneInfo>>> GetMyTimezone([FromQuery] string? tz = "")
        {
            ReadOnlyCollection<TimeZoneInfo> lstz = TimeZoneInfo.GetSystemTimeZones();
            var objlstz = tz != null ? lstz.Where(x => x.DisplayName.Contains(tz, StringComparison.OrdinalIgnoreCase)).ToList() : lstz.ToList();
            return await Task.FromResult(objlstz);
        }

        /// <summary>
        /// Get Metric Numbers
        /// </summary>
        [HttpGet("metric/{num}")]
        public async Task<ActionResult<string>> GetMetricNums(double num)
        {
            return await Task.FromResult(num.ToMetric(null, 2));
        }

        /// <summary>
        /// Get Byte size
        /// </summary>
        [HttpGet("bsize/{num}/{frm}/{decplaces}")]
        public async Task<ActionResult<object>> GetByteSize(long num = 1, ESizeUnits? frm = ESizeUnits.GB, int decplaces = 2)
        {
            Dictionary<string, dynamic> ls = new()
            {
                { "Bit", num.Bits() },
                { "Byte", num.Bytes() },
                { "KB", num.Kilobytes() },
                { "MB", num.Megabytes() },
                { "GB", num.Gigabytes() },
                { "TB", num.Terabytes() }
            };

            object rv = ls.Count > 0 ? ls.FirstOrDefault(d => d.Key == frm.ToString()).Value : ls.FirstOrDefault().Value;

            return await Task.FromResult(new
            {
                bits = GetValSizeUnit(ESizeUnits.Bit, rv, decplaces),
                bytes = GetValSizeUnit(ESizeUnits.Byte, rv, decplaces),
                kilobytes = GetValSizeUnit(ESizeUnits.KB, rv, decplaces),
                megabytes = GetValSizeUnit(ESizeUnits.MB, rv, decplaces),
                gigabytes = GetValSizeUnit(ESizeUnits.GB, rv, decplaces),
                terabytes = GetValSizeUnit(ESizeUnits.TB, rv, decplaces)
            });
        }

        /// <summary>
        /// Basic calculator
        /// </summary>
        [HttpGet("bcalc/{num}/{num2}/{op}")]
        public async Task<ActionResult<object>> DoCalc(double num, double num2, string op = "sum")
        {
            if (string.IsNullOrEmpty(num.ToString()))
            {
                BadRequest(new
                {
                    Result = "Error: Please write number in input 1!"
                });
            }

            if (string.IsNullOrEmpty(num2.ToString()))
            {
                BadRequest(new
                {
                    Result = "Error: Please write number in input 2!"
                });
            }

            if (double.IsNaN(num))
            {
                BadRequest(new
                {
                    Result = "Error: the input 1 needs to be in number!"
                });
            }

            if (double.IsNaN(num2))
            {
                BadRequest(new
                {
                    Result = "Error: the input 2 needs to be in number!"
                });
            }

            object? fs = op switch
            {
                "sum" => num + num2,
                "subtract" => num - num2,
                "multiply" => num * num2,
                "div" => num / num2,
                _ => null
            };

            object res;
            string[] opsary = new string[] { "sum", "subtract", "multiply", "div" };

            if (!string.IsNullOrEmpty(fs?.ToString()) && opsary.Contains(op))
            {
                res = new
                {
                    Result = fs,
                    Num1 = num,
                    Num2 = num2,
                    Operator = op
                };
            }
            else
            {
                res = new
                {
                    Result = "Error: Please select the operator (sum, subtract, multiply, div)!"
                };
            }

            return await Task.FromResult(res);
        }
    }
}
