using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace thechinesebuffet
{
    public partial class PaymentSuccess : System.Web.UI.Page
    {
        protected HtmlGenericControl divProducts;

        protected Panel pnlSuccess;

        protected Literal litVendorTXCode;

        protected Literal litCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["status"] != null)
            {
                string a;
                if ((a = Request.QueryString["status"]) != null)
                {
                    if (a == "DECLINED")
                    {
                        this.Session.Clear();
                        base.Response.Redirect("/", true);
                        return;
                    }
                    if (a == "ABORTED")
                    {
                        base.Response.Redirect("/", true);
                        return;
                    }
                    if (a == "REJECTED")
                    {
                        this.Session.Clear();
                        base.Response.Redirect("/", true);
                        return;
                    }
                    if (a == "ERROR")
                    {
                        this.Session.Clear();
                        base.Response.Redirect("/", true);
                        return;
                    }
                    if (a == "UNKNOWN")
                    {
                        this.Session.Clear();
                        base.Response.Redirect("/", true);
                        return;
                    }
                }
                this.Session.Clear();
                base.Response.Redirect("/", true);
                return;
            }
            if (Request.QueryString["VendorTxCode"] != null && !this.Page.IsPostBack)
            {
                this.Session.Clear();
                this.pnlSuccess.Visible = true;
                this.litVendorTXCode.Text = Request.QueryString["VendorTxCode"];
                clsSagePay clsSagePay = new clsSagePay(Request.Url.AbsoluteUri);
                if (base.Request.QueryString["o"] == "p")
                {
                    this.litCode.Text = getOrderCodeFromTXCode(base.Request.QueryString["VendorTxCode"].ToString());
                }
                else
                {
                    clsSagePay.sendConfirmationEmail(Request.QueryString["VendorTxCode"].ToString(), 1);
                }
                clsSagePay.Dispose();
            }
        }
        public string getOrderCodeFromTXCode(string code)
        {
            string result = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(GetLatestConnectionString());
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("usp_NR_GetOrderCodeFromTXCode", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@code", code);
                result = sqlCommand.ExecuteScalar().ToString();
                sqlConnection.Close();
                sqlCommand.Dispose();
            }
            catch (Exception expr_57)
            {
                //ProjectData.SetProjectError(expr_57);
                Exception ex = expr_57;
                //functions.logError("Error getOrderCodeFromTXCode ", ex.Message, ex.StackTrace.ToString());
                //ProjectData.ClearProjectError();
            }
            return result;
        }
        public static string GetLatestConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
        }
    }
}