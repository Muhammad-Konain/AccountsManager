using AccountsManager.Application.V1.Contracts.HelperContracts;
using Microsoft.Extensions.Configuration;

namespace AccountsManager.Application.V1.Helpers
{
    public sealed class ConfigReader : IConfigReader
    {
        private readonly IConfiguration _configuration;

        public ConfigReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T GetSectionValue<T>(string section)
        {
            return _configuration.GetValue<T>(section);
        }
    }
}
