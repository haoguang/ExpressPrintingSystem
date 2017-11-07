<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="VerifyEmail.aspx.cs" Inherits="ExpressPrintingSystem.Staff.VerifyEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Email Verification</h1>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label>
    <table>
        <tr>
            <td><asp:Label ID="lblEmail" runat="server" Text="Email :"></asp:Label></td>
            <td><asp:TextBox ID="txtEmail" TextMode="Email" runat="server" AutoPostBack="True" OnTextChanged="txtEmail_TextChanged"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPassword" runat="server" Text="Password :"></asp:Label></td>
            <td><asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblEmailProvider" runat="server" Text="Email Provider :"></asp:Label></td>
            <td><asp:DropDownList ID="ddlEmailProvider" runat="server">
        <asp:ListItem Value="microsoft">Outlook</asp:ListItem>
        <asp:ListItem Value="google">Gmail</asp:ListItem>
        <asp:ListItem Value="yahoo">Yahoo</asp:ListItem>
        <asp:ListItem Value="yahoo plus">Yahoo Plus</asp:ListItem>
        <asp:ListItem Value="yahoo uk">Yahoo UK</asp:ListItem>
        <asp:ListItem Value="office365">Office365</asp:ListItem>
    </asp:DropDownList></td>
        </tr>
    </table>
    <asp:Button ID="btnSubmit" runat="server" Text="Login to Email" OnClick="btnSubmit_Click" />
    
</asp:Content>
