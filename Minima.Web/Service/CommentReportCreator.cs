#region Copyright
//+ Copyright © Jampad Technology, Inc. 2007-2009
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>
#endregion
using System;
//+
using Themelia;
using Themelia.Reporting;
//+
namespace Minima.Web.Service
{
    public class CommentReportCreator : Themelia.Reporting.ReportCreator
    {
        //- #CreateCore -//
        protected override String CreateCore(Object content)
        {
            Map data = content as Map;
            if (data != null)
            {
                    this.Write("New Comment!", FormatterType.Heading);
                    this.Write("This comment has been moderated", FormatterType.SubHeading);
                    this.Write(FormatterType.Break);
                    this.Write("Blog Entry Title: " + data["BlogEntryTitle"], FormatterType.Normal);
                    this.Write(FormatterType.Break);
                    this.Write("BlogEntry Guid: " + data["Guid"], FormatterType.Normal);
                    this.Write(FormatterType.Break);
                    this.Write("Comment author: " + data["Author"], FormatterType.Normal);
                    this.Write(FormatterType.Break);
                    this.Write("Comment email: " + data["Email"], FormatterType.Normal);
                    this.Write(FormatterType.Break);
                    this.Write("Comment website: " + data["WebSite"], FormatterType.Normal);
                    this.Write(FormatterType.Break);
                    this.Write("Comment date/time: " + data["DateTime"], FormatterType.Normal);
                    this.Write(FormatterType.Break);
                    this.Write("Comment text: " + data["Text"], FormatterType.PreFormatted);
                    this.Write(FormatterType.Break);
                    this.Write("To activate use the following link:", FormatterType.Normal);
                    this.Write(data["Link"], FormatterType.Link);
            }
            //+
            return this.Result.ToString();
        }
    }
}