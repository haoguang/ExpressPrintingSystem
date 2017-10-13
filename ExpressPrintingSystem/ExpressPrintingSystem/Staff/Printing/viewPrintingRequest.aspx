<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="viewPrintingRequest.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Printing.viewPrintingRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Printing Request Room</h1>
    <h2>Requests Pending for Confirmation</h2>
    <asp:ListView ID="lvRequestConfirmation" DataKeyNames="RequestID" ItemPlaceholderID="PlaceHolderConfirm" runat="server">
        <LayoutTemplate>
            <table class="requestConfirm"  width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="20%">RequestID</th>
                        <th width="20%">Due Time</th>
                        <th width="20%">Payment State</th>
                        <th width="20%">Customer Name</th>                      
                        <th width="17%">Operation</th>
                    </tr>
            </table>
            <asp:PlaceHolder runat="server" ID="PlaceHolderConfirm" />
            
        </LayoutTemplate>
        <ItemTemplate>
            <div class="SUBDIV" runat="server">
                    <table class="packagecontent" width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="20%"><%#Eval("PackageName") %></td>
                            <td width="20%"><%#Eval("PackagePrice") %></td>
                            <td width="20%"><%#Eval("PackageType") %></td>
                            <td width="20%"><%#Eval("PackageSupport") %></td>                         
                            <td width="17%"><%#Eval("PrintingPrice") %></td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:ListView ID="lvPackageItems" DataSource='<%# Eval("PackageItems") %>' runat="server" ItemPlaceholderID="PlaceHolderPackageItems">
                                    <LayoutTemplate>
                                        <div>
                                            <table>
                                                <tr>
                                                    <th>Item ID</th>
                                                    <th>Name</th>
                                                    <th>Quantity</th>
                                                </tr>
                                                <asp:PlaceHolder runat="server" ID="PlaceHolderPackageItems" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval ("Item.ItemID") %></td>
                                            <td><%#Eval ("Item.ItemName") %></td>
                                            <td><%#Eval ("Quantity") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                            
                    </table>
                </div>
        </ItemTemplate>
    </asp:ListView>
    <h2>Requests Pending for Printing</h2>
    <asp:GridView ID="gvPrint" runat="server"></asp:GridView>
</asp:Content>
