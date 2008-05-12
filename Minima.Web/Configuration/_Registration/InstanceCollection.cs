using System;
using System.Collections.Generic;
using System.Configuration;
//+
namespace Minima.Web.Configuration
{
    public class InstanceCollection : ConfigurationElementCollection, IEnumerable<InstanceElement>
    {
        //- @[Indexer] -//
        public InstanceElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as InstanceElement;
            }
        }

        //- #CreateNewElement -//
        protected override ConfigurationElement CreateNewElement()
        {
            return new InstanceElement();
        }

        //- #GetElementKey -//
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((InstanceElement)element).WebSection;
        }

        //- #ElementName -//
        protected override string ElementName
        {
            get
            {
                return "instance";
            }
        }

        //- #IsElementName -//
        protected override Boolean IsElementName(String elementName)
        {
            return !String.IsNullOrEmpty(elementName) && elementName == "instance";
        }

        //- @CollectionType -//
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        #region IEnumerable<InstanceElement> Members

        //- @GetEnumerator -//
        public new IEnumerator<InstanceElement> GetEnumerator()
        {
            for (int i = 0; i < base.Count; i++)
            {
                yield return (InstanceElement)this[i];
            }
        }

        #endregion
    }
}