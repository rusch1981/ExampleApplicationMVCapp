using System.Configuration;

namespace ExampleApplicationMVC.Utilities
{
    public static class ConfigManager
    {
        public static string GetConnectionString(string name)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[$"{name}"].ConnectionString;
        }

        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}