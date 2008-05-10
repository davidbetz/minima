<%@ Control Language="C#" CodeFile="ArchivedEntryList.ascx.cs" Inherits="ArchivedEntryList" %>
<%@ OutputCache Duration="3600" VaryByParam="None" %>
<h2>Archives</h2>
<ul id="recent">
    <asp:Repeater ID="rptArchivedEntryList" runat="server">
        <ItemTemplate>
            <li>
                <asp:HyperLink ID="hlArchivedEntry" runat="server" NavigateUrl='<%#Bind("Url") %>'><asp:Literal ID="Literal2" runat="server" Text='<%#Bind("MonthText") %>'></asp:Literal>&nbsp;<asp:Literal ID="Literal1" runat="server" Text='<%#Bind("Year") %>'></asp:Literal> (<asp:Literal ID="litLinkText" runat="server" Text='<%#Bind("Count") %>'></asp:Literal>)</asp:HyperLink>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
