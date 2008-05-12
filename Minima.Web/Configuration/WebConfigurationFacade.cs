using System.Configuration;
//+
namespace Minima.Web.Configuration
{
    public static class MinimaConfigurationFacade
    {
        private static MinimaConfigurationSection cachedConfiguration;

        //+
        //- @GetWebConfiguration -//
        public static MinimaConfigurationSection GetWebConfiguration()
        {
            if (cachedConfiguration == null)
            {
                cachedConfiguration = (MinimaConfigurationSection)ConfigurationManager.GetSection("minima.blog");
            }
            return cachedConfiguration;
        }
    }
}