<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Item.AddItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Add Item</h1>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label>
    <form id="addItemForm" runat="server">
        <table>
            <tr>
                <td><asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label></td>
                <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblPrice" runat="server" Text="Price:"></asp:Label></td>
                <td><asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblQuantity" runat="server" Text="Stock Quantity:"></asp:Label></td>
                <td><asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblSupplier" runat="server" Text="Supplier:"></asp:Label></td>
                <td><asp:TextBox ID="txtSupplier" runat="server"></asp:TextBox></td>
            </tr>
        </table>
            <asp:Button ID="btnSubmit" runat="server" Text="Add Item" OnClick="btnSubmit_Click" />
        
    </form>
</asp:Content>
