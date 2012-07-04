<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FriendItem.ascx.cs" Inherits="RuzWizardsSocialNetworkApplication.UserControls.FriendItem" %>
 
<table width=100%>
    <tr>
        <td >
 <asp:Image ID="imgUserAvatar" Width="132px" runat="server" 
                ImageUrl="~/Uploads/Photo/no_photo.jpg" Height="139px"/>
        </td>
        <td >
            <asp:Label ID="lblStatus" runat="server" >Status<br /></asp:Label>
            <asp:HyperLink ID="hlFio" runat="server" >First &amp; last name></asp:HyperLink>
        </td>
        <td>
            <asp:LinkButton ID="btnSendMsg" runat="server" onclick="btnSendMsg_Click">Send message<br/></asp:LinkButton>
            <asp:LinkButton ID="btnDelete" runat="server" onclick="btnDelete_Click">Delete from friends</asp:LinkButton>
        </td>
    </tr>
</table>
