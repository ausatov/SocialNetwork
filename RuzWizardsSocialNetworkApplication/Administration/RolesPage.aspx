<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="RolesPage.aspx.cs" Inherits="RuzWizardsSocialNetworkApplication.Administration.RolesPage" %>

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
                        data: "{ 'userEmail': '" + request.term + "' }",
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
            alert("ololo");
            CheckRole(userMail);
        }

        function OnGetRolesClick() {

            GetRoles();
            var ddlAllRoles = $("#<%=ddlAllRoles.ClientID%>");
            ddlAllRoles.change(function (e) {
                var roleID = ddlAllRoles.val();
                if (roleID != -1) {
                    // Get Ban Details
                    GetRoleDetails(roleID.toString());
                }
                else {
                    $("#outputTable").hide();
                }
            });

        }

        function GetRoleDetails(roleID) {
            var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetRole",
                data: "{'roleID':'" + roleID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    var role = response.d;
                    $("#spnRoleId").html(role.UserRoleID);
                    $("#spnRoleName").html(role.Name);
                    $("#spnMask").html(role.PrivelegeMask);
                    $("#outputTable").show();
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }

            });
        }

        function GetRoles() {
            var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
            var ddlAllRoles = $("#<%=ddlAllRoles.ClientID%>");
            ddlAllRoles.append('<option value="-1">--Select role --</option>');
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetAllRoles",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    var roles = response.d;
                    $.each(roles, function (index, role) {
                        ddlAllRoles.append('<option value="' + role.Key + '">' + role.Value + '</option>');
                    });
                    ddlAllRoles.show();
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }

            });
        }
        function CheckRole(userMail) {
            alert("yhaha");
            var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
            var ddluserRoles = $('#<%=ddlUserRoles.ClientID%>');
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetUserRoles",
                data: "{'userEmail':'" + userMail + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    var roles = response.d;
                    $.each(roles, function (index, role) {
                        ddluserRoles.append('<option value="' + role.Key + '">' + role.Value + '</option>');
                    });
                    ddluserRoles.show();
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }

            });
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
    <asp:ImageButton ID="imgBtnGetRoles" runat="server" OnClientClick="OnGetRolesClick();return false;"
        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_getroles.png" />
    <asp:ImageButton ID="imgBtnCheckRole" runat="server" OnClientClick="OnCheckRoleClick();return false;"
        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_checkrole.png" />
    <div class="ui-widget">
        <label for="tbAuto">
            Enter Email:
        </label>
        <asp:TextBox ID="tbAuto" class="tb" runat="server">
        </asp:TextBox>
    </div>
    <asp:DropDownList ID="ddlAllRoles" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlUserRoles" runat="server">
    </asp:DropDownList>
    <table style="border: solid 1px black; width: 300px; display: none; background-color: #f3f3f3"
        cellpadding="4" cellspacing="0" id="outputTable">
        <tr>
            <td>
                Role ID:
            </td>
            <td>
                <span id="spnRoleId" />
            </td>
        </tr>
        <tr>
            <td>
                Name:
                <asp:TextBox ID="tbxName" runat="server"></asp:TextBox>
            </td>
            <td>
                <span id="spnRoleName" />
            </td>
        </tr>
        <tr>
            <td>
                Mask
                <asp:TextBox ID="tbxMask" runat="server"></asp:TextBox>
            </td>
            <td>
                <span id="spnMask" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:ImageButton ID="imgBtnDeleteBan" runat="server" OnClientClick="OnDeleteClick();return false;"
                    ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_delete.png" />
                <asp:ImageButton ID="imgBtnEditBan" runat="server" OnClientClick="Edit();return false;"
                    ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_edit.png" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:ImageButton ID="imgBtnSave" runat="server" OnClientClick="OnSaveClick()" ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_save.png" />
                <asp:ImageButton ID="imgBtnCancel" runat="server" OnClientClick="OnCancelClick();return false;"
                    ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_cancel.png" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">
</asp:Content>
