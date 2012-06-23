<%@ Page Title="" Language="C#" MasterPageFile="~/UserPage.master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="Styles/jQueryUI.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/ui/jquery-ui-1.8.21.custom.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
<script type="text/javascript">
        $(function () {
            $("#divTabs").tabs();
        });

        $(function () {
            $("[name $= 'tbxBirthday']").datepicker({ dateFormat: 'mm.dd.yy' });
        });

         
</script>


<script type="text/javascript">
    $().ready(function () {

        var pageUrl = '<%=ResolveUrl("~/WebServices/FillItemsService.asmx")%>';


        $.ajax({
            type: "POST",
            url: pageUrl + "/GetAllCountries",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (msg) {

                BindStateddl(msg.d)
            }
        });


        $("[name $= 'ddlCountry']").change(function () {
            //$("select#myselect").val();

            var CountryID = $("#ddlCountry > option:selected").val();
            alert(CountryID);
            
            $("[name $= 'ddlCountry']").html("");
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetAllCountryCities",
                data: "{countryID:'" + CountryID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    BindCityddl(msg.d)
                }
            });
        });

    });

    
    function BindStateddl(msg) {
        $.each(msg, function () {
            $("[name $= 'ddlCountry']").append($("<option></option>").val(this['CountryId']).html(this['Name']));
            
        });
    }
    function BindCityddl(msg) {
        $.each(msg, function () {
            $("[name $= 'ddlCity']").append($("<option></option>").val(this['CityID']).html(this['Name']));
            alert("22");
        });
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
                <asp:Panel ID="Panel2" runat="server" Width="50%">
                
                <asp:FormView ID="fvMain" runat="server" DataKeyNames="ID" 
                    DefaultMode="Edit" Width="100%" 
                    ondatabound="fvMain_DataBound">
                    <EditItemTemplate>
                       
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' Visible="false" />
                        <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserID") %>' Visible="false" />
                       
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litNickName" runat="server" Text="NickName:" />
                                </td>
                                <td>
                                    <asp:TextBox ID="NickNameTextBox" runat="server" 
                                        Text='<%# Bind("NickName") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litFirstName" runat="server" Text="FirstName:" />    
                                </td>
                                <td>
                                     <asp:TextBox ID="FirstNameTextBox" runat="server" 
                                        Text='<%# Bind("FirstName") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litLastName" runat="server" Text="LastName:" />
                                </td>
                                <td>
                                     <asp:TextBox ID="LastNameTextBox" runat="server" 
                                        Text='<%# Bind("LastName") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litMiddleName" runat="server" Text="MiddleName:" />
                                </td>
                                <td>
                                     <asp:TextBox ID="MiddleNameTextBox" runat="server" 
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
                                     <asp:DropDownList ID="ddlSex" runat="server" SelectedValue='<%# Bind("Sex") %>'
                                        Width="100%" OnDataBinding="ddlSex_OnDataBinding">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litPhone" runat="server" Text="Phone:" />
                                </td>
                                <td>
                                    <asp:TextBox ID="PhoneTextBox" runat="server" Text='<%# Bind("Phone") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litBirthday" runat="server" Text="Birthday:" />
                                </td>
                                <td>
                                    

                                    <asp:TextBox ID="tbxBirthday" runat="server" class="field" 
                                        Text='<%# Bind("Birthday") %>' ClientIDMode="Static" />
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
                                    <asp:TextBox ID="DescriptionTextBox" Height="100px" Width="96%" runat="server" MaxLength="4000" TextMode="MultiLine" 
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
                <asp:Panel ID="Panel1" runat="server" Width="50%">
                
                <asp:FormView ID="fvAddress" runat="server" DataKeyNames="ID" 
                    DefaultMode="Edit" Width="100%" ondatabinding="fvAddress_DataBinding">
                    <EditItemTemplate>
                        <asp:Label ID="tbxID" runat="server" 
                            Text='<%# Bind("ID") %>' Visible="false"/>
                        <asp:Label ID="tbxUserInfoID" runat="server" 
                            Text='<%# Bind("UserInfoID") %>' Visible="false" />

                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 40%;">
                                    <asp:Literal ID="litCountry" runat="server" Text="Country:">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCountry" runat="server" 
                                        DataValueField="CountryID" DataTextField="Name" AppendDataBoundItems="true"
                                        Width="100%" ClientIDMode="Static">
                                        <asp:ListItem Text="-- Select Country --" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litCity" runat="server" Text="City:"></asp:Literal>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCity" runat="server" Width="100%"
                                        DataValueField="CityID" DataTextField="Name" AppendDataBoundItems="true">
                                        <asp:ListItem Text="-- Select City --" Value="0"></asp:ListItem>
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
                                    <asp:TextBox ID="AreaTextBox" runat="server" Text='<%# Bind("Area") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litStreet" runat="server" Text="Street:">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="StreetTextBox" runat="server" Text='<%# Bind("Street") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litHome" runat="server" Text="Home:">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="HomeTextBox" runat="server" Text='<%# Bind("Home") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td align="right">
                                    <asp:Literal ID="litApartment" runat="server" Text="Apartment:">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="ApartmentTextBox" runat="server" 
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
                
                ImageInsert

            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">
</asp:Content>
