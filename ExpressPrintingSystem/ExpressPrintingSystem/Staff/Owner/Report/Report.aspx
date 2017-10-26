<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Report.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Generate Report</h1>
    <table>
        <tr>
            <td><asp:Label ID="lblReportName" runat="server" Text="Report Name:"></asp:Label></td>
            <td><asp:DropDownList ID="ddlReportName" runat="server">
                <asp:ListItem Value="SR">Sales Report</asp:ListItem>
                <asp:ListItem Value="SRR">Stock Remain Report</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr id="periodRow" runat="server" style="vertical-align:top;">
            <td><asp:Label ID="lblPeriod" runat="server" Text="Period :"></asp:Label></td>
            <td><asp:RadioButtonList ID="rblPeriod" runat="server">
                <asp:ListItem>Daily</asp:ListItem>
                <asp:ListItem>Monthly</asp:ListItem>
                <asp:ListItem>Yearly</asp:ListItem>
                <asp:ListItem>Custom</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr id="dateControl" runat="server" style="vertical-align:top;">
            <td>Date:</td>
            <td></td>
        </tr>
        
    </table>
</asp:Content>
