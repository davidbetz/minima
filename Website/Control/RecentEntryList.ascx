<%@ Control Language="C#" CodeFile="RecentEntryList.ascx.cs" Inherits="RecentEntryList" %>
<%@ OutputCache Duration="3600" VaryByParam="None" %>
<h2>Previous Posts</h2>
<ul id="Ul1">
    <asp:Repeater ID="rptRecentEntryList" runat="server">
        <ItemTemplate>
            <li>
                <asp:HyperLink ID="hlArchivedEntry" runat="server" NavigateUrl='<%#Bind("Url") %>'>
                    <asp:Literal ID="Literal2" runat="server" Text='<%#Bind("Title") %>'></asp:Literal>
                </asp:HyperLink>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
