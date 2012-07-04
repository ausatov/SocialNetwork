<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="PrivateMessageItem.ascx.cs" Inherits="PrivateMessageItem" %>


<table border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td valign="top" style="HEIGHT: 16px">
			<asp:TextBox id="txtDate" runat="server" Width="100px"></asp:TextBox>
		</td>
		<td valign="top" style="HEIGHT: 16px">
			<asp:Button id="btnSelect" runat="server" Text="Select"></asp:Button>
		</td>
	</tr>
	<tr>
		<td valign="top" colspan="2">
			<asp:Calendar id="calDate" runat="server" Width="160px" BackColor="White" DayNameFormat="FirstLetter" ForeColor="Black" Height="180px" Font-Size="8pt" Font-Names="Verdana" BorderColor="#999999" CellPadding="4" Visible="False"></asp:Calendar>
		</td>
	</tr>
</table>