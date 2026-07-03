using Microsoft.Extensions.Configuration;

namespace PokeApiTesting3.utils
{
    public sealed class Config // Singleton
    {
        private static readonly Config _instance = new Config();

        public static Config Instance => _instance;

        public IConfiguration Configuration { get; }

        private Config()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }
    }
}
