using Microsoft.Extensions.Configuration;

namespace DreamHouse.Core.Application.Helpers.FOR_DELETE
{
    public static class GBEV
    {
        public static string GetConnection(this IConfiguration config, string section)
        {
            var server = Environment.GetEnvironmentVariable("DOTNET_SERVER_NAME", EnvironmentVariableTarget.User);
            var connectionString = config.GetConnectionString(section)!;

            var connectionInPart = connectionString.Split('.');
            connectionInPart[0] += server;

            return string.Join("",connectionInPart);
        }
    }
}
