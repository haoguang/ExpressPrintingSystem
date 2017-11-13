<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="SendMessage.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Notify.SendMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Send Message</h1>

    <table>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" Text="Message :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMessage" TextMode="MultiLine" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTargetCustomer" runat="server" Text="Target Customer :"></asp:Label>
            </td>
            <td>
                
            </td>
        </tr>
    </table>
</asp:Content>
