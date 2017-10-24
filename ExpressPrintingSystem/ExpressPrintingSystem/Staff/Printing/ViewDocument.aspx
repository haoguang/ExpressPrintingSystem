<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="ViewDocument.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Printing.ViewDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Document Viewer</h1>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <h2>Printing requirement</h2>
    <table>
        <tr>
            <td><asp:Label ID="lblColorText" runat="server" Text="Color :"></asp:Label></td>
            <td><asp:Label ID="lblColor" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblBothSideText" runat="server" Text="Print Option :"></asp:Label></td>
            <td><asp:Label ID="lblBothSide" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPaperTypeText" runat="server" Text="Paper Type :"></asp:Label></td>
            <td><asp:Label ID="lblPaperType" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblQuantityText" runat="server" Text="Print Quantity :"></asp:Label></td>
            <td><asp:Label ID="lblQuantity" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDescriptionText" runat="server" Text="Description :"></asp:Label></td>
            <td><asp:Label ID="lblDescription" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
    <GleamTech:DocumentViewer runat="server" 
        ID="documentViewer"
		Width="100%" Height="900px" /> 
</asp:Content>
