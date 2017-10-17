<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="manageStaff.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.StaffManagement.manageStaff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Manage Staff</h1>
     <p><asp:Label ID="lblMessage" runat="server" ForeColor="Red"  Text=""></asp:Label></p>
     <p><asp:Label ID="lblSearch" runat="server" Text="Search :"></asp:Label><asp:TextBox ID="txtSearch" TextMode="Search" AutoPostBack="true" ToolTip="Press Enter after finish typing keywords" runat="server" Width="144px"></asp:TextBox></p>
     <asp:GridView ID="gvStaffList" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="StaffID" DataSourceID="sdsStaffList" AllowSorting="True" OnRowCommand="gvStaffList_RowCommand">
         <Columns>
             <asp:BoundField DataField="StaffID" HeaderText="Staff ID" ReadOnly="True" SortExpression="StaffID" />
             <asp:BoundField DataField="StaffName" HeaderText="Name" SortExpression="StaffName" />
             <asp:BoundField DataField="StaffEmail" HeaderText="Email" SortExpression="StaffEmail" />
             <asp:BoundField DataField="StaffNRIC" HeaderText="NRIC" SortExpression="StaffNRIC" />
             <asp:BoundField DataField="StaffDOB" HeaderText="Birthday" dataformatstring="{0:d MMMM yyyy}" SortExpression="StaffDOB" />
             <asp:BoundField DataField="StaffPhoneNo" HeaderText="Phone No" SortExpression="StaffPhoneNo" />
             <asp:ButtonField CommandName="Edit" HeaderText="Operation" ShowHeader="True" Text="Update Staff" />
         </Columns>
         <EmptyDataTemplate>No Record Available.</EmptyDataTemplate> 
    </asp:GridView>
    <asp:SqlDataSource ID="sdsStaffList" runat="server" ConnectionString="<%$ ConnectionStrings:printDBServer %>" SelectCommand="SELECT [StaffID], [StaffName], [StaffEmail], [StaffNRIC], [StaffDOB], [StaffPhoneNo] FROM [CompanyStaff] WHERE (([StaffRole] = @StaffRole) AND ([CompanyID] = @CompanyID) AND 
        (([StaffID] LIKE '%' + @keyword + '%') OR ([StaffName] LIKE '%' + @keyword + '%') OR ([StaffEmail] LIKE '%' + @keyword + '%') OR ([StaffNRIC] LIKE '%' + @keyword + '%') OR ([StaffPhoneNo] LIKE '%' + @keyword + '%')))">
        <SelectParameters>
            <asp:Parameter DefaultValue="Staff" Name="StaffRole" Type="String" />
            <asp:CookieParameter CookieName="CompanyID" Name="CompanyID" Type="String" />
            <asp:ControlParameter ControlID="txtSearch" Name="keyword" PropertyName="Text" DefaultValue="s" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

