using System;
using System.Collections.Generic;
using System.ServiceModel;
//+
namespace Minima.Service
{
    [ServiceContract(Namespace = Information.Minima.Namespace)]
    public interface ILabelService
    {
        //- ApplyLabel -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void ApplyLabel(String blogEntryGuid, String labelGuid);
        
        //- CreateLabel -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        String CreateLabel(String blogGuid, String title);

        //- RemoveLabel -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void RemoveLabel(String labelGuid, String blogEntryGuid);

        //- UpdateLabel -//
        [FaultContract(typeof(ArgumentException))]
        [OperationContract]
        void UpdateLabel(String labelGuid, String title);

        //- GetBlogEntryLabelList -//
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        List<Label> GetBlogEntryLabelList(String blogEntryGuid);

        //- GetBlogLabelList -//
        [FaultContract(typeof(ArgumentException))]
        [OperationContract]
        List<Label> GetBlogLabelList(String blogGuid);

        //- GetLabelByTitle -//
        [OperationContract]
        Label GetLabelByTitle(String title);

    }
}