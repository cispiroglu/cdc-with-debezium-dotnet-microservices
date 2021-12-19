using Microsoft.Extensions.Configuration;
using Shared.Common.DbParams;

namespace Shared.Common.Extensions.Configuration;

public static class ConfigurationHelper
{
    private static IConfiguration  _configuration;
    
    private static IDbParams _dbParams;
    
    public static IConfiguration Configuration
    {
        get
        {
            if (_configuration != null) 
                return _configuration;
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.development.json", optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            return _configuration;
        }
    }
    
    public static IDbParams DbParams
    {
        get
        {
            if (_dbParams != null) 
                return _dbParams;
            
            var dbParams = Configuration.GetSection(nameof(DbParams)).Get<DbParams.DbParams>();

            return _dbParams;
        }
    }
}