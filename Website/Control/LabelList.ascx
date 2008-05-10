<%@ Control Language="C#" CodeFile="LabelList.ascx.cs" Inherits="LabelList" %>
<%@ OutputCache Duration="3600" VaryByParam="None" %>
<h2>Labels</h2>
<ul id="labels">
    <asp:Repeater ID="rptLabelList" runat="server">
        <ItemTemplate>
            <li>
                <asp:HyperLink ID="hlLabel" runat="server" NavigateUrl='<%#Bind("Url") %>'>
                <asp:Literal ID="Literal2" runat="server" Text='<%#Bind("Title") %>'></asp:Literal>
                (<asp:Literal ID="Literal1" runat="server" Text='<%#Bind("EntryCount") %>'></asp:Literal>)
                </asp:HyperLink>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
