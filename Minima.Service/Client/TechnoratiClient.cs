using System;
//+
namespace Minima.Service.Client
{
    public class TechnoratiClient : MinimaClientBase<ITechnoratiService>, ITechnoratiService
    {
        //- @Ctor -//
        public TechnoratiClient(String endpointConfigurationName)
            : base(endpointConfigurationName) { }

        #region ITechnoratiService Members

        public void PingTechnorati(string blogGuid)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}