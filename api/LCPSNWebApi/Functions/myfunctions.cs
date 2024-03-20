using LCPSNLibrary.Classes;
using System.Text.Json;
using System.Globalization;

namespace LCPSNWebApi.Functions;

public static class MyFunctions 
{
    public static JsonSerializerOptions SetJSONSerializerOptions() {
        return new JsonSerializerOptions() { 
            PropertyNameCaseInsensitive = true 
        };
    }

    public static string GetCurLibPath() {
        return Path.GetDirectoryName(Directory.GetCurrentDirectory())!.ToString().Replace(@"\api", @"\LCPSNLibrary");
    }

    public static async Task<List<LanguagesCl>> GetLanguages() {
        using FileStream stream = File.OpenRead(@$"{GetCurLibPath()}\Data\languages.json");
        var resdata = await JsonSerializer.DeserializeAsync<List<LanguagesCl>>(stream, SetJSONSerializerOptions());
        return resdata!.OrderBy(x => x.Name).ToList()!;
    }

    public static async Task<List<CultureInfo>> GetListCultures() {
        var supportedCultures = new List<CultureInfo>();
        
        foreach(var i in await GetLanguages()) {
            supportedCultures.Add(new CultureInfo(i.CountryCodeIso!));
        }

        return supportedCultures;
    }
}