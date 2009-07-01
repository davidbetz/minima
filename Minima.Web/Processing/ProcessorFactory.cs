#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
//+
namespace Minima.Web.Processing
{
    public class ProcessorFactory : Themelia.Web.Processing.ProcessorFactoryBase
    {
        //- @CreateProcessor -//
        public override Themelia.Web.Processing.IProcessor CreateProcessor(String text)
        {
            switch (text)
            {
                case "__$minima$overrideprocessor":
                    return new OverrideProcessor();
                case "__$minima$initprocessor":
                    return new InitProcessor();
            }
            //+
            return null;
        }
    }
}