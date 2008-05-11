using System.Configuration;
//+
namespace Minima.Web.Configuration
{
    public static class WebConfigurationFacade
    {
        private static WebConfigurationSection cachedConfiguration;

        //+
        //- @GetWebConfiguration -//
        public static WebConfigurationSection GetWebConfiguration()
        {
            if (cachedConfiguration == null)
            {
                cachedConfiguration = (WebConfigurationSection)ConfigurationManager.GetSection("minima.web");
            }
            return cachedConfiguration;
        }
    }
}