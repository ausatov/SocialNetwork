<%@ Page Title="" Language="C#" MasterPageFile="~/UserPage.master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="RuzWizardsSocialNetworkApplication.Messages" %>





<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="EntityDataSource1">
        <ItemTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <div style="float: left; padding: 5px;">
                            <asp:CheckBox ID="cbSelectItem" runat="server" />
                        </div>
                        <div style="float: left; padding: 5px;">
                            <asp:Image ID="imgSmallAvatar" runat="server" Width="50px" Height="50px" />
                        </div>
                        <div style="float: left; padding: 5px;">
                            <asp:LinkButton ID="btnSender" runat="server">LinkButton</asp:LinkButton>
                            <br />
                            <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
                            <br />
                            <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div style="float: left; padding: 5px;">
                            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div style="float: right; padding: 5px;">
                            <asp:LinkButton ID="btnDelete" runat="server">Delete</asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </table>
            
            
            
            
        </ItemTemplate>
        <SeparatorTemplate>
            
            <hr />

        </SeparatorTemplate>
    </asp:Repeater>

    <asp:EntityDataSource ID="EntityDataSource1" runat="server" 
        ConnectionString="name=SocialNetworkDBEntities" 
        DefaultContainerName="SocialNetworkDBEntities" EnableDelete="True" 
        EnableFlattening="False" EntitySetName="Messages">
    </asp:EntityDataSource>
</asp:Content>





<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">

</asp:Content>
