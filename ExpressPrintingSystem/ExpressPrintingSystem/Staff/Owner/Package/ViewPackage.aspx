<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="ViewPackage.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Package.ViewPackage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript">
        $(document).ready(function () {
            // THIS IS FOR HIDE ALL DETAILS ROW
            $(".SUBDIV table tr:not(:first-child)").not("tr tr").hide();
            $(".SUBDIV .btncolexp").click(function () {
                $(this).closest('tr').next('tr').toggle();
                //this is for change img of btncolexp button
                if ($(this).attr('class').toString() == "btncolexp collapsepromo") {
                    $(this).addClass('expandpromo');
                    $(this).removeClass('collapsepromo');
                }
                else {
                    $(this).removeClass('expandpromo');
                    $(this).addClass('collapsepromo');
                }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Packages View</h1>
    <p><asp:Label ID="lblMessage" ForeColor="Red" runat="server" Text=""></asp:Label></p>
    <p><asp:Label ID="lblSearch" runat="server" Text="Search :"></asp:Label><asp:TextBox ID="txtSearch" TextMode="Search" AutoPostBack="true" ToolTip="Press Enter after finish typing keywords" runat="server" Width="144px"></asp:TextBox></p>

    <asp:ListView ID="lvProductList" DataKeyNames="PackageID" ItemPlaceholderID="PlaceHolderPackage" runat="server">
        <LayoutTemplate>
            <table class="package"  width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="20%">Name</th>
                        <th width="20%">Price (RM)</th>
                        <th width="20%">Type</th>
                        <th width="20%">Files Support</th>                      
                        <th width="17%">Printing Price/Paper (RM)</th>
                    </tr>
            </table>
            <asp:PlaceHolder runat="server" ID="PlaceHolderPackage" />
            
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

</asp:Content>
