<%@ Page Title="" Language="C#" MasterPageFile="~/LoginPage.master" AutoEventWireup="true" Inherits="Login" Theme="LoginPage" Codebehind="Login.aspx.cs" %>
<%@ Register TagPrefix="aspajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<%-- Усатов --%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <script type="text/javascript">
        function isValidEmail(email) {
            var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
            return pattern.test(email);
        }
        
        function vfIsValidEmail(oSrc, args) {
            if ($.trim(args.Value) == '' || !isValidEmail(args.Value)) {
                args.IsValid = false;
                $("[name $= 'tbxEmail']").removeClass("tbxNormal");
                $("[name $= 'tbxEmail']").addClass("tbxSelected");
            }
            else {
                args.IsValid = true;
                $("[name $= 'tbxEmail']").removeClass("tbxSelected");
                $("[name $= 'tbxEmail']").addClass("tbxNormal");
            }
        }

        function vfIsValidRegEmail(oSrc, args) {
            if ($.trim(args.Value) == '' || !isValidEmail(args.Value)) {
                args.IsValid = false;
            }
        }

        function vfIsValidPassword(oSrc, args) {
            if ($.trim(args.Value) == '' || args.Value.length < 4) {
                args.IsValid = false;
                $("[name $= 'tbxPassword']").removeClass("tbxNormal");
                $("[name $= 'tbxPassword']").addClass("tbxSelected");
            }
            else {
                args.IsValid = true;
                $("[name $= 'tbxPassword']").removeClass("tbxSelected");
                $("[name $= 'tbxPassword']").addClass("tbxNormal");
            }
        }

        function vfIsValidRegPassword(oSrc, args) {
            var password1 = $.trim(args.Value);
            if (password1.length < 4) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        // Измнить стиль поля Photo при отображении поля City 
        // (в ddlCountry выбрано значение отличное от 0)
        $(document).change(function () {
            var ddlSelectedValue = $("#<%= ddlCountry.ClientID %>").val();
            if (ddlSelectedValue > 0) {
                $('#trPhoto').removeClass("tblNormalRow");
                $('#trPhoto').addClass("tblAlternativeRow");
            }
            else {
                $('#trPhoto').removeClass("tblAlternativeRow");
                $('#trPhoto').addClass("tblNormalRow");
            }
        });

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
    <asp:UpdatePanel ID="upContent" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%-- Welcome content --%>
            <asp:Panel ID="pnlLogin" runat="server" Visible="true" CssClass="tblSideBorders"
                Width="98%">
                <center>
                    <h3>
                        Welcome to our Social Network!
                    </h3>
                </center>
                <center>
                    <img alt="" src="App_Themes/MainSkin/img/snw_logo.png" />
                </center>
            </asp:Panel>
            <%-- End welcome content --%>
            <%-- Registration content --%>
            <center>
                <asp:Panel ID="pnlRegistrtion" runat="server" Visible="false" HorizontalAlign="Center">
                    <table width="98%" cellpadding="2px" cellspacing="0" 
                        class="tblSideBorders" style="margin-top: -1px;">
                        <tr>
                            <td colspan="2" class="tblHeaderRow">
                                <span class="tblHeaderFont">Registration</span>
                            </td>
                        </tr>
                        <tr class="tblNormalRow">
                            <td style="width: 300px;">
                                Email
                            </td>
                            <td>
                                <asp:TextBox ID="tbxRegEmail" runat="server" MaxLength="40">
                                </asp:TextBox>
                                <asp:CustomValidator ID="csvRegEmail" runat="server" 
                                    ClientValidationFunction="vfIsValidRegEmail"
                                    ControlToValidate="tbxRegEmail" Display="Dynamic" 
                                    SetFocusOnError="True" ValidateEmptyText="True">
                                    <span class="errTextMessage">
                                        *<br />Incorrect email address
                                    </span>
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr class="tblAlternativeRow">
                            <td>
                                Password
                            </td>
                            <td>
                                <asp:TextBox ID="tbxRegPassword1" runat="server" 
                                    MaxLength="32" TextMode="Password">
                                </asp:TextBox>
                                <asp:CustomValidator ID="csvRegPassword1" runat="server" 
                                    ClientValidationFunction="vfIsValidRegPassword"
                                    ControlToValidate="tbxRegPassword1" Display="Dynamic" 
                                    SetFocusOnError="False"
                                    ValidateEmptyText="True">
                                    <span class="errTextMessage">
                                        *<br />Password must be longer then 4 chars
                                    </span>
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr class="tblNormalRow">
                            <td>
                                Repeat Password
                            </td>
                            <td>
                                <asp:TextBox ID="tbxRegPassword2" runat="server" 
                                    MaxLength="32" TextMode="Password">
                                </asp:TextBox>
                                <asp:CompareValidator ID="cmvRegPassword2" runat="server" 
                                    ControlToCompare="tbxRegPassword1"
                                    ControlToValidate="tbxRegPassword2" Display="Dynamic" 
                                    ErrorMessage="CompareValidator"
                                    SetFocusOnError="True">
                                    <span class="errTextMessage">
                                        *<br />Passwords not compare
                                    </span>
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr class="tblAlternativeRow">
                            <td>
                                First Name
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFirstName" runat="server" MaxLength="24">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFirstName" 
                                    runat="server" ControlToValidate="tbxFirstName"
                                    Display="Dynamic" ErrorMessage="RequiredFieldValidator" 
                                    SetFocusOnError="True">
                                    <span class="errTextMessage">
                                        *
                                    </span>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tblNormalRow">
                            <td>
                                Second Name
                            </td>
                            <td>
                                <asp:TextBox ID="tbxSecondName" runat="server" MaxLength="24">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSecondName" 
                                    runat="server" ControlToValidate="tbxSecondName"
                                    Display="Dynamic" ErrorMessage="RequiredFieldValidator" 
                                    SetFocusOnError="True">
                                    <span class="errTextMessage">
                                        *
                                    </span>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tblAlternativeRow">
                            <td>
                                Father Name
                            </td>
                            <td>
                                <asp:TextBox ID="tbxFatherName" runat="server" MaxLength="24">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tblNormalRow">
                            <td>
                                Nick Name
                            </td>
                            <td>
                                <asp:TextBox ID="tbxNickName" runat="server" MaxLength="36">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tblAlternativeRow">
                            <td>
                                Sex
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSex" runat="server">
                                    <asp:ListItem Selected="True" Value="0">Man</asp:ListItem>
                                    <asp:ListItem Value="1">Woman</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tblNormalRow">
                            <td>
                                Phone
                            </td>
                            <td>
                                <asp:TextBox ID="tbxPhone" runat="server" MaxLength="24">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Ins_Phone" 
                                    runat="server"
                                    ControlToValidate="tbxPhone" Display="Dynamic" 
                                    ErrorMessage="RequiredFieldValidator"
                                    SetFocusOnError="True">
                                    <span class="errTextMessage">
                                        *
                                    </span>
                                </asp:RequiredFieldValidator>
                                <%-- Провекрка на корректный номер телефона (регулярное выражение) --%>
                                <asp:RegularExpressionValidator ID="revPhone" runat="server" 
                                    ControlToValidate="tbxPhone"
                                    Display="Dynamic" ErrorMessage="RegularExpressionValidator" 
                                    SetFocusOnError="True"
                                    ValidationExpression="([+]?\d{1,3})?(([(]\d{2}[)])|(\d{2}))?\d{6,7}">
                                    <span class="errTextMessage">
                                        *
                                        <br />Input format: +1(23)4567890
                                    </span>
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="tblAlternativeRow">
                            <td>
                                Country
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True"
                                    DataSourceID="edsCountry" DataTextField="Name" 
                                    DataValueField="CountryID" 
                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="0">
                                        Not defined
                                    </asp:ListItem>
                                </asp:DropDownList>
                                <asp:EntityDataSource ID="edsCountry" runat="server" 
                                    ConnectionString="name=RusWizardsSocialNetworkDBEntities"
                                    DefaultContainerName="RusWizardsSocialNetworkDBEntities" 
                                    EnableFlattening="False"
                                    EntitySetName="Countries" OrderBy="it.[Name]" 
                                    Select="it.[CountryID], it.[Name]">
                                </asp:EntityDataSource>
                            </td>
                        </tr>
                        <tr id="trCity" runat="server" class="tblNormalRow" visible="false">
                            <td>
                                City
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCity" runat="server" 
                                    AppendDataBoundItems="True" DataSourceID="edsCity"
                                    DataTextField="Name" DataValueField="CityID">
                                    <asp:ListItem Selected="True" Value="0">
                                        Not defined
                                    </asp:ListItem>
                                </asp:DropDownList>
                                <asp:EntityDataSource ID="edsCity" runat="server" 
                                    ConnectionString="name=RusWizardsSocialNetworkDBEntities"
                                    DefaultContainerName="RusWizardsSocialNetworkDBEntities" 
                                    EnableFlattening="False"
                                    EntitySetName="Cities" OrderBy="it.[Name]" 
                                    Where="it.[CountryID] = 0">
                                </asp:EntityDataSource>
                            </td>
                        </tr>
                        <tr id="trPhoto" class="tblNormalRow">
                            <td>
                                Photo
                            </td>
                            <td>
                                <center>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <aspajax:AsyncFileUpload ID="fuPhoto" runat="server" 
                                            OnUploadedComplete="fuPhoto_UploadedComplete"
                                            UploaderStyle="Modern" Width="180px" />
                                    </asp:Panel>
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <asp:ImageButton ID="btnRegCompleteRegistration" runat="server" 
                                    ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_complete.png"
                                    OnClick="btnRegCompleteRegistration_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </center>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <%-- End registration content --%>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" Runat="Server">
    <asp:UpdatePanel ID="upLeftSideBar" runat="server">
        <ContentTemplate>
            <div>
                <p>
                    <%-- Email --%>
                    <asp:Literal ID="ltrEmail" runat="server">Email:</asp:Literal><br />
                    <asp:TextBox ID="tbxEmail" runat="server" MaxLength="40" 
                        ValidationGroup="vg1">
                    </asp:TextBox>
                    <%-- 
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                        ErrorMessage="RequiredFieldValidator" ValidationGroup="vg1" 
                        SetFocusOnError="True" Display="None"
                        ControlToValidate="tbxEmail">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" 
                        ErrorMessage="RegularExpressionValidator" ValidationGroup="vg1" 
                        ControlToValidate="tbxEmail" Display="None"
                        ValidationExpression="\b[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b" 
                        SetFocusOnError="True">*</asp:RegularExpressionValidator>
                    --%>
                    <asp:CustomValidator ID="csvEmail" runat="server" 
                        ClientValidationFunction="vfIsValidEmail" 
                        ControlToValidate="tbxEmail" Display="None" ValidationGroup="vg1" 
                        SetFocusOnError="True" ValidateEmptyText="True">*
                    </asp:CustomValidator>
                    <%-- Password --%>
                    <asp:Literal ID="ltrPassword" runat="server">Password:</asp:Literal><br />
                    <asp:TextBox ID="tbxPassword" runat="server" MaxLength="32" 
                        ValidationGroup="vg1" TextMode="Password">
                    </asp:TextBox>
                    <asp:CustomValidator ID="csvPassword" runat="server" 
                        ClientValidationFunction="vfIsValidPassword" 
                        ControlToValidate="tbxPassword" Display="None" ValidationGroup="vg1" 
                        SetFocusOnError="True" ValidateEmptyText="True">*
                    </asp:CustomValidator>
                </p>
                <p>
                    <asp:ImageButton ID="btnLogin" runat="server" 
                        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_login.png" 
                        ValidationGroup="vg1" />
                </p>
                <p>
                    <asp:ImageButton ID="btnRegistration" runat="server" 
                        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_registration.png" 
                        onclick="btnRegistration_Click" ValidationGroup="vg9" />
                </p>
                <p style="text-align:center; margin-left:-10px;">
                    <asp:LinkButton ID="btnLostPassword" 
                        runat="server" ValidationGroup="vg10">
                        Lost Password?
                    </asp:LinkButton>    
                </p>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    
</asp:Content>

