<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Report.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Generate Report</h1>
    <table>
        <tr>
            <td><asp:Label ID="lblReportName" runat="server" Text="Report Name:"></asp:Label></td>
            <td><asp:DropDownList ID="ddlReportName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlReportName_SelectedIndexChanged">
                <asp:ListItem Value="SR">Sales Report</asp:ListItem>
                <asp:ListItem Value="SRR">Stock Remain Report</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr id="periodRow" runat="server" style="vertical-align:top;">
            <td><asp:Label ID="lblPeriod" runat="server" Text="Period :"></asp:Label></td>
            <td><asp:RadioButtonList ID="rblPeriod" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblPeriod_SelectedIndexChanged">
                <asp:ListItem>Daily</asp:ListItem>
                <asp:ListItem>Monthly</asp:ListItem>
                <asp:ListItem>Yearly</asp:ListItem>
                <asp:ListItem>Custom</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr id="stockControl" runat="server">
            <td>Show Stock Quantity Lower Than :</td>
            <td><asp:TextBox ID="txtStock" TextMode="Number" Text="0" runat="server"></asp:TextBox></td>
        </tr>
        <tr id="dateControlDaily" runat="server" style="vertical-align:top;">
            <td>Date:</td>
            <td>
                <asp:TextBox ID="txtDaily" TextMode="Date" runat="server"></asp:TextBox>   
            </td>
        </tr>
        <tr id="dateControlMonthly" runat="server" style="vertical-align:top;">
            <td>Date:</td>
            <td>
                <asp:TextBox ID="txtMonthly" TextMode="Month" runat="server"></asp:TextBox>           
            </td>
        </tr>
        <tr id="dateControlYearly" runat="server" style="vertical-align:top;">
            <td>Date:</td>
            <td>
                <asp:TextBox ID="txtYearly" TextMode="Month" runat="server"></asp:TextBox>  
            </td>
        </tr>
        <tr id="dateControlCustom" runat="server" style="vertical-align:top;">
            <td>Date:</td>
            <td>
                From <asp:TextBox ID="txtDateFrom" TextMode="Date" runat="server"></asp:TextBox>   
            </td>
            <td>
                To <asp:TextBox ID="txtDateTo" TextMode="Date" runat="server"></asp:TextBox>               
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" OnClick="btnGenerate_Click" />
            </td>
        </tr>
        
    </table>
</asp:Content>
