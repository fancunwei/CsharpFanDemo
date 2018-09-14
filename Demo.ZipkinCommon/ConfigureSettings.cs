using Microsoft.Extensions.Configuration;
using System.Threading;

using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
namespace Demo.ZipkinCommon
{
    public class ConfigureSettings
    {
        public static IConfiguration CreateConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}