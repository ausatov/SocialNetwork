<%@ Page Title="" Language="C#" MasterPageFile="~/UserPage.master" AutoEventWireup="true" Inherits="UserProfile" Codebehind="UserProfile.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <table width="98%" class="tblSideBorders" cellpadding="2px" cellspacing="0">
        <tr>
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
                        <%-- Последнии фотографии (сейчас не реализуем) --%>
                        <div>
                            <span style="font-family: MS Sans Serif; font-weight: 900; font-size: 14px;">
                                <asp:Label ID="lblUserName" runat="server" Text="Label">
                                </asp:Label>
                            </span>
                        </div>
                        <div>
                            <br />
                            <asp:LinkButton ID="btnStatusMessage" runat="server" OnClick="btnStatusMessage_Click"
                                Text=""></asp:LinkButton>
                            <asp:Panel ID="pnlStatusMessage" runat="server" Visible="false">
                                <asp:DropDownList ID="ddlStatus" runat="server" OnDataBinding="ddlStatus_DataBinding">
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox ID="tbxStatusMessage" MaxLength="50" Width="100px" runat="server" Text="">
                                </asp:TextBox>
                                <br />
                                <asp:ImageButton ID="btnSaveStatusMessage" runat="server" ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_save.png"
                                    OnClick="btnSaveStatusMessage_Click" />
                            </asp:Panel>
                        </div>

                        <hr />

                        <div>
                        <%--DataKeyNames="ID" DataSourceID="edsPersonalInfo" убрать источник--%> 
                            <asp:DetailsView ID="dtlUserInfo" runat="server" Height="50px" Width="98%" 
                                AutoGenerateRows="false"
                                DataKeyNames="ID" DataSourceID="edsPersonalInfo"
                                ondatabound="dtlUserInfo_DataBound" GridLines="None">
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
                            <asp:EntityDataSource ID="edsPersonalInfo" runat="server" 
                                ConnectionString="name=SocialNetworkDBEntities" 
                                DefaultContainerName="SocialNetworkDBEntities" 
                                EnableFlattening="False" 
                                EntitySetName="PersonalInfoes">
                            </asp:EntityDataSource>
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
                                         <asp:DetailsView ID="dtlWall" runat="server" Height="50px" Width="100%" 
                                             AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="edsWall" 
                                             DefaultMode="Insert">
                                             <Fields>

                                                 <asp:TemplateField HeaderText="Special" Visible="False">
                                                     <InsertItemTemplate>

                                                         <asp:Label ID="lblFromID" runat="server" Text='<%# Bind("FromID") %>'></asp:Label>
                                                         <asp:Label ID="lblToID" runat="server" Text='<%# Bind("ToID") %>'></asp:Label>
                                                     
                                                     </InsertItemTemplate>
                                                     
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Message" SortExpression="Message">
                                                     <InsertItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ContentTypeID") %>'></asp:TextBox>
                                                        <%-- <asp:DropDownList ID="ddlWallItemContentType" runat="server">
                                                         </asp:DropDownList>--%>
                                                        <br />
                                                         <asp:TextBox ID="tbxMessage" runat="server" Text='<%# Bind("Message") %>'></asp:TextBox>
                                                     </InsertItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField ShowHeader="False">
                                                     <InsertItemTemplate>
                                                         <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                                             CommandName="Insert" Text="Insert"></asp:LinkButton>
                                                         &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                                             CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                                     </InsertItemTemplate>
                                                 </asp:TemplateField>
                                             </Fields>
                                        </asp:DetailsView>  
                                        
                                         
                                         <asp:GridView ID="grdWall" runat="server" Width="100%" AllowPaging="True" 
                                            AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="edsWall" 
                                            ShowHeader="False" BorderColor="#E3D5FF" BorderStyle="None" 
                                            BorderWidth="0px" onrowdatabound="grdWall_RowDataBound">
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
                                                        <asp:Label ID="lblBindFromID" runat="server" 
                                                            Text='<%# Bind("FromID") %>' Visible="false">
                                                        </asp:Label>

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
                                                                        OnClientClick=<%# "javascript:return confirm('Вы действительно хотите удалить запись?')" %> 
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


                                         <%-- Источник данных--%> 
                                        <asp:EntityDataSource ID="edsWall" runat="server" 
                                            ConnectionString="name=SocialNetworkDBEntities" 
                                            DefaultContainerName="SocialNetworkDBEntities" EnableDelete="True" 
                                            EnableFlattening="False" EnableInsert="True" EnableUpdate="True" 
                                            EntitySetName="WallBoardItems">
                                        </asp:EntityDataSource>
                                        
                        
                        
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
                       </div>






                    </div>
                
            </td>
        </tr>
    </table>



    <table width="100%">
    <tr>
    <td style="width:210px;">

        
        

    </td>
    <td>

        

        
    </td>
    </tr>
</table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" Runat="Server">
    <asp:Menu ID="Menu1" runat="server" StaticSubMenuIndent="10px" 
        BackColor="#FFFFFF" DynamicHorizontalOffset="2" Font-Names="Verdana" 
        Font-Size="0.8em" ForeColor="#284E98" StaticSelectedStyle-Width="100px" StaticMenuItemStyle-Width="100px">
        <DynamicHoverStyle BackColor="#284E98" ForeColor="White" />
        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <DynamicMenuStyle BackColor="#B5C7DE" />
        <DynamicSelectedStyle BackColor="#507CD1" />
        <Items>
            <asp:MenuItem Text="My Page" Value="My Page">
            </asp:MenuItem>
            <asp:MenuItem Text="Friends" Value="Friends">
            </asp:MenuItem>
            <%-- 
            <asp:MenuItem Enabled="False" Text="Photo" Value="Photo">
            </asp:MenuItem>
            <asp:MenuItem Enabled="False" Text="Video" Value="Video">
            </asp:MenuItem>
            <asp:MenuItem Enabled="False" Text="Audio" Value="Audio">
            </asp:MenuItem>
            <asp:MenuItem Enabled="False" Text="News" Value="News">
            </asp:MenuItem>
            <asp:MenuItem Enabled="False" Text="Applications" Value="Applications">
            </asp:MenuItem>
            --%>
            <asp:MenuItem Text="Messages" Value="Messages">
            </asp:MenuItem>
            <asp:MenuItem Text="Settings" Value="Settings">
            </asp:MenuItem>
        </Items>
    
        <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <StaticSelectedStyle BackColor="#e3d5ff" />
    
    </asp:Menu>
</asp:Content>

