<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FriendInvItem.ascx.cs" Inherits="RuzWizardsSocialNetworkApplication.UserControls.FriendInvItem" %>
 
<table width=100%>
    <tr>
        <td >
 <asp:Image ID="imgUserAvatar" Width="132px" runat="server" 
                ImageUrl="~/Uploads/Photo/no_photo.jpg" Height="139px"/>
        </td>
        <td >
            <asp:HyperLink ID="hlFio" runat="server" >First &amp; last name></asp:HyperLink>
        </td>
        <td>
            <asp:LinkButton ID="btnAddFriend" runat="server" onclick="OnbtnAddFriend_Click">Add friend<br></br></asp:LinkButton>
            <asp:LinkButton ID="btnReject" runat="server" onclick="OnbtnReject_Click">Reject invitation</asp:LinkButton>
        </td>
    </tr>
</table>

