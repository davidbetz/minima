using System;
using System.Collections.Generic;
//+
using Minima.Service;
//+
namespace Minima.Web.Control
{
    public class IndexEntry
    {
        //- @Url -//
        public String Url { get; set; }

        //- @Title -//
        public String Title { get; set; }

        //- @TypeGuid -//
        public String TypeGuid { get; set; }

        //- @DateTimeString -//
        public String DateTimeString { get; set; }

        //- @PostDateTime -//
        public DateTime PostDateTime { get; set; }

        //- @DateTimeDisplay -//
        public String DateTimeDisplay { get; set; }

        //- @LabelList -//
        public List<Label> LabelList { get; set; }
    }
}