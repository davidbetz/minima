using System.Configuration;
//+
namespace Minima.Web.Configuration
{
    public class MinimaConfigurationSection : ConfigurationSection
    {
        //- @Instances -//
        [ConfigurationProperty("registration")]
        [ConfigurationCollection(typeof(InstanceElement), AddItemName = "instance")]
        public InstanceCollection Registration
        {
            get
            {
                return (InstanceCollection)this["registration"];
            }
            set
            {
                this["registration"] = value;
            }
        }
    }
}