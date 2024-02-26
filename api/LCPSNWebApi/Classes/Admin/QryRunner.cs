using System.ComponentModel.DataAnnotations;

namespace LCPSNWebApi.Classes.Admin;
public class QryRunner
{
    [Required] public string DBMode { get; set; } = QryDBModeEnum.SQLServer.ToString();
    [Required] public string QryStr { get; set; } = null!;
}

public enum QryDBModeEnum
{
    SQLServer,
    SQLite,
    MySQL,
    PostgreSQL
}