using System;
using System.Collections.Generic;
//+
namespace Minima.Web.Routing.Component
{
    public class MinimaComponentSetting : Themelia.Web.Routing.Component.ComponentSetting
    {
        //- @MinimaParameter -//
        public class MinimaInfo
        {
            //- @BlogGuid -//
            public String BlogGuid { get; set; }

            //- @Page -//
            public String Page { get; set; }

            //- @WebSection -//
            public String WebSection { get; set; }
        }

        //+
        //- @CurrentComponentSetting -//
        public static MinimaComponentSetting CurrentComponentSetting
        {
            get
            {
                return Themelia.Web.Routing.Component.Settings.Components["Minima"] as MinimaComponentSetting;
            }
        }

        //+
        //- @GetParameterList -//
        public List<MinimaInfo> GetParameterList()
        {
            List<MinimaInfo> list = new List<MinimaInfo>();
            Themelia.Web.Routing.Component.WebSectionSettingMap webSectionSettingMap = MinimaComponentSetting.CurrentComponentSetting.WebSections;
            foreach (String webSectionName in webSectionSettingMap.Keys)
            {
                list.Add(new MinimaInfo
                {
                    BlogGuid = webSectionSettingMap[webSectionName].Parameters["blogguid"],
                    Page = webSectionSettingMap[webSectionName].Parameters["page"],
                    WebSection = webSectionName
                });
            }
            //+
            return list;
        }
    }
}