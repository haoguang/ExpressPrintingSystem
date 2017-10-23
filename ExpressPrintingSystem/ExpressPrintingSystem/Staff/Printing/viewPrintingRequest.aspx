<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="viewPrintingRequest.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Printing.viewPrintingRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveUrl("~/styles/expendableTable.css") %>" type="text/css" />
    <script src="<%= Page.ResolveUrl("~/scripts/jquery-1.6.4.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/scripts/jquery.signalR-2.2.2.js") %>"></script>
    <script type="text/javascript">
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
            alert('test 1');
            function postBack() {

                __doPostBack("", "ReloadTable");
            }

            alert('test 2');
            
            var con = $.hubConnection();
            var hub = con.createHubProxy('PrintingRequestHub');
            hub.on('displayTable', function () {
                postBack();
            });
            
            alert('test 3');
            con.start();
            alert('test 4');
            
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Printing Request Room</h1>
    <h2>Requests Pending for Confirmation</h2>
    <asp:ListView ID="lvRequestConfirmation"  ItemPlaceholderID="PlaceHolderConfirm" runat="server" OnItemCommand="lvRequestConfirmation_ItemCommand">
        <LayoutTemplate>
            <table class="expendableTable"  width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="40px"></th>
                        <th style="display:none">Requestlist ID</th>
                        <th width="20%">Request ID</th>
                        <th width="20%">Type</th>
                        <th width="20%">Due Time</th>
                        <th width="20%">Payment State</th>
                        <th style="display:none">Item ID</th>                     
                        <th>Operation</th>
                    </tr>
                <asp:PlaceHolder runat="server" ID="PlaceHolderConfirm" />
            </table>
            
        </LayoutTemplate>
        <ItemTemplate>
                    <tr>
                            <td width="40px" class="btncolexp collapserequest">
                            </td>
                            <td style="display:none"><%#Eval("RequestLists[0].RequestlistID") %></td>
                            <td width="20%"><%#Eval("RequestID") %></td>
                            <td width="20%"><%#Eval("RequestLists[0].RequestType") %></td>
                            <td width="20%"><%#Eval("DueDateTime", "{0:dd/MM/yyyy HH:mm}") %></td>
                            <td width="20%"><%#(Eval("Payment")!= null) ? "Paid": "Unpaid" %></td>   
                            <td style="display:none"><%#Eval("RequestLists[0].RequestItemID") %></td>                      
                            <td class="operationColumn">
                                <asp:LinkButton ID="lbtnComplete" runat="server" CommandArgument='<%#Eval("RequestLists[0].RequestlistID") %>' CommandName="TaskComplete"  ForeColor="Red">
                                    <asp:Image ID="imgBox" ImageUrl="~/Images/square-outline-red.png" runat="server" /><asp:Label ID="lblComplete" runat="server" Text="Complete" Css="completeLabel"></asp:Label>
                                </asp:LinkButton>                          
                            </td>
                        </tr>
                        <tr class="childRow">
                            <td colspan="6">
                                <asp:ListView ID="lvPackageItems" DataSource='<%# Eval("RequestLists[0].DocumentList") %>' runat="server" ItemPlaceholderID="PlaceHolderPackageItems">
                                    <LayoutTemplate>

                                            <table>
                                                <tr>
                                                    <th style="display:none">Document ID</th>
                                                    <th>Document Name</th>
                                                    <th>Type</th>
                                                    <th>Print Quantity</th>
                                                    <th>Paper Type</th>
                                                    <th>View</th>
                                                </tr>
                                                <asp:PlaceHolder runat="server" ID="PlaceHolderPackageItems" />
                                            </table>

                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="display:none"><%#Eval ("Document.DocumentID") %></td>
                                            <td><%#Eval ("Document.DocumentName") %></td>
                                            <td><%#Eval ("Document.DocumentType") %></td>
                                            <td><%#Eval ("DocumentQuantity") %></td>
                                            <td><%#Eval ("DocumentPaperType") %></td>
                                            <td><asp:HyperLink ID="hplViewDocument" ToolTip="View Document" ImageUrl="~/Images/view.png" NavigateUrl='<%# getDocumentViewerUrl(Eval("Document.DocumentID")) %>' runat="server"></asp:HyperLink></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
        </ItemTemplate>
    </asp:ListView>
    <h2>Requests Pending for Printing</h2>
    <asp:GridView ID="gvPrint" runat="server"></asp:GridView>
</asp:Content>
