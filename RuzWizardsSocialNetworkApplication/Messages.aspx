<%@ Page Title="" Language="C#" MasterPageFile="~/UserPage.master" AutoEventWireup="True" CodeBehind="Messages.aspx.cs" Inherits="Messages" %>





<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

    <script type="text/javascript">
        

        function OnReceivedClick() {
            $("[name $= 'btnShowReceived']").removeClass("btnReceivedPassive");
            $("[name $= 'btnShowReceived']").addClass("btnReceivedActive");
            $("[name $= 'btnShowPosted']").removeClass("btnPostedActive");
            $("[name $= 'btnShowPosted']").addClass("btnPostedPassive");


        }

//        $(document).ready(function () {
//            var heightElm = $("#wrapper").height();
//            var heightElm1 = $("#footer").height();



//            $('#divMainBorder').css("height", (heightElm - heightElm1).toString() + "px");

//        });

        
        
        function OnPostedClick() {
            $("[name $= 'btnShowReceived']").removeClass("btnReceivedActive");
            $("[name $= 'btnShowReceived']").addClass("btnReceivedPassive");
            $("[name $= 'btnShowPosted']").removeClass("btnPostedPassive");
            $("[name $= 'btnShowPosted']").addClass("btnPostedActive");
        }

        

    </script>


</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    
    

    <asp:Panel ID="pnlTopButtons" runat="server" CssClass="pnlTopButtonsPanel">
    
    
        <asp:Panel ID="pnlTopLeftButtons" runat="server" Width="260px" CssClass="floatbarLeft">
            <table width="100%">
                <tr>
                    <td>
                        <asp:ImageButton ID="btnShowReceived" runat="server" AlternateText="." ToolTip="Received"
                            CssClass="btnReceivedActive" OnClientClick="OnReceivedClick()" 
                            OnClick="btnShowReceived_Click" 
                            ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_received.png" />
                    </td>
                    <td>
                        |
                    </td>
                    <td>
                        <asp:ImageButton ID="btnShowPosted" runat="server" AlternateText="." ToolTip="Posted"
                            CssClass="btnPostedPassive" OnClientClick="OnPostedClick()" 
                            OnClick="btnShowPosted_Click" 
                            ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_posted_passive.png" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    

        <asp:Panel ID="pnlTopRightButtons" runat="server" CssClass="floatbarRight" Width="150px">
            <table width="100%">
                <tr>
                    <td>
                        <asp:ImageButton ID="btnNewMessage" runat="server" AlternateText="." ToolTip="New message"
                            CssClass="btnNew" 
                            ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_new.png" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    
   </asp:Panel>
    
    <div class="specialRowStyle" style="height:50px;">
        <div style="padding:12px 0 0 0;">
            <div class="floatbarLeft">
                <asp:Literal ID="litSelectText" runat="server">Select:</asp:Literal>
                <asp:LinkButton ID="btnSelectAll" runat="server">All</asp:LinkButton>
                <asp:LinkButton ID="btnSelectNew" runat="server">New</asp:LinkButton>
                <asp:LinkButton ID="btnSelectOld" runat="server">Old</asp:LinkButton>
            </div>
            <%--<div class="floatbarLeft">
                 <asp:ImageButton ID="btnDeleteMessage" runat="server" AlternateText="." ToolTip="Delete message"
                            CssClass="btnDelete" 
                            ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_delete.png" />
                 <asp:ImageButton ID="btnSetReadMessage" runat="server" AlternateText="." ToolTip="Set read message"
                            CssClass="btnRead" 
                            ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_read.png" />
            </div>--%>
            <div class="floatbarRight">
                <asp:Literal ID="litSearchText" runat="server">Search:</asp:Literal>
                <asp:TextBox ID="tbxSearch" runat="server" AutoCompleteType="Search" Width="100px"></asp:TextBox>
                <asp:ImageButton ID="btnFindMessages" runat="server" 
                    ImageUrl="~/App_Themes/MainSkin/img/btn_find.png" Width="16px" Height="16px" />
            </div>
        </div>
    </div>
   

    

    <asp:Repeater ID="rptMessages" runat="server" 
        onitemdatabound="OnMessagesItemDataBound">
        <ItemTemplate>
            <table width="100%">
                <asp:Label ID="lblMessageID" runat="server" Text='<%# Bind("MessageID") %>' Visible="false"></asp:Label>
                <tr>
                    <td>
                        <%--<div class="innerContainer">
                            <div class="triplebarLeft">
                                <div style="float: left; padding: 5px;">
                                    <asp:CheckBox ID="cbSelectItem" runat="server" />
                                </div>
                                <div style="float: left; padding: 5px;">
                                    <asp:Image ID="imgSmallAvatar" runat="server" Width="50px" Height="50px" />
                                </div>
                            </div>
                            <div class="triplebarMiddle" style="float: left; padding: 5px;">
                                <asp:LinkButton ID="btnSender" Text="btnSender" runat="server"></asp:LinkButton>
                                <br />
                                <asp:Label ID="lblStatus" runat="server" Text="lblStatus"></asp:Label>
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text="lblDate"></asp:Label>
                            </div>
                            <div class="triplebarRight">
                                <div style="float: left; padding: 5px;">
                                    <asp:Label ID="lblMessageContent" runat="server" Text="lblMessageContent"></asp:Label>
                                </div>
                                <div style="float: right; padding: 5px;">
                                    <asp:LinkButton ID="btnDelete" runat="server">Delete</asp:LinkButton>
                                </div>
                            </div>
                        </div>--%>
                        <table width="100%">
                            <tr>
                                <td style="width:10px;">
                                    <div style="float: left; padding: 2px;">
                                        <asp:CheckBox ID="cbSelectItem" runat="server" />
                                    </div>
                                </td>

                                <td style="width:60px;">
                                    <div style="float: left; padding: 5px;">
                                        <asp:Image ID="imgSmallAvatar" runat="server" Width="50px" Height="50px" />
                                    </div>
                                </td>

                                <td style="width:150px;">
                                    <asp:LinkButton CssClass="txtDescriptiveName" ID="btnSender" Text='<%# Bind("FromID") %>' runat="server"></asp:LinkButton>
                                    <br />
                                    <%--<asp:Label ID="lblStatus" runat="server" Text="lblStatus"></asp:Label>--%>
                                    <span class="txtDescriptive" id="UserStatus">online/offline</span>
                                    <br />
                                    <asp:Label CssClass="txtDescriptiveDate"  ID="lblDate" runat="server" Text='<%# Bind("SendDate") %>'></asp:Label>
                                </td>

                                <td>
                                     <div style="float: left; padding: 5px;">
                                        <asp:Label ID="lblMessageContent" runat="server" Text='<%# Bind("Text") %>'></asp:Label>
                                    </div>
                                </td>

                                <td style="width:50px;">
                                    <div style="float: right; padding: 5px;">
                                        <asp:LinkButton ID="btnDelete" runat="server">Delete</asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            
            
            
        </ItemTemplate>
        <SeparatorTemplate>
            <hr />
        </SeparatorTemplate>
    </asp:Repeater>




</asp:Content>





<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">

</asp:Content>
