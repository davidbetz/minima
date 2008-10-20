#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Themelia.Web.Routing.Data;
//+
namespace Minima.Web.Routing
{
    public class ProcessorFactory : Themelia.Web.Routing.ProcessorFactoryBase
    {
        //- @CreateProcessor -//
        public override Themelia.Web.Routing.IProcessor CreateProcessor(String text)
        {
            switch (text)
            {
                case "__$minima$blogfallthroughprocessor":
                    return new BlogFallThroughProcessor();
                case "__$minima$injectionprocessor":
                    return new InjectionProcessor();
                case "__$minima$postprocessor":
                    return new PostProcessor();
                case "__$minima$preprocessor":
                    return new PreProcessor();
            }
            //+
            return null;
        }
    }
}