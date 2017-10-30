using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ExpressPrintingSystem.Staff.Printing
{
    public class PrintingRequestHub : Hub
    {
        public static void refreshTable()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PrintingRequestHub>();
            context.Clients.All.displayTable();
        }
    }
}