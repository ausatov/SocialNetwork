<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="RolesPage.aspx.cs" Inherits="RuzWizardsSocialNetworkApplication.Administration.RolesPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".tb").autocomplete({
                source: function (request, response) {
                    var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
                    $.ajax({
                        url: pageUrl + "/FetchEmailList",
                        data: "{ 'mail': '" + request.term + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item.Email
                                }
                            }))
                        },
                        error: function (xhr, status, error) {
                            var err = eval("(" + xhr.responseText + ")");
                            alert(err.Message);
                        }
                    });
                },
                minLength: 2
            });
        });
        function OnCheckRoleClick() {
            var tbUserMail = $("#<%=tbAuto.ClientID%>");
            var userMail = tbUserMail.val();
            CheckRole(userMail);
        }
        function CheckRole(userMail) {

        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:ImageButton ID="imgBtnNewRole" runat="server" OnClientClick=";return false;"
        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_new.png" />
    <asp:ImageButton ID="imgBtnEditRole" runat="server" OnClientClick=";return false;"
        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_edit.png" />
    <asp:ImageButton ID="imgBtnDeleteRole" runat="server" OnClientClick=";return false;"
        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_delete.png" />
    <asp:ImageButton ID="imgBtnGetRoles" runat="server" OnClientClick=";return false;"
        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_getroles.png" />
        <asp:ImageButton ID="imgBtnCheckRole" runat="server" OnClientClick="OnCheckRoleClick;return false;"
        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_checkrole.png" />
    <div class="ui-widget">
        <label for="tbAuto">
            Enter Email:
        </label>
        <asp:TextBox ID="tbAuto" class="tb" runat="server">
        </asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="label"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">
</asp:Content>
