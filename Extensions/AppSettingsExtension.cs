namespace CachingWithRedis.Extensions
{
    public static class AppSettingsExtension
    {
        public static T GetValue<T>(string propertyName)
        {
            // below code will now be used to read values from appsettings.json in static method in static class without using DI
            var builder = new ConfigurationBuilder()            
            .SetBasePath(Directory.GetCurrentDirectory()) // get current project directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            var value = configuration.GetValue<T>(propertyName);

            if (value is not null)
                return value;

            return default(T);
        }
    }
}