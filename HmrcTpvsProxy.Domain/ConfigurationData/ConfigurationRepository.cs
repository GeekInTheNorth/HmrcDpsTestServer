using System.IO;
using System.Web.Script.Serialization;

namespace HmrcTpvsProxy.Domain.ConfigurationData
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly string configPath;

        public ConfigurationRepository(string configPath)
        {
            this.configPath = configPath;
        }

        public Configuration GetConfiguration()
        {
            var jsonHandler = new JavaScriptSerializer();

            using (var reader = new StreamReader(configPath))
            {
                var jsonText = reader.ReadToEnd();
                return jsonHandler.Deserialize<Configuration>(jsonText);
            }
        }
    }
}