using System.Configuration;
//+
namespace Minima.Web.Configuration
{
    public class WebConfigurationSection : ConfigurationSection
    {
        //- @HttpHandlers -//
        [ConfigurationProperty("httpHandlers")]
        [ConfigurationCollection(typeof(UrlRewriteElement), AddItemName = "add")]
        public HttpHandlerCollection HttpHandlers
        {
            get
            {
                return (HttpHandlerCollection)this["httpHandlers"];
            }
            set
            {
                this["httpHandlers"] = value;
            }
        }

        //- @UrlRewrites -//
        [ConfigurationProperty("urlRewrites")]
        [ConfigurationCollection(typeof(UrlRewriteElement), AddItemName = "add")]
        public UrlRewriteCollection UrlRewrites
        {
            get
            {
                return (UrlRewriteCollection)this["urlRewrites"];
            }
            set
            {
                this["urlRewrites"] = value;
            }
        }
    }
}