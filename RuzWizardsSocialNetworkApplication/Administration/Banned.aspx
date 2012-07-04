<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Banned.aspx.cs" Inherits="RuzWizardsSocialNetworkApplication.Administration.Banned" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblReason" runat="server" Text="Reason"></asp:Label>
&nbsp;<asp:TextBox ID="tbxReason" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblFromDate" runat="server" Text="From date"></asp:Label>
&nbsp;<asp:TextBox ID="tbxFromDate" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblTodate" runat="server" Text="To dae"></asp:Label>
&nbsp;<asp:TextBox ID="tbxToDate" runat="server"></asp:TextBox>
        <br />
&nbsp;
        <asp:Button ID="btnBan" runat="server" onclick="btnBan_Click" Text="ban" />
    
    </div>
    </form>
</body>
</html>
