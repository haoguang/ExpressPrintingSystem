<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderReport.aspx.cs" Inherits="ExpressPrintingSystem.OrderReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="3000px" Width="100%">
        
            <LocalReport ReportPath="Report1.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet2" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet3" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet4" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet5" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet6" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet7" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet8" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet9" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet10" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet11" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet12" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="OrderReport" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="ExpressPrintingSystem.DataSet1TableAdapters.OrderReportTableAdapter"></asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
