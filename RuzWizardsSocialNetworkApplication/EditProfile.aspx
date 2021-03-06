<%@ Page Title="" Language="C#" MasterPageFile="~/UserPage.master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Theme="MainSkin" Inherits="EditProfile" EnableEventValidation="false" %>
<%@ Register TagPrefix="cc" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/jQueryUI.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/ui/jquery-ui-1.8.21.custom.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="Scripts/jquery-cookies.js" type="text/javascript"></script>
    <script src="Scripts/main-scripts.js" type="text/javascript"></script>
    <script type="text/javascript">
        var webServiceURL = '<%=ResolveUrl("~/WebServices/FillItemsService.asmx")%>';

        $(function () {
            TabsFnc("#divTabs");
            DatepickerFnc("tbxBirthday");
            FillCountryXCitiesCascadeDropList("ddlCountry", "ddlCity", webServiceURL);
        });

        // Function executed after the file successfully uploaded.
        function OnAsyncUploadComplete(sender, args) {
            AsyncUploadComplete(sender, args, webServiceURL, "imgAvatar");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="innerContainer">

        <div id="divTabs" style="width:98%;">
            <ul>
                <li><a href="#tab-1">Main</a></li>
                <li><a href="#tab-2">Address</a></li>
                <li><a href="#tab-3">Avatar</a></li>
            </ul>
            <%-- Main container --%>
            <div id="tab-1">

                <center>
                <asp:Panel ID="pnlMain" runat="server" Width="50%">
                
                <asp:FormView ID="fvMain" runat="server" DataKeyNames="ID" 
                    DefaultMode="Edit" Width="100%" ondatabound="OnMainDataBound" 
                        onitemupdating="OnMainItemUpdating">
                    <EditItemTemplate>
                       
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false" />
                        <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserID") %>' Visible="false" />
                       
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litNickName" runat="server" Text="NickName:" />
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxNickName" MaxLength="50" runat="server" 
                                        Text='<%# Bind("NickName") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litFirstName" runat="server" Text="FirstName:" />    
                                </td>
                                <td>
                                     <asp:TextBox ID="tbxFirstName" MaxLength="50" runat="server" 
                                        Text='<%# Bind("FirstName") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litLastName" runat="server" Text="LastName:" />
                                </td>
                                <td>
                                     <asp:TextBox ID="tbxLastName" MaxLength="50" runat="server" 
                                        Text='<%# Bind("LastName") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litMiddleName" runat="server" Text="MiddleName:" />
                                </td>
                                <td>
                                     <asp:TextBox ID="tbxMiddleName" MaxLength="50" runat="server" 
                                        Text='<%# Bind("MiddleName") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                              
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litSex" runat="server" Text="Sex:" />
                                </td>
                                <td>
                                     <asp:DropDownList ID="ddlSex" runat="server" SelectedValue='<%# Eval("Sex") ?? 0 %>'
                                        Width="100%" OnDataBinding="OnSexDataBinding">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litPhone" runat="server" Text="Phone:" />
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxPhone" MaxLength="50" runat="server" Text='<%# Bind("Phone") %>' />
                                    <%-- Провекрка на корректный номер телефона (регулярное выражение) --%>
                                    <asp:RegularExpressionValidator ID="revPhone" runat="server" 
                                        ControlToValidate="tbxPhone"
                                        Display="Dynamic" ErrorMessage="" 
                                        SetFocusOnError="True"
                                        ValidationExpression="([+]?\d{1,3})?(([(]\d{2}[)])|(\d{2}))?\d{6,7}">
                                        <span class="errTextMessage">
                                            *
                                            <br />Input format: +1(23)4567890
                                        </span>
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litBirthday" runat="server" Text="Birthday:" />
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxBirthday" runat="server" 
                                        Text='<%# Eval("Birthday") %>' ClientIDMode="Static" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                                
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litDescription" runat="server" Text="Description:" />
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDescription" Height="100px" Width="96%" runat="server" MaxLength="4000" TextMode="MultiLine" 
                                        Text='<%# Bind("Description") %>' />
                                </td>
                            </tr>


                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                                
                            </tr>

                            <tr>
                                <td colspan="2" style="text-align:center;">
                                    <asp:ImageButton ID="btnSaveMain" runat="server" CommandName="Update"
                                        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_save.png" />
                                </td>
                            </tr>
                        </table>

                    </EditItemTemplate>
                </asp:FormView>
                </asp:Panel>    
                </center>
            </div>
            <%-- Address container --%>
            <div id="tab-2">
             

                <center>
                <asp:Panel ID="pnlAddress" runat="server" Width="50%">
                
                <asp:FormView ID="fvAddress" runat="server" DataKeyNames="ID" 
                    DefaultMode="Edit" Width="100%" onitemupdating="OnAddressItemUpdating">
                    <EditItemTemplate>
                        <asp:Label ID="lblID" runat="server" 
                            Text='<%# Bind("ID") %>' Visible="false"/>
                        <asp:Label ID="lblUserInfoID" runat="server" 
                            Text='<%# Bind("UserInfoID") %>' Visible="false" />

                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 40%;">
                                    <asp:Literal ID="litCountry" runat="server" Text="Country:">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" 
                                        SelectedValue='<%# Eval("CountryID") ?? 0 %>'
                                        Width="100%" ClientIDMode="Static" ondatabinding="OnCountryDataBinding" 
                                        DataTextField="Name" DataValueField="CountryID">
                                        <asp:ListItem Text="- Select Country -" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litCity" runat="server" Text="City:"></asp:Literal>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCity" runat="server" Width="100%" 
                                        AppendDataBoundItems="true"
                                        SelectedValue='<%# Eval("CityID") ?? 0 %>' 
                                        OnDataBinding="OnCityDataBinding"
                                        DataTextField="Name" DataValueField="CityID">
                                        <asp:ListItem Text="- Select City -" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litArea" runat="server" Text="Area:">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxArea" MaxLength="50" runat="server" Text='<%# Bind("Area") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litStreet" runat="server" Text="Street:">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxStreet" MaxLength="50" runat="server" Text='<%# Bind("Street") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litHome" runat="server" Text="Home:">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxHome" MaxLength="50" runat="server" Text='<%# Bind("Home") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litApartment" runat="server" Text="Apartment:">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxApartment" MaxLength="50" runat="server" 
                                        Text='<%# Bind("Apartment") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2" style="text-align:center;">
                                    <asp:ImageButton ID="btnSaveAddress" runat="server" CommandName="Update" 
                                        ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_save.png" />
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                    
                </asp:FormView>

                </asp:Panel>    
                </center>

            </div>

            <%-- Avatar container --%>
            <div id="tab-3">
                <cc:ToolkitScriptManager ID="ccScriptManager" runat="server"/>

                <center>
                <asp:Panel ID="pnlAvatar" runat="server" Width="50%">
                    <div style="padding-bottom:10px;">
                        <asp:Image ID="imgAvatar" runat="server" ImageUrl="~/Uploads/Photo/no_photo.jpg" CssClass="imgAvatar" />
                    </div>
                    <asp:Panel ID="pnlAsyncFileUpload" runat="server" CssClass="AsyncFileUploadControlPanel"> 
                        <cc:AsyncFileUpload ID="ccAsyncFileUpload" runat="server" 
                            CssClass="AsyncFileUploadControl" UploaderStyle="Traditional"
                            onuploadedcomplete="OnAsyncFileUploadedComplete" 
                            ToolTip="Upload Avatar" onclientuploadcomplete="OnAsyncUploadComplete" 
                            onclientuploaderror="OnAsyncUploadError" />
                        <asp:ImageButton ID="btnAsyncFileUpload" runat="server" 
                            ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_upload.png" 
                            CssClass="AsyncFileUploadControlButton" />
                    </asp:Panel>
                </asp:Panel>
                </center>
            </div> 
        </div>      
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">
</asp:Content>
