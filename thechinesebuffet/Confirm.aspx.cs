using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net.Mail;

namespace thechinesebuffet
{
    public partial class Confirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string status = string.Empty;
            string reasoncode = string.Empty;
            string vendortxcode = string.Empty;
            if (!string.IsNullOrEmpty(Request.Form["VendorTxCode"]))
            {
                vendortxcode = Request.Form["VendorTxCode"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.Form["status"]))
            {
                status = Request.Form["status"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.Form["reasonCode"]))
            {
                reasoncode = Request.Form["reasonCode"].ToString();
            }

        }
    }
}