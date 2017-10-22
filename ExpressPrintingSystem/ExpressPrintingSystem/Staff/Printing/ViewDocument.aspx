<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="ViewDocument.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Printing.ViewDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <GleamTech:DocumentViewer runat="server" 
        ID="documentViewer"
		Width="100%" Height="900px" /> 
</asp:Content>
