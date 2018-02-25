namespace ExampleApplicationMVC.Utilities
{
    public static class ConfigManager
    {
        public static string GetConnectionString(string name)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[$"{name}"].ConnectionString;
        }
    }
}