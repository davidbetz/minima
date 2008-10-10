<%@ Page Language="C#" CodeFile="Viewer.aspx.cs" AutoEventWireup="false" Inherits="WebSite.Information.Viewer" %>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Resource_/Style/MinimaBasicSample/default.css" />
</head>
<body>
    <p>Here is the document you requested:</p>
    <h4>Begin</h4>
    <minima:BlogViewer id="bev01" runat="server" />
    <h4>End</h4>
</body>
</html>