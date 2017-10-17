<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="viewPrintingRequest.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Printing.viewPrintingRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Printing Request Room</h1>
    <h2>Requests Pending for Confirmation</h2>
    <asp:ListView ID="lvRequestConfirmation" DataKeyNames="RequestLists[0].RequestlistID" ItemPlaceholderID="PlaceHolderConfirm" runat="server">
        <LayoutTemplate>
            <table class="requestConfirm"  width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="32px"></th>
                        <th width="20%">Request ID</th>
                        <th width="20%">Type</th>
                        <th width="20%">Due Time</th>
                        <th width="20%">Payment State</th>
                        <th style="visibility:hidden">Item ID</th>                     
                        <th width="17%">Operation</th>
                    </tr>
            </table>
            <asp:PlaceHolder runat="server" ID="PlaceHolderConfirm" />
            
        </LayoutTemplate>
        <ItemTemplate>
            <div class="SUBDIV" runat="server">
                    <table class="requestcontent" width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="32px">
                                <div class="btncolexp collapseRequest">
                                    &nbsp;
                                </div>
                            </td>
                            <td width="20%"><%#Eval("RequestID") %></td>
                            <td width="20%"><%#Eval("RequestType") %></td>
                            <td width="20%"><%#Eval("DueDateTime") %></td>
                            <td width="20%"></td>   
                            <td style="visibility:hidden"><%#Eval("RequestItemID") %></td>                      
                            <td width="17%"></td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:ListView ID="lvPackageItems" DataSource='<%# Eval("DocumentList") %>' DataKeyNames="Document.DocumentID" runat="server" ItemPlaceholderID="PlaceHolderPackageItems">
                                    <LayoutTemplate>
                                        <div>
                                            <table>
                                                <tr>
                                                    <th>Document Name</th>
                                                    <th>Type</th>
                                                    <th>Print Quantity</th>
                                                    <th>Paper Type</th>
                                                </tr>
                                                <asp:PlaceHolder runat="server" ID="PlaceHolderPackageItems" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval ("Document.DocumentName") %></td>
                                            <td><%#Eval ("Document.DocumentType") %></td>
                                            <td><%#Eval ("DocumentQuantity") %></td>
                                            <td><%#Eval ("DocumentPaperType") %></td>
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
