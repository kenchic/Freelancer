using Microsoft.Extensions.Configuration;

namespace GrupoXpert.Tools
{
    public static class Configuration
    {
        private static readonly IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", true, true)
        .Build();

        public static string AppAPIUrl()
        {
            var value = configuration["APIUrl"];
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }
    }
}