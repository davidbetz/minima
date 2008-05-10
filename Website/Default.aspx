<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.cs" EnableSessionState="true" Inherits="Default" Title="" %>
<%@ Register Src="~/Control/MathVerification.ascx" TagPrefix="minima" TagName="MathVerification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:PlaceHolder ID="phNoEntries" runat="server" Visible="false">
    <asp:Literal ID="litNoEntriesMessage" runat="server"></asp:Literal>
    </asp:PlaceHolder>
    <asp:Repeater ID="rptPosts" runat="server">
        <ItemTemplate>
            <asp:HiddenField ID="hBlogEntryGuid" runat="server" Value='<%#Bind("Guid")%>'></asp:HiddenField>
            <div class="post">
                <a id="htmlPostName" runat="server"></a>
                <h3><asp:HyperLink ID="hlPostLink2" runat="server" NavigateUrl='<%#Bind("Url")%>'><asp:Literal ID="Literal3" runat="server" Text='<%#Bind("Title")%>'></asp:Literal></asp:HyperLink></h3>
                <h2 class="date-header"><asp:Literal ID="Literal1" runat="server" Text='<%#Bind("DateTimeString")%>'></asp:Literal></h2>
                <div class="post-body">
                    <div>
                        <asp:Literal ID="litBlogEntryText" runat="server" Text='<%#Bind("Content")%>'></asp:Literal>
                    </div>
                </div>
                <p class="post-footer">
                    <em>posted by
                        <asp:Literal ID="litBlogEntryAuthorSeries" runat="server" Text='<%#Bind("AuthorSeries")%>'></asp:Literal>
                        at
                        <asp:Literal ID="litBlogEntryDateTimeDisplay" runat="server" Text='<%#Bind("DateTimeDisplay")%>'></asp:Literal>
                    </em>
                    <asp:PlaceHolder ID="phLabels" runat="Server" Visible="false">
                    <p class="post-labels">
                    <asp:Literal ID="litBlogEntryLabelSeries" runat="server" Text='<%#Bind("LabelSeries")%>'></asp:Literal>
                    </p>
                    </asp:PlaceHolder>
                    
                    <asp:MultiView ID="mvCommentSummary" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vCommentCount" runat="server">
                            <p class="comment-count">
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Bind("Url")%>'>
                                    (<asp:Literal ID="Literal4" runat="server" Text='<%#Bind("ViewableCommentCount")%>'></asp:Literal>
                                    comments)
                                </asp:HyperLink>
                                <asp:HiddenField ID="hfBlogEntryCommentAllowStatusId" runat="server" Value='<%#Bind("AllowCommentStatus") %>' />
                            </p>
                        </asp:View>
                        <asp:View ID="vCommentsDisabled" runat="server">
                            <p class="comment-count">
                            <i>
                                Comments are disabled for this entry.</i>
                            </p>
                        </asp:View>
                    </asp:MultiView>
                </p>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:MultiView ID="mvCommentContent" runat="Server" ActiveViewIndex="0">
        <asp:View id="vNothing" runat="server"></asp:View>
        <asp:View ID="vShowComments" runat="server">
        <div id="comments">
            <h4 id="htmlCommentListHeader" runat="server" visible="false">
                Comments (<asp:Literal ID="litCommentCount" runat="server"></asp:Literal>)
            </h4>
            <asp:Repeater ID="rptComments" runat="server">
                <ItemTemplate>
                    <div id="commentBlock">
                        <p class="comment-person">
                            <asp:MultiView ID="mvCommentAuthor" runat="server" ActiveViewIndex="0">
                                <asp:View ID="vAuthorOnly" runat="server">
                                    <asp:Literal ID="litCommentAuthor2" runat="server" Text='<%#Bind("Name")%>'></asp:Literal>
                                </asp:View>
                                <asp:View ID="vWithWebsite" runat="server">
                                    <asp:HyperLink ID="hlCommentWebsite" runat="server" NavigateUrl='<%#Bind("Website")%>'>
                                        <asp:Literal ID="litCommentAuthor" runat="server" Text='<%#Bind("Name")%>'></asp:Literal>
                                    </asp:HyperLink>
                                </asp:View>
                            </asp:MultiView>
                        </p>
                        <p class="comment-body">
                            <asp:Literal ID="litCommentBody" runat="server" Text='<%#Bind("Text")%>'></asp:Literal>
                        </p>
                        <p class="comment-timestamp">
                            <asp:Literal ID="litCommentDateTime" runat="server" Text='<%#Bind("DateTime")%>'></asp:Literal>
                        </p>
                        <p class="comment-timestamp">
                            <asp:HyperLink ID="hlCommentPostUrl" runat="server" Text='<%#Bind("Name")%>' Visible="false"></asp:HyperLink>
                        </p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:MultiView ID="mvCommentInput" runat="server" ActiveViewIndex="0">
                <asp:View ID="vCommentForm" runat="server">
                    <div id="commentInput">
                    <p>
                        <label for="txtCommentAuthorName">Name *</label>
                        <asp:RequiredFieldValidator ID="rfvCommentAuthorName" runat="server" Display="Dynamic" Text="(required)" ControlToValidate="txtCommentAuthorName" />
                    </p>
                    <asp:TextBox id="txtCommentAuthorName" runat="server" CssClass="comment-input"></asp:TextBox>
                    
                    <p>
                    <label for="txtCommentAuthorEmail">E-mail *</label>
                    <asp:RequiredFieldValidator ID="rfvCommentAuthorEmail" runat="server" Display="Dynamic" Text="(required)" ControlToValidate="txtCommentAuthorEmail" />
                    <asp:CustomValidator ID="cvCommentAuthorEmail" runat="server" Display="Dynamic" Text="(e-mail must at least look real)" ControlToValidate="txtCommentAuthorEmail"></asp:CustomValidator>
                    </p>
                    <asp:TextBox id="txtCommentAuthorEmail" runat="server" CssClass="comment-input"></asp:TextBox>
                    
                    <label for="txtCommentWebsite">Website</label>
                    <asp:TextBox id="txtCommentWebsite" runat="server" CssClass="comment-input"></asp:TextBox>
                    
                    <p>
                        <label for="txtCommentText">Comment *</label>
                        <asp:RequiredFieldValidator ID="rfvCommentText" runat="server" Display="Dynamic" Text="(required -- isn't that the entire point?)" ControlToValidate="txtCommentText" />
                    </p>
                    <asp:TextBox Rows="10" Columns="50" id="txtCommentText" runat="server" TextMode="MultiLine" CssClass="comment-input"></asp:TextBox>
                    
                    <div id="commentPreview">
                    </div>
                    
                    <minima:MathVerification id="math01" runat="server"></minima:MathVerification>
                    
                    <input type="button" id="btnSubmitComment" value="Submit" />
                    <p>Notice: all comments are subject to moderation.</p>
                    <asp:Label ID="lblStatusMessage" runat="server" CssClass="comment-status"></asp:Label>
                    <asp:HiddenField ID="hfBlogEntryGuid" runat="server" Visible="true"></asp:HiddenField>
                    </div>
                    <div id="commentInputCompleted" style="display: none;">
                    <p>Comment saved.  All comments are moderated and may not show up for some time.</p>
                    </div>
                    <script type="text/javascript">
                    document.observe('dom:loaded', function( ) {
                        Prominax.AspNet.registerObject('hfBlogEntryGuid', '<%=hfBlogEntryGuid.ClientID%>');
                        Prominax.AspNet.registerObject('txtCommentAuthorName', '<%=txtCommentAuthorName.ClientID%>');
                        Prominax.AspNet.registerObject('txtCommentAuthorEmail', '<%=txtCommentAuthorEmail.ClientID%>');
                        Prominax.AspNet.registerObject('txtCommentWebsite', '<%=txtCommentWebsite.ClientID%>');
                        Prominax.AspNet.registerObject('txtCommentText', '<%=txtCommentText.ClientID%>');
                        Prominax.AspNet.registerObject('rfvCommentAuthorName', '<%=rfvCommentAuthorName.ClientID%>');
                        Prominax.AspNet.registerObject('rfvCommentAuthorEmail', '<%=rfvCommentAuthorEmail.ClientID%>');
                        Prominax.AspNet.registerObject('cvCommentAuthorEmail', '<%=cvCommentAuthorEmail.ClientID%>');
                        Prominax.AspNet.registerObject('rfvCommentText', '<%=rfvCommentText.ClientID%>');
                        //+
                        Prominax.AspNet.registerObject('lblStatusMessage', '<%=lblStatusMessage.ClientID%>');
                    });
                    </script>
                </asp:View>
                <asp:View ID="vCommentClosed" runat="server">
                    <p class="comment-status">
                        Comments have been closed for this entry.</p>
                </asp:View>
            </asp:MultiView>
        </div>
        </asp:View>
        <asp:View ID="vCommentsDisabled" runat="server">
            <p class="comment-status">Comments have been disabled for this entry.</p>
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript">
    document.observe('dom:loaded', function( ) {
        Initialization.init( );
    });
    </script>
</asp:Content>