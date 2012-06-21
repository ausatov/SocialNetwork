<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true"
    CodeBehind="Bans.aspx.cs" Inherits="RuzWizardsSocialNetworkApplication.Administration.Bans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <!--<script src="../Scripts/ajaxfunc.js" type="text/javascript"></script> -->
    <!--<script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script> -->
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/ajaxfunc.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:TextBox ID="tbUserID" runat="server"></asp:TextBox>
    <asp:DropDownList ID="ddlBans" runat="server">
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="Start();return false;" />
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">
</asp:Content>
