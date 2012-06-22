<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true"
    CodeBehind="Bans.aspx.cs" Inherits="RuzWizardsSocialNetworkApplication.Administration.Bans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <!--<script src="../Scripts/ajaxfunc.js" type="text/javascript"></script> -->
    <!--<script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script> -->
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script> 
    <!--<script src="../Scripts/ajaxfunc.js" type="text/javascript"></script>-->
    <script type="text/javascript">
        function Start() {
            alert("JQUERY IS WORKING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            var tbUserId = $("#<%=tbUserID.ClientID%>");
            //tbUserId.change(function (e) {
            //alert("alalala");
            var userID = tbUserId.val();
            //alert("alalala");
            GetAllBans(userID);
            //});


            //alert("alalala");

            //            var ddlBans = $('#<%=ddlBans.ClientID%>');

            //            ddlBans.change(function (e) {
            //                var banId = ddlbans.val();
            //                if (banId != -1) {
            //                    // Get Employee Details
            //                    GetBanDetails(banId);
            //                }
            //                else {
            //                    $("#outputTable").hide();
            //                }
            //            });
        }

        //});

        function GetAllBans(userID) {
            var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>'
            //alert("in get all bans start");   
            var ddlBans = $('#<%=ddlBans.ClientID%>');
            //+ ban.FromDate + "  " + ban.ToDate +
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetAllBans",
                data: "{'userID':'" + userID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    var Bans = response.d;
                    $.each(Bans, function (index, ban) {
                        alert("FORMIROVKA LISTA");
                        ddlBans.append('<option value="' + ban.Key + '">' + ban.Value + '</option>');
                    });
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }

            });
            //alert("in get all bans end");
        }



        function GetBanDetails(banId) {
            $.ajax({
                type: "POST",
                url: "~/WebServices/SocialNetworkService.asmx/GetBanDetails",
                data: "{'banId':'" + banId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var ban = response.d;
                    $("#...").html(ban.Reason);
                    $("#...").html(ban.FromDate);
                    $("#...").html(ban.ToDate);
                    $("#...").show();
                },
                failure: function (msg) {
                    alert(msg);
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:TextBox ID="tbUserID" runat="server"></asp:TextBox>
    <asp:DropDownList ID="ddlBans" runat="server">
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="Start();return false;" />
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">
</asp:Content>
