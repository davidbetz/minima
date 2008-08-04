using System;
using System.Text.RegularExpressions;
//+
using Themelia;
//+
namespace Minima.Parsing
{
    public class AmazonAffiliateCodeParser : Themelia.CodeParsing.CodeParserBase
    {
        public class Info
        {
            public const string AmazonAffiliate = "AmazonAffiliate";
        }

        //- @Id -//
        public override string Id
        {
            get { return "AmazonAffiliate"; }
        }

        //+
        //- @Parse -//
        public override String ParseCode(String code)
        {
            Template linkTemplate = new Template(@"<a href=""http://www.amazon.com/gp/product/{ASIN}/{AffiliateCode}"">{ItemTitle}</a>");
            Map map = new Map();
            map.Add("AffiliateCode", Themelia.Configuration.ConfigAccessor.ApplicationSettings(Info.AmazonAffiliate));
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