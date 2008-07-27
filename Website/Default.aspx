<%@ Page Language="C#" CodeFile="Default.aspx.cs" AutoEventWireup="false" Inherits="Default" %>
<%@ Register Src="~/Control/GoogleAnalytics.ascx" TagPrefix="web" TagName="GoogleAnalytics" %>
<%@ Register Src="~/Control/GoogleAdsense.ascx" TagPrefix="web" TagName="GoogleAdsense" %>
<%@ Register Src="~/Control/FirefoxGoogleAdsense.ascx" TagPrefix="web" TagName="FirefoxGoogleAdsense" %>
<%@ Register Src="~/Control/LicenseInformation.ascx" TagPrefix="web" TagName="LicenseInformation" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Resource_/Style/MinimaBasicSample/default.css" />
    <link runat="server" id="rssLink" rel="alternate" type="application/rss+xml" />
    <link rel="EditURI" type="application/rsd+xml" runat="server" id="rsd" />
    <link rel="wlwmanifest" type="application/wlwmanifest+xml" runat="server" id="wlwmanifest" />
    <script src="/Resource_/Lib/prototype-1.6.0.2.js" type="text/javascript"></script>
    <script src="/Resource_/Lib/PrototypeExtension.js" type="text/javascript"></script>
    <script src="/Resource_/Lib/Prominax/Trace.js" type="text/javascript"></script>
    <script src="/Resource_/Lib/Prominax/AspNet.js" type="text/javascript"></script>
    <script src="/Resource_/Lib/Prominax/WCF.js" type="text/javascript"></script>
    <script src="/Resource_//Configuration.js" type="text/javascript"></script>
    <script src="/Resource_/Code/Initialization.js" type="text/javascript"></script>
</head>
<body>
    <div id="header">
        <h1 id="image" class="title-image" runat="server">
        </h1>
        <h1 id="text">
            <asp:HyperLink ID="hlBlogUrl" runat="server"></asp:HyperLink>
        </h1>
        <p id="description">
            <asp:Literal ID="litBlogDescription" runat="server"></asp:Literal>
        </p>
    </div>
    <div id="separator">
    </div>
    <div id="content">
        <div id="main">
            <div id="sidebar">
                <h2>Marketing Resources</h2>
                <ul id="sidebarBlock1">
                    <li><a href="#">Marketing Media Group</a></li>
                    <li><a href="#">Universal Marketing Association</a></li>
                    <li><a href="#">12 Steps to Great Marketing</a></li>
                    <li><strong><a href="http://www.netfxharmonics.com/">NetFXHarmonics Blog</a></strong></li>
                    <li><strong><a href="http://www.linkedin.com/in/davidbetz">David Betz on LinkedIn</a></strong></li>
                </ul>
                <h2>Events and Centers</h2>
                <ul id="sidebarBlock2">
                    <li><a href="#">Heartland Marketing Center</a></li>
                    <li><a href="#">Writing Conference</a></li>
                    <li><a href="#">International Marketing</a></li>
                    <li><a href="#">International Writing Organization</a></li>
                </ul>
                <asp:PlaceHolder id="phLabelList" runat="server" />
                <asp:PlaceHolder id="phArchivedEntryList" runat="server" />
                <asp:PlaceHolder id="phRecentEntryList" runat="server" />
                <asp:PlaceHolder id="phRecentEntryListSecondary" runat="server" />
                <p id="firefoxIcon">
                <a href="http://www.spreadfirefox.com/?q=affiliates&amp;id=72158&amp;t=218"><img border="0" alt="Firefox 3" title="Firefox 3" src="http://sfx-images.mozilla.org/affiliates/Buttons/firefox2/ff2o80x15.gif"/></a>
                </p>
                <p id="feeds">
                    <asp:HyperLink ID="hlFeedUrl" runat="server">
                        <img src="http://www.feedburner.com/fb/images/pub/feed-icon32x32.png" alt="" style="border: 0" />
                    </asp:HyperLink>
                </p>
                <div id="googleAdsense">
                </div>
            </div>
            <minima:MinimaForm runat="server">
                <asp:PlaceHolder ID="phMinimaBlog" runat="server"></asp:PlaceHolder>
            </minima:MinimaForm>
        </div>
    </div>
    <div id="license">
        <web:LicenseInformation id="minimaLicenseInformation" runat="server" />
    </div>
    <div id="footer">
        <a href="http://www.spreadfirefox.com/?q=affiliates&amp;id=72158&amp;t=202">
            <img alt="Upgrade to Firefox 3.0!" title="Upgrade to Firefox 3.0!" src="http://sfx-images.mozilla.org/affiliates/products/firefox/upgrade_1_5_468b1.jpg" />
        </a>
    </div>
    <script type=""text/javascript"">
    document.observe('dom:loaded', Initialization.init);
    </script>
    <web:GoogleAnalytics id="ga" runat="server" />
</body>
</html>