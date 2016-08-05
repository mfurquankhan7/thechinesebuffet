using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace thechinesebuffet
{
    public partial class orderFailed : System.Web.UI.Page
    {
        protected Label lblFailureReason;

        protected Panel pnlTransactionDetails;

        protected Label lblVendorTxCodeReference;

        protected ImageButton proceed;

        private string status
        {
            get
            {
                return this.ViewState["strStatus"].ToString();
            }
            set
            {
                this.ViewState["strStatus"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string text = "";
            string a = "";
            if (base.Request.QueryString["reasonCode"] != null)
            {
                a = base.Request.QueryString["reasonCode"];
            }
            int num = Convert.ToInt32(Request.QueryString["VendorTxCode"].Substring(0, 1));
            clsSagePay clsSagePay = new clsSagePay(base.Request.Url.AbsoluteUri);
            clsPayments clsPayments = new clsPayments();
            if (a != "")
            {
                string value = Request.QueryString["reasonCode"].ToString();
                if (Convert.ToInt32(value) == 1)
                {
                    text = "We were unable to locate your transaction in our database. Please try your order again.  You have NOT been charged for this order.";
                }
                else if (Convert.ToInt32(value) == 2)
                {
                    text = "There was a problem validating the result from our Payment Gateway. To protect you we have cancelled the payment.  Please try your order again.";
                }
                else
                {
                    text = "The transaction process failed.  Please contact us with the date and time of your order and we will investigate.";
                }
            }
            else if (base.Request.QueryString["VendorTxCode"] != null)
            {
                if (base.Request.QueryString["VendorTxCode"].Length > 0)
                {
                    this.pnlTransactionDetails.Visible = true;
                    string text2 = Request.QueryString["VendorTxCode"].ToString();
                    if (text2.Length == 0)
                    {
                        base.Response.Clear();
                        base.Server.Transfer(clsSagePay.getRedirectBaseURLbyClientID(num.ToString()));
                        base.Response.End();
                    }
                    this.lblVendorTxCodeReference.Text = text2;
                    string sagePayPaymentStatusMessage = clsPayments.getSagePayPaymentStatusMessage(text2);
                    if (orderFailed.Left(sagePayPaymentStatusMessage, 8) == "DECLINED")
                    {
                        this.status = "DECLINED";
                        text = "You payment was declined by the bank.  This could be due to insufficient funds, or incorrect card details.";
                    }
                    else if (orderFailed.Left(sagePayPaymentStatusMessage, 7) == "ABORTED")
                    {
                        this.status = "ABORTED";
                        text = "You chose to Cancel your order on the payment pages.  If you wish to change your order and resubmit it you can do so here. If you have questions or concerns about ordering online, please call the numer on our contact us page.";
                    }
                    else if (orderFailed.Left(sagePayPaymentStatusMessage, 8) == "REJECTED")
                    {
                        this.status = "REJECTED";
                        text = "Your order did not meet our minimum fraud screening requirements. If you have questions about our fraud screening rules, or wish to contact us to discuss this, please call the numer on our contact us page.";
                    }
                    else if (orderFailed.Left(sagePayPaymentStatusMessage, 5) == "ERROR")
                    {
                        this.status = "ERROR";
                        text = "We could not process your order because our Payment Gateway service was experiencing difficulties.";
                    }
                    else
                    {
                        this.status = "UNKNOWN";
                        text = "The transaction process failed. Please contact us with the date and time of your order and we will investigate.";
                    }
                }
                else
                {
                    base.Response.Clear();
                    base.Server.Transfer(clsSagePay.getRedirectBaseURLbyClientID(num.ToString()));
                    base.Response.End();
                }
            }
            this.lblFailureReason.Text = text;
        }

        protected void clearSessions()
        {
            this.Session.Clear();
        }

        protected void proceed_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Clear();
            int num = Convert.ToInt32(base.Request.QueryString["VendorTxCode"].Substring(0, 1));
            clsSagePay clsSagePay = new clsSagePay(base.Request.Url.AbsoluteUri);
            this.clearSessions();
            base.Response.Redirect(clsSagePay.getRedirectBaseURLbyClientID(num.ToString()) + "/bolton/paymentsuccess.aspx?status=" + this.status);
            base.Response.End();
        }

        public static string Left(string param, int length)
        {
            return param.Substring(0, length);
        }
    }
}