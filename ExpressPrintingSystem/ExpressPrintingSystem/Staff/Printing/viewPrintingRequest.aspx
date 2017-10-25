<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="viewPrintingRequest.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Printing.viewPrintingRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="<%= Page.ResolveUrl("~/styles/expendableTable.css") %>" type="text/css" />
    <script src="<%= Page.ResolveUrl("~/scripts/jquery-1.6.4.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/scripts/jquery.signalR-2.2.2.js") %>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            /* used to hide or unhide sub table*/
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

            /* SignalR function and connection*/
            function postBack(type) {
                __doPostBack("", type);
            }
            
            var con = $.hubConnection();
            var hub = con.createHubProxy('PrintingRequestHub');
            hub.on('displayTable', function () {
                postBack("ReloadTable");
            });

            con.start();

        });

        function sortTable(header) {
            __doPostBack("","SortTable;"+header);
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    

    <h1>Printing Request Room</h1>
    <h2>Requests Pending for Process</h2>
    <asp:ListView ID="lvRequestConfirmation"  ItemPlaceholderID="PlaceHolderConfirm" runat="server" OnItemCommand="lvRequestConfirmation_ItemCommand">
        <LayoutTemplate>
            <table class="expendableTable"  width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="40px" ></th>
                        <th style="display:none">Requestlist ID</th>
                        <th width="20%" style="cursor:pointer" onclick="sortTable('RequestID;0')">Request ID</th>
                        <th width="20%" style="cursor:pointer" onclick="sortTable('Type;0')">Type</th>
                        <th width="20%" style="cursor:pointer" onclick="sortTable('DueTime;0')">Due Time</th>
                        <th width="20%" style="cursor:pointer" onclick="sortTable('PaymentState;0')">Payment State</th>
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
                                <asp:LinkButton ID="lbtnComplete" runat="server" CommandArgument='<%#Eval("RequestLists[0].RequestlistID") %>' CommandName='<%# getCommenName((string)Eval("RequestLists[0].RequestStatus")) %>' ForeColor='<%# getLabelColor((string)Eval("RequestLists[0].RequestStatus")) %>'>
                                    <asp:Image ID="imgBox" ImageUrl=<%# string.Format("~/Images/{0}.png",Eval("RequestLists[0].RequestStatus"))%> runat="server" /><asp:Label ID="lblComplete" runat="server" Text='<%# getButtonText((string)Eval("RequestLists[0].RequestStatus")) %>' Css="completeLabel"></asp:Label>
                                </asp:LinkButton>                          
                            </td>
                        </tr>
                        <tr class="childRow">
                            <td colspan="6">
                                <div style="padding:0;width:30%;float:left;">
                                    <h2>Requirements</h2>
                                    <div style="padding:0;">
                                        <h3 style="text-align:left">Package :</h3>
                                        <ul style="text-align:left;">
                                            <li><%#Eval("RequestLists[0].Package.PackageName", "Name : {0}") %></li>
                                            <li>
                                                Items: <%# (int)Eval("RequestLists[0].Package.PackageItems.Count") == 0 ? "None" : "" %>
                                                <asp:ListView ID="lvItems" DataSource='<%# Eval("RequestLists[0].Package.PackageItems") %>' runat="server" ItemPlaceholderID="PlaceHolderPackageItems">
                                                    <LayoutTemplate><ul><asp:PlaceHolder runat="server" ID="PlaceHolderPackageItems" /></ul></LayoutTemplate>
                                                    <ItemTemplate><li><%#Eval("Item.ItemName")%><%#Eval("Quantity","({0})")%></li></ItemTemplate>
                                                </asp:ListView>
                                            </li>
                                        </ul>
                                    </div>                          
                                </div>

                                <div style="padding:0;margin:0;width:70%;overflow:hidden;">
                                    <asp:ListView ID="lvPackageItems" DataSource='<%# Eval("RequestLists[0].DocumentList") %>' runat="server" ItemPlaceholderID="PlaceHolderDocumentList">
                                    <LayoutTemplate>

                                            <table style="width:100%;">
                                                <tr>
                                                    <th style="display:none">Document ID</th>
                                                    <th>Document Name</th>
                                                    <th>Type</th>
                                                    <th>Print Quantity</th>
                                                    <th>Paper Type</th>
                                                    <th>View</th>
                                                </tr>
                                                <asp:PlaceHolder runat="server" ID="PlaceHolderDocumentList" />
                                            </table>

                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="display:none"><%#Eval ("Document.DocumentID") %></td>
                                            <td><%#Eval ("Document.DocumentName") %></td>
                                            <td><%#Eval ("Document.DocumentType") %></td>
                                            <td><%#Eval ("DocumentQuantity") %></td>
                                            <td><%#Eval ("DocumentPaperType") %></td>
                                            <td><asp:HyperLink ID="hplViewDocument" ToolTip="View Document" ImageUrl="~/Images/view.png" Target="_blank" NavigateUrl='<%# getDocumentViewerUrl(Eval("RequestlistID") ,Eval("Document.DocumentID")) %>' runat="server"></asp:HyperLink></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                </div>
                            </td>
                        </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <p style="font: italic  bold 16px sans-serif;">There is no request received currently.</p>
        </EmptyDataTemplate>
    </asp:ListView>
    <h2>Requests Pending for Collection</h2>
    <asp:ListView ID="lvPickUpRequest"  ItemPlaceholderID="PlaceHolderConfirm" runat="server" OnItemCommand="lvRequestConfirmation_ItemCommand">
        <LayoutTemplate>
            <table class="expendableTable"  width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="40px"></th>
                        <th style="display:none">Requestlist ID</th>
                        <th width="20%" style="cursor:pointer" onclick="sortTable('RequestID;1')">Request ID</th>
                        <th width="20%" style="cursor:pointer" onclick="sortTable('Type;1')">Type</th>
                        <th width="20%" style="cursor:pointer" onclick="sortTable('DueTime;1')">Due Time</th>
                        <th width="20%" style="cursor:pointer" onclick="sortTable('PaymentState;1')">Payment State</th>
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
                                <asp:LinkButton ID="lbtnComplete" runat="server" CommandArgument='<%#Eval("RequestLists[0].RequestlistID") %>' CommandName='<%# getCommenName((string)Eval("RequestLists[0].RequestStatus")) %>' ForeColor='<%# getLabelColor((string)Eval("RequestLists[0].RequestStatus")) %>'>
                                    <asp:Image ID="imgBox" ImageUrl=<%# string.Format("~/Images/{0}.png",Eval("RequestLists[0].RequestStatus"))%> runat="server" /><asp:Label ID="lblComplete" runat="server" Text='<%# getButtonText((string)Eval("RequestLists[0].RequestStatus")) %>' Css="completeLabel"></asp:Label>
                                </asp:LinkButton>                          
                            </td>
                        </tr>
                        <tr class="childRow">
                            <td colspan="6">
                                <div style="padding:0;width:30%;float:left">
                                    <h2>Requirements</h2>
                                    <div style="padding:0;">
                                        <h3 style="text-align:left">Package :</h3>
                                        <ul style="text-align:left;">
                                            <li><%#Eval("RequestLists[0].Package.PackageName", "Name : {0}") %></li>
                                            <li>
                                                Items: <%# (int)Eval("RequestLists[0].Package.PackageItems.Count") == 0 ? "None" : "" %>
                                                <asp:ListView ID="lvItems" DataSource='<%# Eval("RequestLists[0].Package.PackageItems") %>' runat="server" ItemPlaceholderID="PlaceHolderPackageItems">
                                                    <LayoutTemplate><ul><asp:PlaceHolder runat="server" ID="PlaceHolderPackageItems" /></ul></LayoutTemplate>
                                                    <ItemTemplate><li><%#Eval("Item.ItemName")%><%#Eval("Quantity","({0})")%></li></ItemTemplate>
                                                </asp:ListView>
                                            </li>
                                        </ul>
                                    </div>                          
                                </div>

                                <div style="padding:0;margin:0;width:70%;overflow:hidden;">
                                    <asp:ListView ID="lvPackageItems" DataSource='<%# Eval("RequestLists[0].DocumentList") %>' runat="server" ItemPlaceholderID="PlaceHolderDocumentList">
                                    <LayoutTemplate>

                                            <table style="width:100%;">
                                                <tr>
                                                    <th style="display:none">Document ID</th>
                                                    <th>Document Name</th>
                                                    <th>Type</th>
                                                    <th>Print Quantity</th>
                                                    <th>Paper Type</th>
                                                    <th>View</th>
                                                </tr>
                                                <asp:PlaceHolder runat="server" ID="PlaceHolderDocumentList" />
                                            </table>

                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="display:none"><%#Eval ("Document.DocumentID") %></td>
                                            <td><%#Eval ("Document.DocumentName") %></td>
                                            <td><%#Eval ("Document.DocumentType") %></td>
                                            <td><%#Eval ("DocumentQuantity") %></td>
                                            <td><%#Eval ("DocumentPaperType") %></td>
                                            <td><asp:HyperLink ID="hplViewDocument" ToolTip="View Document" ImageUrl="~/Images/view.png" Target="_blank" NavigateUrl='<%# getDocumentViewerUrl(Eval("RequestlistID") ,Eval("Document.DocumentID")) %>' runat="server"></asp:HyperLink></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                </div>
                                
                            </td>
                        </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <p style="font: italic bold 16px sans-serif;">There is no request received currently.</p>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
