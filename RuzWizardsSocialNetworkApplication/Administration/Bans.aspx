<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Bans.aspx.cs" Inherits="RuzWizardsSocialNetworkApplication.Administration.Bans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <!--<script src="../Scripts/ajaxfunc.js" type="text/javascript"></script>-->
    <link href="../App_Themes/BanPage/ban.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var ddlBans = $('#<%=ddlBans.ClientID%>');
            ddlBans.hide();
            $("#<%=imgBtnSave.ClientID%>").hide();
            $("#<%=imgBtnCancel.ClientID%>").hide();
            $("#<%=tbReason.ClientID%>").hide();
            $("#<%=tbToDate.ClientID%>").datepicker();
            $("#<%=tbToDate.ClientID%>").hide();
        });
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


        //        $('#<%=ddlBans.ClientID%>').change(function (e) {

        //            var ddlBans = $('#<%=ddlBans.ClientID%>');
        //            var banId = ddlBans.val();
        //            if (banId != -1) {
        //                // Get Ban Details
        //                GetBanDetails(banId);
        //            }
        //            else {
        //                $("#outputTable").hide();
        //            }
        //        });

        function Start() {
            var tbUserMail = $("#<%=tbAuto.ClientID%>");
            var userMail = tbUserMail.val();
            var ddlBans = $('#<%=ddlBans.ClientID%>');
            $("#outputTable").hide();
            ddlBans.empty();
            ddlBans.append('<option value="-1">--Select from ad to ban date --</option>');
            GetAllBans(userMail);

            ddlBans.change(function (e) {
                OnCancelClick();
                var banId = ddlBans.val();
                if (banId != -1) {
                    // Get Ban Details
                    GetBanDetails(banId.toString());
                }
                else {
                    $("#outputTable").hide();
                }
            });
        }



        function GetAllBans(userMail) {
            var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
            var ddlBans = $('#<%=ddlBans.ClientID%>');
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetAllBans",
                data: "{'userEmail':'" + userMail + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    var Bans = response.d;
                    $.each(Bans, function (index, ban) {
                        ddlBans.append('<option value="' + ban.Key + '">' + ban.Value + '</option>');
                    });
                    ddlBans.show();
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }

            });
        }



        function GetBanDetails(banId) {
            var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetBan",
                data: "{'banID':'" + banId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    var ban = response.d;
                    $("#spnBanId").html(ban.ID);
                    $("#spnBanReason").html(ban.Reason);
                    //$("#<%=tbReason.ClientID%>").val(ban.Reason.toString());
                    var parsedFromDate = new Date(parseInt(ban.FromDate.toString().substr(6)));
                    var jsFromDate = new Date(parsedFromDate);
                    $("#spnFromDate").html(jsFromDate.getMonth()+'/'+jsFromDate.getDay()+'/'+jsFromDate.getFullYear());
                    var parsedToDate = new Date(parseInt(ban.ToDate.substr(6)));
                    var jsToDate = new Date(parsedToDate);
                    $("#spnToDate").html(jsToDate.getMonth() + '/' + jsToDate.getDay() + '/' + jsToDate.getFullYear());
                    $("#outputTable").show();
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }

            });
        }


        function Edit() {
            var reason = $('#spnBanReason').html();
            var todate = $('#spnToDate').html();
            $("#<%=imgBtnEditBan.ClientID%>").hide();
            $("#<%=imgBtnSave.ClientID%>").show();
            $("#<%=imgBtnCancel.ClientID%>").show();
            $("#<%=tbReason.ClientID%>").val(reason.toString());
            $("#<%=tbReason.ClientID%>").show();
            $("#<%=tbToDate.ClientID%>").val(todate);
            $("#<%=tbToDate.ClientID%>").show();

        }
        function OnSaveClick() {
            var banReason = $("#<%=tbReason.ClientID%>").val();
            var toDate = $("#<%=tbToDate.ClientID%>").val();
            var banId = $('#<%=ddlBans.ClientID%>').val();
            SaveParams(banReason, toDate, banId);

        }
        function SaveParams(banReason, toDate, banId) {
            var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
            $.ajax({
                type: "POST",
                url: pageUrl + "/UpdateBan",
                data: "{'banReason': '" + banReason + "', 'toDate': '" + toDate + "','banId':'" + banId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    OnCancelClick();
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }
            });
        }

        function OnCancelClick() {
            $("#<%=imgBtnEditBan.ClientID%>").show();
            $("#<%=imgBtnSave.ClientID%>").hide();
            $("#<%=imgBtnCancel.ClientID%>").hide();
            $("#<%=tbReason.ClientID%>").hide();
            $("#<%=tbToDate.ClientID%>").hide();
        }

        function OnDeleteClick() {
            if (confirm('Are you sure to delete this ban?') == true) {
                var banId = $('#<%=ddlBans.ClientID%>').val();
                Deleteban(banId);
            }
            else {
                return false;
            }
        }
        function Deleteban(banId) {
            var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
            $.ajax({
                type: "POST",
                url: pageUrl + "/DeleteBan",
                data: "{'banId':'" + banId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    $('#<%=ddlBans.ClientID%>').val('-1');
                    $("#outputTable").hide();
                    Start();
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
    <div id="ban-control">
        <!--<asp:TextBox ID="tbUserMail" runat="server"></asp:TextBox> -->
        <table>
            <tr>
                <td>
                    <div class="ui-widget">
                        <label for="tbAuto">
                            Enter Email:
                        </label>
                        <asp:TextBox ID="tbAuto" class="tb" runat="server">
                        </asp:TextBox>
                    </div>
                </td>
                <td>
                    <asp:ImageButton ID="imgBtnBans" runat="server" OnClientClick="Start();return false;"
                        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_getbans.png" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlBans" runat="server" CssClass="element-padding">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="border: solid 1px black; width: 300px; display: none; background-color: #f3f3f3"
                        cellpadding="4" cellspacing="0" id="outputTable">
                        <tr>
                            <td>
                                Ban ID:
                            </td>
                            <td>
                                <span id="spnBanId" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ban reason:
                                <asp:TextBox ID="tbReason" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <span id="spnBanReason" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                From date:
                            </td>
                            <td>
                                <span id="spnFromDate" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                To date:
                                <asp:TextBox ID="tbToDate" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <span id="spnToDate" />
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">
</asp:Content>
