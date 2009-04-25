#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Themelia;
//+
namespace Minima.Parsing
{
    public class AmazonAffiliateCodeParser : Themelia.CodeParsing.CodeParserBase
    {
        public class Info
        {
            public const String AmazonAffiliate = "AmazonAffiliate";
        }

        //- @Id -//
        public override String Id
        {
            get { return "AmazonAffiliate"; }
        }

        //+
        //- @Parse -//
        public override String ParseCode(String code)
        {
            Template linkTemplate = new Template(@"<a href=""http://www.amazon.com/gp/product/{ASIN}/{AffiliateCode}"">{ItemTitle}</a>");
            Map map = new Map();
            map.Add("AffiliateCode", Minima.Configuration.BlogSection.GetConfigSection().CodeParsers.GetParameterMap().PeekSafely(Info.AmazonAffiliate));
            if (!String.IsNullOrEmpty(code))
            {
                String[] partArray = code.Split('|');
                if (partArray.Length == 2)
                {
                    map.Add("ASIN", partArray[0]);
                    map.Add("ItemTitle", partArray[1]);
                    //+
                    return linkTemplate.Interpolate(map);
                }
            }
            //+
            return null;
        }
    }
}