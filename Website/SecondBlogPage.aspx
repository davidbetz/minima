<%@ Page Language="C#" CodeFile="SecondBlogPage.aspx.cs" AutoEventWireup="false" Inherits="SecondBlogPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <div id="header">
        <h1>Plain And Simple Blog</h1>
        <h1 id="text">
            <asp:HyperLink ID="hlBlogUrl" runat="server"></asp:HyperLink>
        </h1>
        <p id="description">
            <asp:Literal ID="litBlogDescription" runat="server"></asp:Literal>
        </p>
    </div>
    <div id="content">
        <minima:MinimaForm ID="form01" runat="server">
            <asp:PlaceHolder ID="phMinimaBlog" runat="server"></asp:PlaceHolder>
        </minima:MinimaForm>
    </div>
</body>
</html>
