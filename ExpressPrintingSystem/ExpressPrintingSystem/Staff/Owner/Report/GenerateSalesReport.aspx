<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="GenerateSalesReport.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Report.GenerateSalesReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #report{
            width:100%;
        }
        #report table {
            padding:0;
            z-index:-1;
        }

        #report div{
            padding:0;
            z-index:-1;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="report">
    <rsweb:ReportViewer ID="rvSalesReport" CssClass="abc" runat="server" SizeToReportContent="True">
    </rsweb:ReportViewer>
        </div>
</asp:Content>
