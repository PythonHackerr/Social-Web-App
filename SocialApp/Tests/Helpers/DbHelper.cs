using MySqlConnector;

namespace Tests.Helpers;

public static class DbHelper
{
    public const string ConnectionString = 
        "Server=pismysqlserv.mysql.database.azure.com;Uid=Admin123;Pwd=Password123!;Database=twittercopy";

    /// <summary>
    /// create it manually because of issues with manual DI resolve in the test project
    /// </summary>
    public static MySqlConnection Connection() => new(ConnectionString);
}