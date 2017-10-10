<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="EditItem.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Item.EditItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Edit Item</h1>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label>
    <table>
            <tr>
                <td><asp:Label ID="lblID" runat="server" Text="ID:"/></td><td><asp:Label ID="lblItemID" runat="server" Text="Name:"/></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label></td>
                <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ForeColor="Red" ErrorMessage="Please fill up the the Name field."></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblPrice" runat="server" Text="Price (RM):"></asp:Label></td>
                <td><asp:TextBox ID="txtPrice"  runat="server"></asp:TextBox></td>
                <td><asp:CompareValidator ID="cvValidValuePrice" runat="server" Operator="GreaterThan" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPrice" ValueToCompare="0" ErrorMessage="Price cannot be less than 0."/>
                    <asp:CompareValidator ID="cvTypePrice" runat="server" Operator="DataTypeCheck" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPrice" Type="Currency" ErrorMessage="Please key in a valid number."/>
                    <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice" ForeColor="Red" Display="Dynamic" ErrorMessage="Price field cannot be empty."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblQuantity" runat="server" Text="Stock Quantity:"></asp:Label></td>
                <td><asp:TextBox ID="txtQuantity" TextMode="Number" runat="server"></asp:TextBox></td>
                <td><asp:CompareValidator ID="cvValidValueQuantity" runat="server" Operator="GreaterThan" ForeColor="Red" Display="Dynamic" ControlToValidate="txtQuantity" ValueToCompare="0" ErrorMessage="Quantity cannot be less than 0."/>
                    <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" ForeColor="Red" Display="Dynamic" ErrorMessage="Quantity field cannot be empty."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblSupplier" runat="server" Text="Supplier:"></asp:Label></td>
                <td><asp:TextBox ID="txtSupplier" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfvSupplier" runat="server" ControlToValidate="txtSupplier" ForeColor="Red" Display="Dynamic" ErrorMessage="Supplier field cannot be empty."></asp:RequiredFieldValidator></td>
            </tr>
        </table>
            <asp:Button ID="btnSubmit" runat="server" Text="Edit Item" OnClick="btnSubmit_Click" />
</asp:Content>
