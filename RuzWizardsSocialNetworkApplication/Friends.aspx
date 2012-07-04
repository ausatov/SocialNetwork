<%@ Page Title="" Language="C#" MasterPageFile="~/UserPage.master" AutoEventWireup="true"
    CodeBehind="Friends.aspx.cs" Inherits="RuzWizardsSocialNetworkApplication.Friends" %>

<%@ Register Src="~/UserControls/FriendItem.ascx" TagName="FriendItem" TagPrefix="my" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/jQueryUI.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/ui/jquery-ui-1.8.21.custom.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#divTabs").tabs();
        });
        $(document).ready(function () {
            var startIndex = 2;
            function InfiniteScroll() {
                var userID = $("#<%=hfUserID.ClientID%>").val();
                $('#divPostsLoader').html('<img src="~/Styles/images/loader.gif">');
                var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
                //send a query to server side to present new content
                $.ajax({
                    type: "POST",
                    url: pageUrl + "/GetScrollData",
                    data: "{'userID': '" + userID + "', 'startIndex': '" + startIndex + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data != "") {
                            $('.divLoadData:last').append(data.d);
                            startIndex = startIndex + 1;
                        }
                        $('#divPostsLoader').empty();
                    }
                })
            };
            //When scroll down, the scroller is at the bottom with the function below and fire the lastPostFunc function
            $(window).scroll(function () {
                if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                    InfiniteScroll();
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div id="divTabs" style="width: 98%;">
        <ul>
            <li>
                <asp:HiddenField ID="hfUserID" runat="server" />
                <a href="#tab-1">All friends</a></li>
            <li><a href="#tab-2">Friends online</a></li>
            <li><a href="#tab-3">Friend invitations</a></li>
        </ul>
        <%-- All friends --%>
        <div id="tab-1">
            <asp:Repeater ID="rptFriends" runat="server">
                <ItemTemplate>
                    <my:FriendItem ID="ucFriendItem" runat="server" FriendID='<%# DataBinder.Eval(Container.DataItem, "FriendID") %>' />
                </ItemTemplate>
            </asp:Repeater>
            <div class="divLoadData">
            </div>
            <div id="divPostsLoader">
            </div>
        </div>
        <%-- Online friends --%>
        <div id="tab-2">
            <asp:Repeater ID="rptOnlineFriends" runat="server">
                <ItemTemplate>
                    <my:FriendItem ID="ucFriendItem" runat="server" FriendID='<%# DataBinder.Eval(Container.DataItem, "FriendID") %>' />
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <%-- Friend invitations --%>
        <div id="tab-3">
        ololo
            <%--<asp:Repeater ID="rptFriendInv" runat="server">
                <ItemTemplate>
                    <my:FriendItem ID="ucFriendItem" runat="server" FriendID='<%# DataBinder.Eval(Container.DataItem, "FriendID") %>' />
                </ItemTemplate>
            </asp:Repeater>--%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">
</asp:Content>
