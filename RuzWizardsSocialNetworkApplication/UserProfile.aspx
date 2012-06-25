<%@ Page Title="" Language="C#" MasterPageFile="~/UserPage.master" AutoEventWireup="true" Inherits="UserProfile" Codebehind="UserProfile.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <script type="text/javascript">

        function ConfirmDeleteWallBoardItem() {
            return confirm('Вы действительно хотите удалить запись?');
        }
    
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <table width="100%" class="" cellpadding="2px" cellspacing="0">
        <tr style="width:1000px;">
            <td class="tblHeaderRow">
                <div style="float: left; padding-left: 10px;">
                    <asp:Label ID="lblHeadUserName" runat="server" Text="">
                    Имя пользователя
                    </asp:Label>
                </div>
                <div style="float: right; padding-right: 10px;">
                    <asp:Label ID="lblHeadUserStatus" runat="server" Text="">
                    Статус
                    </asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="innerContainer">
                    <div class="specialSidebar" style="text-align: center;">
                        <div id="divUserAvatar">
                            <asp:Image ID="imgUserAvatar" Width="200px" runat="server" ImageUrl="~/Uploads/Photo/no_photo.jpg" />
                        </div>
                        <%-- --%>

                         <div style="padding-top: 10px; width: 200px;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="tblHeaderRow">
                                        <center>
                                            <span class="tblHeaderFont">Friends</span></center>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <%-- --%>

                    </div>

                    <%----------------------------------- --%>

                    <div class="mainSidebar">
                        <div>
                            <span style="font-family: MS Sans Serif; font-weight: 900; font-size: 14px;">
                                <asp:Label ID="lblUserName" runat="server" Text="Label">
                                </asp:Label>
                            </span>
                        </div>
                        <div>
                            <br />
                            <asp:LinkButton ID="btnStatusMessage" runat="server" OnClick="OnStatusMessageClick"
                                Text=""></asp:LinkButton>
                            <asp:Panel ID="pnlStatusMessage" runat="server" Visible="false">
                                <asp:DropDownList ID="ddlStatus" runat="server" OnDataBinding="OnStatusDataBinding">
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox ID="tbxStatusMessage" MaxLength="50" Width="100px" runat="server" Text="">
                                </asp:TextBox>
                                <br />
                                <asp:ImageButton ID="btnSaveStatusMessage" runat="server" ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_save.png"
                                    OnClick="OnSaveStatusMessageClick" />
                            </asp:Panel>
                        </div>

                        <hr />

                        <div>
                       
                            <asp:DetailsView ID="dvUserInfo" runat="server" Height="50px" Width="98%" 
                                AutoGenerateRows="false"
                                DataKeyNames="ID"
                                ondatabound="OnUserInfoDataBound" GridLines="None">
                                <Fields>
                                    <asp:TemplateField HeaderText="Birthday:" SortExpression="Birthday">
                                        <ItemTemplate>
                                            <%--Text='<%# Eval("Birthday") != null ? 
                                                Convert.ToDateTime(Eval("Birthday")).ToShortDateString() : 
                                                Eval("Birthday") %>'--%>
                                            <asp:Label ID="lblBirthday" runat="server" Text="">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sex:" SortExpression="Sex">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSex" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone:" SortExpression="Phone">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Country:" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City:" SortExpression="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Area:" SortExpression="Area">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArea" runat="server" Text="Area"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Street:" SortExpression="Street">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStreet" runat="server" Text="Street"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Home:" SortExpression="Home">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHome" runat="server" Text="Home"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apartment:" SortExpression="Apartment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApartment" runat="server" Text="Apartment"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                        </div>
                                      
                        <div style="padding-top:10px;">
                            <table width="101%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="tblHeaderRow">
                                        <center><span class="tblHeaderFont">Wall</span></center>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:FormView ID="fvWall" runat="server" DataKeyNames="ID" 
                                            DefaultMode="Insert" Width="100%" 
                                            onitemcommand="OnWallItemCommand">
                                            <InsertItemTemplate>
                                                <asp:DropDownList ID="ddlWallItemContentType" SelectedValue='<%# Bind("ContentTypeID") %>' runat="server">
                                                         </asp:DropDownList>
                                                        <br />
                                                         <asp:TextBox ID="tbxMessage" TextMode="MultiLine" Height="60px" MaxLength="4000" Width="98%" runat="server" Text='<%# Bind("Message") %>'></asp:TextBox>
                                                <br />
                                                <asp:ImageButton ID="btnSendWallMessage" 
                                                    CommandName="SendWallMessage" runat="server"
                                                    ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_send.png" />
                                            </InsertItemTemplate>
                                        </asp:FormView>

                                         <asp:GridView ID="grdWall" runat="server" Width="100%" AllowPaging="True" 
                                            AutoGenerateColumns="False" DataKeyNames="ID" 
                                            ShowHeader="False" BorderColor="#E3D5FF" BorderStyle="None" 
                                            BorderWidth="0px" onrowdatabound="OnWallRowDataBound" 
                                            onrowdeleting="OnWallRowDeleting">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                                                    SortExpression="ID" Visible="False" />
                                                <asp:TemplateField HeaderText="Message" SortExpression="Message">
                                                    <EditItemTemplate>

                                                        <asp:Label ID="lblFromID" runat="server" Text='<%# Bind("FromID") %>'></asp:Label>
                                        
                                                        <asp:TextBox ID="lblMessage" runat="server" Text='<%# Bind("Message") %>'></asp:TextBox>
                                                        <asp:Label ID="lblSendDate" runat="server" Text='<%# Bind("SendDate") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <table width="100%">
                                                            <tr style="width:98%;">
                                                                <td>
                                                                    <div class="txtDescriptive">
                                                                        <asp:Label ID="lblFromID" runat="server" 
                                                                            Text='<%# Eval("FromID") %>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="txtDescriptive" style="float:right;">
                                                                        <asp:ImageButton ID="btnDelete" runat="server" 
                                                                        CommandName="Delete" CommandArgument='<%# Bind("ID") %>'
                                                                        OnClientClick="JavaScript: return ConfirmDeleteWallBoardItem();" 
                                                                        Height="16px" Width="16px" ImageAlign="Top" 
                                                                        ImageUrl="~/App_Themes/MainSkin/img/icons/delete-icon.png" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div>
                                                                        <asp:Label ID="lblMessage" runat="server" 
                                                                            Text='<%# Bind("Message") %>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div class="txtDescriptive">
                                                                        Отправлено: 
                                                                        <asp:Label ID="lblSendDate" runat="server" 
                                                                            Text='<%# ((DateTime)Eval("SendDate")).ToLongDateString() %>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <%--Источник данных 
                                        <asp:EntityDataSource ID="dsWall" runat="server" 
                                            ConnectionString="name=SocialNetworkDBEntities" 
                                            DefaultContainerName="SocialNetworkDBEntities" EnableDelete="True" 
                                            EnableFlattening="False" EnableInsert="True" EnableUpdate="True" 
                                            EntitySetName="WallBoardItems">
                                        </asp:EntityDataSource>--%>

                                        <%--<asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1">
                                        </asp:GridView>
                                        
                                        <asp:ObjectDataSource 
                                            ID="ObjectDataSource1" runat="server" SelectMethod="SelectAllItems"
                                            TypeName="SocialNetwork.DataAccess.Repositories.WallBoardItemRepository">
                                        </asp:ObjectDataSource>


                                        
                                        
                                        --%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" Runat="Server">
</asp:Content>

