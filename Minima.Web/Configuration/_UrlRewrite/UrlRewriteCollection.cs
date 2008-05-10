using System;
using System.Collections.Generic;
using System.Configuration;
//+
namespace Minima.Web.Configuration
{
    public class UrlRewriteCollection : ConfigurationElementCollection, IEnumerable<UrlRewriteElement>
    {
        //- @[Indexer] -//
        public UrlRewriteElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as UrlRewriteElement;
            }
        }

        //- #CreateNewElement -//
        protected override ConfigurationElement CreateNewElement()
        {
            return new UrlRewriteElement();
        }

        //- #GetElementKey -//
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UrlRewriteElement)element).Match;
        }

        //- #ElementName -//
        protected override string ElementName
        {
            get
            {
                return "add";
            }
        }

        //- #IsElementName -//
        protected override Boolean IsElementName(String elementName)
        {
            return !String.IsNullOrEmpty(elementName) && elementName == "add";
        }

        //- @CollectionType -//
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        #region IEnumerable<UrlRewriteElement> Members

        //- @GetEnumerator -//
        public new IEnumerator<UrlRewriteElement> GetEnumerator()
        {
            for (int i = 0; i < base.Count; i++)
            {
                yield return (UrlRewriteElement)this[i];
            }
        }

        #endregion
    }
}