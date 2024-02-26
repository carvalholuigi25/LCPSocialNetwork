using System.Runtime.CompilerServices;

namespace LCPSNWebApi.Context;
public static class DBNGUTC
{
    [ModuleInitializer]
    public static void Initialize()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}