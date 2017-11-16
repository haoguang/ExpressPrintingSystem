using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;


namespace ExpressPrintingSystem.Model.Messenging
{
    public class WhatappClass
    {
        //pending no api support for whatsapp

        
        public static void sendWhatsappWithURL(string phoneNo, string Message, HttpResponse response)
        {
            response.Write("<script LANGUAGE='JavaScript' >alert('System will redirect you to new tab to send message')</script>");
            response.Write("<script>window.open ('api.whatsapp.com/send?phone=6"+ phoneNo + "&text=" + Message + "','_blank');</script>");
        }
        

    }
}