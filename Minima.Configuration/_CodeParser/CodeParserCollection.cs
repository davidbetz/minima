#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
using System.Configuration;
//+
using Themelia;
//+
namespace Minima.Configuration
{
    public class CodeParserCollection : Themelia.Configuration.ParameterCollection
    {
        //- @GetParameterMap -//
        public Map GetParameterMap()
        {
            Map map = new Map();
            foreach (Themelia.Configuration.ParameterElement element in this)
            {
                map.Add(element.Name, element.Value);
            }
            //+
            return map;
        }
    }
}