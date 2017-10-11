<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="ViewItem.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Item.ViewItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>View Item</h1>
    <p><asp:Label ID="lblMessage" runat="server" ForeColor="Red"  Text=""></asp:Label></p>
    <p><asp:Label ID="lblSearch" runat="server" Text="Search :"></asp:Label><asp:TextBox ID="txtSearch" TextMode="Search" AutoPostBack="true" ToolTip="Press Enter after finish typing keywords" runat="server" Width="144px"></asp:TextBox></p>
    <asp:GridView ID="gvItemList" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ItemID" DataSourceID="sdsItem" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" ForeColor="#333333" OnRowCommand="gvItemList_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="ItemID" HeaderText="Item ID" ReadOnly="True" SortExpression="ItemID" />
            <asp:BoundField DataField="ItemName" HeaderText="Name" SortExpression="ItemName" />
            <asp:BoundField DataField="ItemPrice" HeaderText="Price (RM)" SortExpression="ItemPrice" />
            <asp:BoundField DataField="ItemStockQuantity" HeaderText="Stock Quantity" SortExpression="ItemStockQuantity" />
            <asp:BoundField DataField="ItemSupplier" HeaderText="Supplier" SortExpression="ItemSupplier" />
            <asp:ButtonField CommandName="Edit" HeaderText="Operation" ShowHeader="True" Text="Edit Item" />
        </Columns>
        <EditRowStyle BackColor="#99BBFF" Font-Bold="True" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#b3b3cc" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:SqlDataSource ID="sdsItem" runat="server" ConnectionString="<%$ ConnectionStrings:printDBServer %>" SelectCommand="SELECT [ItemID], [ItemName], [ItemPrice], [ItemStockQuantity], [ItemSupplier] FROM [Item] WHERE ([ItemID] LIKE '%' + @keyword + '%')  OR ([ItemName] LIKE '%' + @keyword + '%') OR ([ItemSupplier] LIKE '%' + @keyword + '%')">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" Name="keyword" PropertyName="Text" DefaultValue="i" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
