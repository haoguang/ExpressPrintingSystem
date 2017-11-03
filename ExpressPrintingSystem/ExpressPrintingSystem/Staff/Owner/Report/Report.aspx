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
                <asp:Calendar ID="cdrDaily" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>   
            </td>
        </tr>
        <tr id="dateControlMonthly" runat="server" style="vertical-align:top;">
            <td>Date:</td>
            <td>
                <asp:DropDownList ID="ddlMonthMonthly" runat="server">
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlYearMonthly" runat="server">
                    <asp:ListItem>2017</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="dateControlYearly" runat="server" style="vertical-align:top;">
            <td>Date:</td>
            <td>
                 <asp:DropDownList ID="ddlYearYearly" runat="server">
                    <asp:ListItem>2017</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="dateControlCustom" runat="server" style="vertical-align:top;">
            <td>Date:</td>
            <td>
                <asp:Calendar ID="cdrFrom" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>   
            </td>
            <td>
                <asp:Calendar ID="cdrTo" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" OnClick="btnGenerate_Click" />
            </td>
        </tr>
        
    </table>
</asp:Content>
