<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="ViewPackage.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Package.ViewPackage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveUrl("~/styles/expendableTable.css") %>" type="text/css" />
    <script language="javascript">
        $(document).ready(function () {
            // THIS IS FOR HIDE ALL DETAILS ROW
            $(".childRow").hide();
            $(".btncolexp").click(function () {
                $(this).closest('tr').next('tr').toggle();
                //this is for change img of btncolexp button
                if ($(this).attr('class').toString() == "btncolexp collapserequest") {
                    $(this).addClass('expandrequest');
                    $(this).removeClass('collapserequest');
                }
                else {
                    $(this).removeClass('expandrequest');
                    $(this).addClass('collapserequest');
                }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Packages View <asp:HyperLink ID="hyAdd" ToolTip="Add Package" ImageUrl="~/Images/add.png" NavigateUrl="~/Staff/Owner/Package/AddPackage.aspx" runat="server"></asp:HyperLink></h1>
    
    <p><asp:Label ID="lblMessage" ForeColor="Red" runat="server" Text=""></asp:Label></p>
    <p><asp:Label ID="lblSearch" runat="server" Text="Search :"></asp:Label><asp:TextBox ID="txtSearch" TextMode="Search" AutoPostBack="true" ToolTip="Press Enter after finish typing keywords" runat="server" Width="144px"></asp:TextBox></p>

    <asp:ListView ID="lvProductList"  ItemPlaceholderID="PlaceHolderConfirm" runat="server" >
        <LayoutTemplate>
            <table class="expendableTable"  width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="40px" ></th>
                        <th>Package ID</th>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Type</th>
                        <th>Document Supports</th>
                        <th>Price per Paper</th>
                        <th>Operation</th>
                    </tr>
                <asp:PlaceHolder runat="server" ID="PlaceHolderConfirm" />
            </table>
            
        </LayoutTemplate>
        <ItemTemplate>
                        <tr>
                            <td width="40px" class="btncolexp collapserequest">
                            </td>
                            <td><%# Eval("PackageID") %></td>    
                            <td><%# Eval("PackageName") %></td> 
                            <td><%# Eval("PackagePrice") %></td>
                            <td><%# Eval("PackageType") %></td> 
                            <td><%# Eval("PackageSupport") %></td> 
                            <td><%# Eval("PrintingPrice") %></td>
                            <td><asp:HyperLink ID="hplEditPackage" ToolTip="Edit Package" ImageUrl="~/Images/edit.png" NavigateUrl='<%# getEditPackageUrl((string)Eval("PackageID"))%>' runat="server"></asp:HyperLink></td> 
                        </tr>
                        <tr class="childRow">
                            <td colspan="8">

                                    <asp:ListView ID="lvPackageItems" DataSource='<%# Eval("packageItems") %>' runat="server" ItemPlaceholderID="PlaceHolderDocumentList">
                                    <LayoutTemplate>

                                            <table style="width:100%;">
                                                <tr>
                                                    <th>Item ID</th>
                                                    <th>Item Name</th>
                                                    <th>Quantity</th>
                                                </tr>
                                                <asp:PlaceHolder runat="server" ID="PlaceHolderDocumentList" />
                                            </table>

                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval ("Item.ItemID") %></td>
                                            <td><%#Eval ("Item.ItemName") %></td>
                                            <td><%#Eval ("Quantity") %></td>
                                        </tr>
                                    </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <p style="font: italic  bold 16px sans-serif;">There is no Item in the package. Edit to add more.</p>
                                        </EmptyDataTemplate>
                                </asp:ListView>
                               
                            </td>
                        </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <p style="font: italic  bold 16px sans-serif;">There is no request received currently.</p>
        </EmptyDataTemplate>
    </asp:ListView>

</asp:Content>
