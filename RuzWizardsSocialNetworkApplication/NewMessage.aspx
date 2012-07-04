<%@ Page Title="" Language="C#" MasterPageFile="~/UserPage.master" AutoEventWireup="true" CodeBehind="NewMessage.aspx.cs" Inherits="NewMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

	<link href="Styles/jQueryUI.css" rel="stylesheet" type="text/css" />
	<script src="Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="Scripts/ui/jquery.ui.button.js" type="text/javascript"></script>
	<script src="Scripts/ui/jquery.ui.position.js" type="text/javascript"></script>
	<script src="Scripts/ui/jquery.ui.autocomplete.js" type="text/javascript"></script>
	<script src="Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="Scripts/ui/jquery-ui-1.8.21.custom.js" type="text/javascript"></script>
	<script src="Scripts/autocomplete.js" type="text/javascript"></script>
	<script src="Scripts/main-scripts.js" type="text/javascript"></script>
	<script type="text/javascript">
		var imgAvatarID = "imgAvatar";
		var webServiceURL = '<%=ResolveUrl("~/WebServices/FillItemsService.asmx")%>';
		$(function () {
			InitAutocomplete(webServiceURL, imgAvatarID);
			SelectFirstItem("ddlReceiver");
			HideSmallAvatar(imgAvatarID);
			SetComboboxStyle("ddlReceiver");
		});
	</script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">


	<div class="innerContainer" style="padding: 10px 40px 10px 40px;">
		<div class="floatbarLeft" style="padding: 10px;">
			<asp:Image ID="imgAvatar" runat="server" CssClass="imgAvatar100" />
		</div>
		<div class="floatbarLeft" style="padding: 10px;">
			<div>
				<b>
					<asp:Label ID="lblReceiver" runat="server" Text="Receiver">
					</asp:Label>
				</b>
				<br />
				<div class="ui-widget">
					<asp:DropDownList ID="ddlReceiver" runat="server" AppendDataBoundItems="true" Width="400px">
						<asp:ListItem Value="0">-- input person name --</asp:ListItem>
					</asp:DropDownList>
				</div>
			</div>
			<div style="width: 98%">
				<b>
					<asp:Label ID="lblMessageHeader" runat="server" Text="Theme">
					</asp:Label>
				</b>
				<br />
				<asp:TextBox ID="tbxMessageHeader" MaxLength="100" runat="server" Width="400px"></asp:TextBox>
			</div>
		</div>
	</div>

	<div style="padding:10px;">
		<center>
		<asp:TextBox ID="tbxMessageText" MaxLength="4000" TextMode="MultiLine" Width="90%" Height="100px" runat="server"></asp:TextBox>
		</center>
	</div>

	<div style="text-align:center;">
		<asp:ImageButton ID="btnSendMessage" runat="server" 
			ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_send.png" 
			onclick="OnSendMessageClick" />
	</div>




</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">
</asp:Content>










































