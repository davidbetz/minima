using System;
using System.Collections.Generic;
using System.Configuration;
//+
namespace Minima.Web.Configuration
{
    public class HttpHandlerCollection : ConfigurationElementCollection, IEnumerable<HttpHandlerElement>
    {
        //- @[Indexer] -//
        public HttpHandlerElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as HttpHandlerElement;
            }
        }

        //- #CreateNewElement -//
        protected override ConfigurationElement CreateNewElement()
        {
            return new HttpHandlerElement();
        }

        //- #GetElementKey -//
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((HttpHandlerElement)element).MatchText;
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

        #region IEnumerable<HttpHandlerElement> Members

        //- @GetEnumerator -//
        public new IEnumerator<HttpHandlerElement> GetEnumerator()
        {
            for (int i = 0; i < base.Count; i++)
            {
                yield return (HttpHandlerElement)this[i];
            }
        }

        #endregion
    }
}