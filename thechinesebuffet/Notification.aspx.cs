using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace thechinesebuffet
{
    public partial class Notification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string text = "";
            try
            {

                    string text2 = Request.Form["Status"].ToString();
                    string text3 = Request.Form["VendorTxCode"].ToString();
                    string text4 = Request.Form["VPSTxId"].ToString();

                    clsSagePay clsSagePay = new clsSagePay(Request.Url.AbsoluteUri);
                    clsPayments clsPayments = new clsPayments();
                    if (!(text3 == "") && !(text4 == ""))
                    {
                        text = clsPayments.getSagePaySecurityKeyForComparison(text3, text4);
                    }
                    if (text.Length == 0)
                    {
                        Response.Clear();
                        Response.ContentType = "text/plain";
                        Response.Write("Status=INVALID" + Environment.NewLine);
                        if (clsSagePay.ConnectTo == "LIVE")
                        {
                            //Response.Write("RedirectURL=" + clsSagePay.YourSiteFQDN + "/restaurants/bolton/orderfailed.html?reasonCode=001" + Environment.NewLine);
                            Response.Write("RedirectURL=http://thechinesebuffet.com/orderFailed.aspx?VendorTxCode="+text3+"&reasonCode=001" + Environment.NewLine);
                        }
                        else
                        {
                            //Response.Write("RedirectURL=" + clsSagePay.YourSiteInternalFQDN + "/restaurants/bolton/orderfailed.html?reasonCode=001" + Environment.NewLine);
                            Response.Write("RedirectURL=http://thechinesebuffet.com/orderFailed?VendorTxCode=" + text3 + "&reasonCode=001" + Environment.NewLine);
                        }
                        Response.Write("StatusDetail=Unable to find the transaction in our database." + Environment.NewLine);
                        Response.End();
                    }
                    else
                    {
                        string text5 = "";
                        string text6 = "";
                        string text7 = "";
                        string text8 = "";
                        string text9 = "";
                        string text10 = "";
                        string text11 = "";
                        string text12 = "";
                        string text13 = "";
                        string text14 = "";
                        string text15 = "";
                        string text16 = "";
                        //clsSagePay.cleanInput(Request.Form["VPSSignature"].ToString());
                        string strRawText = Request.Form["StatusDetail"].ToString();
                        if (!string.IsNullOrEmpty(Request.Form["TxAuthNo"]))
                        {
                            text5 = Request.Form["TxAuthNo"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Request.Form["AVSCV2"]))
                        {
                            text6 = Request.Form["AVSCV2"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Request.Form["AddressResult"]))
                        {
                            text7 = Request.Form["AddressResult"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Request.Form["PostCodeResult"]))
                        {
                            text8 = Request.Form["PostCodeResult"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Request.Form["CV2Result"]))
                        {
                            text9 = Request.Form["CV2Result"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Request.Form["GiftAid"]))
                        {
                            text10 = Request.Form["GiftAid"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Request.Form["3DSecureStatus"]))
                        {
                            text11 = Request.Form["3DSecureStatus"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Request.Form["CAVV"]))
                        {
                            text12 = Request.Form["CAVV"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Request.Form["AddressStatus"]))
                        {
                            text13 = Request.Form["AddressStatus"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Request.Form["PayerStatus"]))
                        {
                            text14 = Request.Form["PayerStatus"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Request.Form["CardType"]))
                        {
                            text15 = Request.Form["CardType"].ToString();
                        }
                        if (!string.IsNullOrEmpty(Request.Form["Last4Digits"]))
                        {
                            text16 = Request.Form["Last4Digits"].ToString();
                        }
                        string password = string.Concat(new string[]
                    {
                        text4,
                        text3,
                        text2,
                        text5,
                        clsSagePay.VendorName,
                        text6,
                        text,
                        text7,
                        text8,
                        text9,
                        text10,
                        text11,
                        text12,
                        text13,
                        text14,
                        text15,
                        text16
                    });
                        FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                        bool flag = false;
                        string failureReason;
                        if (text2 == "OK")
                        {
                            failureReason = "";
                            flag = true;
                        }
                        else if (text2 == "NOTAUTHED")
                        {
                            failureReason = "DECLINED - The transaction was not authorised by the bank.";
                        }
                        else if (text2 == "ABORT")
                        {
                            failureReason = "ABORTED - The customer clicked Cancel on the payment pages, or the transaction was timed out due to customer inactivity.";
                        }
                        else if (text2 == "REJECTED")
                        {
                            failureReason = "REJECTED - The transaction was failed by your 3D-Secure or AVS/CV2 rule-bases.";
                        }
                        else if (text2 == "AUTHENTICATED")
                        {
                            failureReason = "AUTHENTICATED - The transaction was successfully 3D-Secure Authenticated and can now be Authorised.";
                        }
                        else if (text2 == "REGISTERED")
                        {
                            failureReason = "REGISTERED - The transaction was could not be 3D-Secure Authenticated, but has been registered to be Authorised.";
                        }
                        else if (text2 == "ERROR")
                        {
                            failureReason = "ERROR - There was an error during the payment process.  The error details are: " + clsSagePay.SQLSafe(strRawText);
                        }
                        else
                        {
                            failureReason = "UNKNOWN - An unknown status was returned from Sage Pay.  The Status was: " + clsSagePay.SQLSafe(text2) + ", with StatusDetail:" + clsSagePay.SQLSafe(strRawText);
                        }
                        clsPayments.UpdatePaymentStatus(text4, text3, flag, failureReason);
                        string text17 = "bolton";
                        if (flag)
                        {
                            clsOrders clsOrders = new clsOrders();
                            string text18 = clsOrders.IsReservationOrder(text4).ToString();
                            if (text18 != "")
                            {
                                DataSet dataSet = new DataSet();
                                dataSet = clsOrders.getReservationByReservationID(Convert.ToInt32(text18));
                                if (dataSet.Tables[0].Rows.Count > 0)
                                {
                                    string text19 = "";
                                    string text20 = "";
                                    DateTime dReservationDate = default(DateTime);
                                    if (dataSet.Tables[0].Rows[0]["DateTimeReservation"] != DBNull.Value)
                                    {
                                        dReservationDate = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["DateTimeReservation"]);
                                        text19 = dReservationDate.Hour.ToString();
                                        text20 = dReservationDate.Minute.ToString();
                                    }
                                    clsOrders.UpdateReservationNotes(Convert.ToInt32(text18));
                                    var conn = GetLatestConnectionString();
                                    completeReservation(Convert.ToInt32(dataSet.Tables[0].Rows[0]["RestaurantFK"]), dataSet.Tables[0].Rows[0]["Fullname"].ToString(), dataSet.Tables[0].Rows[0]["Mobile"].ToString(), dataSet.Tables[0].Rows[0]["Email"].ToString(), dReservationDate, text19, text20, Convert.ToInt32(dataSet.Tables[0].Rows[0]["NumberOfGuests"]), dataSet.Tables[0].Rows[0]["AdditionalNotes"] + " Deposit paid ", dataSet.Tables[0].Rows[0]["vPostCode"].ToString(), conn);
                                    SetReservationConfirmed(Convert.ToInt32(text18), conn);
                                    string text21 = "";
                                    string text22 = "";
                                    string text23 = "";
                                    int num = Convert.ToInt32(dataSet.Tables[0].Rows[0]["RestaurantFK"]);
                                    if (base.Request.Url.AbsoluteUri.ToString().Contains("thechinesebuffet.com"))
                                    {
                                        if (num == 2)
                                        {
                                            text17 = "wigan";
                                        }
                                        else if (num == 8)
                                        {
                                            text17 = "blackpool";
                                        }
                                        else if (num == 11)
                                        {
                                            text17 = "bury";
                                        }
                                        else if (num == 13)
                                        {
                                            text17 = "darlington";
                                        }
                                        else if (num == 14)
                                        {
                                            text17 = "huddersfield";
                                        }
                                        else if (num == 10)
                                        {
                                            text17 = "wrexham";
                                        }
                                        else if (num == 1)
                                        {
                                            text17 = "bolton";
                                        }
                                        else if (num == 15)
                                        {
                                            text17 = "st helens";
                                        }
                                        else if (num == 16)
                                        {
                                            text17 = "wakefield";
                                        }
                                        else if (num == 9)
                                        {
                                            text17 = "preston";
                                        }
                                        else if (num == 18)
                                        {
                                            text17 = "halifax";
                                        }
                                        else if (num == 19)
                                        {
                                            text17 = "bradford";
                                        }
                                        else
                                        {
                                            text17 = "";
                                        }
                                    }
                                    else if (num == 2)
                                    {
                                        text17 = "wigan";
                                    }
                                    else if (num == 10)
                                    {
                                        text17 = "wrexham";
                                    }
                                    else if (num == 1)
                                    {
                                        text17 = "bolton";
                                    }
                                    else if (num == 7)
                                    {
                                        text17 = "st helens";
                                    }
                                    else if (num == 6)
                                    {
                                        text17 = "wakefield";
                                    }
                                    else if (num == 9)
                                    {
                                        text17 = "preston";
                                    }
                                    else if (num == 19)
                                    {
                                        text17 = "bradford";
                                    }
                                    else if (num == 8)
                                    {
                                        text17 = "blackpool";
                                    }
                                    else if (num == 11)
                                    {
                                        text17 = "bury";
                                    }
                                    else if (num == 13)
                                    {
                                        text17 = "darlington";
                                    }
                                    else if (num == 14)
                                    {
                                        text17 = "huddersfield";
                                    }
                                    else
                                    {
                                        text17 = "";
                                    }
                                    if (dataSet.Tables[0].Rows[0]["Forename"] != DBNull.Value)
                                    {
                                        text21 = dataSet.Tables[0].Rows[0]["Forename"].ToString();
                                    }
                                    if (dataSet.Tables[0].Rows[0]["Surname"] != DBNull.Value)
                                    {
                                        text22 = dataSet.Tables[0].Rows[0]["Surname"].ToString();
                                    }
                                    if (dataSet.Tables[0].Rows[0]["UniqueOrderID"] != DBNull.Value)
                                    {
                                        text23 = dataSet.Tables[0].Rows[0]["UniqueOrderID"].ToString();
                                    }
                                    clsSagePay.sendConfirmationOfDepositEmail(text3, 1);
                                    if (base.Request.Url.AbsoluteUri.ToString().Contains("thechinesebuffet.com"))
                                    {
                                        //clsGeneric clsGeneric2 = new clsGeneric();
                                        //CreateTillReservation(text17, text21, text22, dataSet.Tables[0].Rows[0]["Telephone"].ToString(), dataSet.Tables[0].Rows[0]["AdditionalNotes"].ToString(), dataSet.Tables[0].Rows[0]["NumberOfGuests"].ToString(), dReservationDate.ToString("yyyy-MM-dd"), text19.PadLeft(2, '0') + text20.PadLeft(2, '0'), dataSet.Tables[0].Rows[0]["Deposit"].ToString(), dataSet.Tables[0].Rows[0]["NumberOfSeats"].ToString(), dataSet.Tables[0].Rows[0]["NumberOfHighChairs"].ToString(), dataSet.Tables[0].Rows[0]["NumberOfWheelChairs"].ToString(), dataSet.Tables[0].Rows[0]["NumberOfPrams"].ToString(), "EAAA8C5D-F7BD-4243-BD6C-EB6BE4C37E23");
                                        var connectionString = GetLatestConnectionString();
                                        insertEntriesInTillSystem(num, text17, text21, text22, dataSet.Tables[0].Rows[0]["Telephone"].ToString(), dataSet.Tables[0].Rows[0]["AdditionalNotes"].ToString(), Convert.ToInt16(dataSet.Tables[0].Rows[0]["NumberOfGuests"].ToString()), dReservationDate.ToString("yyyy-MM-dd"), text19.PadLeft(2, '0') + text20.PadLeft(2, '0'), Convert.ToInt16(dataSet.Tables[0].Rows[0]["Deposit"].ToString()), Convert.ToInt16(dataSet.Tables[0].Rows[0]["NumberOfSeats"].ToString()), Convert.ToInt16(dataSet.Tables[0].Rows[0]["NumberOfHighChairs"].ToString()), Convert.ToInt16(dataSet.Tables[0].Rows[0]["NumberOfWheelChairs"].ToString()), Convert.ToInt16(dataSet.Tables[0].Rows[0]["NumberOfPrams"].ToString()), connectionString);
                                        if (text17 == "bolton")
                                        {
                                            //sendEmail("gaurav@simplex-services.com", "Bolton Manager " + text22, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + text23, GetReservationEmailBody(text21.Trim() + " " + text22.Trim(), "01204 388222", text23.ToString()), true);
                                            sendEmail("boltononlinedeposit@thechinesebuffet.com", "Bolton Manager " + text22, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code=" + text23, this.GetReservationEmailBody(text21 + " " + text22, "01204 388222", text23), true);
                                        }
                                        else if (text17 == "wigan")
                                        {
                                            sendEmail("wiganonlinedeposit@thechinesebuffet.com", "Wigan Manager " + text22, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code=" + text23, this.GetReservationEmailBody(text21 + " " + text22, "01942 820277", text23), true);
                                        }
                                        else if (text17 == "wrexham")
                                        {
                                            sendEmail("wrexhamonlinedeposit@thechinesebuffet.com", "Wrexham Manager " + text22, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code=" + text23, this.GetReservationEmailBody(text21 + " " + text22, "01978 266898", text23), true);
                                        }
                                        else if (text17 == "wakefield")
                                        {
                                            sendEmail("wakefieldonlinedeposit@thechinesebuffet.com", "Wakefield Manager " + text22, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code=" + text23, this.GetReservationEmailBody(text21 + " " + text22, "01924 339322", text23), true);
                                        }
                                        else if (text17 == "st helens")
                                        {
                                            sendEmail("sthelensonlinedeposit@thechinesebuffet.com", "St Helens Manager " + text22, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code=" + text23, this.GetReservationEmailBody(text21 + " " + text22, "01744 610002", text23), true);
                                        }
                                        else if (text17 == "preston")
                                        {
                                            sendEmail("prestononlinedeposit@thechinesebuffet.com", "Preston Manager " + text22, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code=" + text23, this.GetReservationEmailBody(text21 + " " + text22, "01772 883088", text23), true);
                                        }
                                        else if (text17 == "halifax")
                                        {
                                            sendEmail("halifaxonlinedeposit@thechinesebuffet.com", "Halifax Manager " + text22, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code=" + text23, this.GetReservationEmailBody(text21 + " " + text22, "01422 354001", text23), true);
                                        }
                                        else if (text17 == "bradford")
                                        {
                                            sendEmail("bradfordonlinedeposit@thechinesebuffet.com", "Wrexham Manager " + text22, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code=" + text23, this.GetReservationEmailBody(text21 + " " + text22, "01422 354001", text23), true);
                                        }
                                        else if (text17 == "darlington")
                                        {
                                            sendEmail("bradfordonlinedeposit@thechinesebuffet.com", "Darlington Manager " + text22, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code=" + text23, this.GetReservationEmailBody(text21 + " " + text22, "01422 354001", text23), true);
                                        }
                                    }
                                    //clsGeneric.WriteFile("reservation completed : 304");
                                }
                            }
                        }
                        base.Response.Clear();
                        base.Response.ContentType = "text/plain";
                        if (text2 == "ERROR")
                        {
                            base.Response.Write("Status=INVALID\n");
                        }
                        else
                        {
                            base.Response.Write("Status=OK\n");
                        }
                        if (text2 == "OK" || text2 == "AUTHENTICATED" || text2 == "REGISTERED")
                        {
                            string text24;
                            if (Request.QueryString["o"] == "d")
                            {
                                //text24 = "/restaurants/" + text17.Replace(" ", "") + "/paymentsuccess.html?o=p&VendorTxCode=" + text3;
                                text24 = "/PaymentSuccess.aspx?o=p&VendorTxCode=" + text3;
                            }
                            else
                            {
                                //text24 = "/restaurants/" + text17.Replace(" ", "") + "/paymentsuccess.html?VendorTxCode=" + text3;
                                text24 = "/PaymentSuccess.aspx?o=p&VendorTxCode=" + text3;
                            }
                            if (clsSagePay.ConnectTo == "TEST")
                            {
                                //base.Response.Write("RedirectURL=http://beta.thechinesebuffet.com" + text24 + Environment.NewLine);
                                Response.Write("RedirectURL=http://thechinesebuffet.com"+text24+ Environment.NewLine);
                            }
                            else
                            {
                                //base.Response.Write("RedirectURL=http://www.thechinesebuffet.com" + text24 + Environment.NewLine);
                                Response.Write("RedirectURL=http://thechinesebuffet.com" + text24 + Environment.NewLine);
                            }
                        }
                        else
                        {
                            //string text24 = "/restaurants/" + text17.Replace(" ", "") + "/orderfailed.html?VendorTxCode=" + text3;
                            string text24 = "/orderFailed.aspx?VendorTxCode=" + text3;
                            if (clsSagePay.ConnectTo == "LIVE")
                            {
                                base.Response.Write("RedirectURL=http://thechinesebuffet.com"  + text24 + Environment.NewLine);
                            }
                            else
                            {
                                base.Response.Write("RedirectURL=http://thechinesebuffet.com" + text24 + Environment.NewLine);
                            }
                        }
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
            }
            catch (Exception ex)
            {
                //new clsGeneric();
                //clsGeneric.WriteFile(ex.Message.ToString() + Environment.NewLine + ex.StackTrace);
            }
        }
        public void insertEntriesInTillSystem(int restID, string restaurantname, string sForename, string sSurname, string contactnumber, string reservationnotes, int noofguests, string reservationdate, string reservationtime, int iDeposit, int iseats, int ihigh, int iwheel, int iprams, string connectionString)
        {
            try
            {
                string ipaddress = string.Empty;
                string portnumber = string.Empty;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_PK_GetLocalConnectionDetailsFromRestaurantID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restID;
                        //cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                ipaddress = reader["IpAddress"].ToString();
                                portnumber = reader["PortAddress"].ToString();
                            }

                        }
                        finally
                        {
                            // Always call Close when done reading.
                            reader.Close();
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
                var serverConnString = GetServerConnectionString();
                serverConnString = serverConnString.Replace("{IPADDRESS,PORTADDRESS}", ipaddress);
                SqlParameter sqlParameter = new SqlParameter("@iOut", SqlDbType.Int);
                sqlParameter.Direction = ParameterDirection.Output;
                using (SqlConnection con = new SqlConnection(serverConnString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_PK_AddOnlineReservationToTill ", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@RestaurantName", SqlDbType.VarChar).Value = restaurantname;
                        cmd.Parameters.Add("@Forename", SqlDbType.NVarChar).Value = sForename;
                        cmd.Parameters.Add("@Surname", SqlDbType.NVarChar).Value = sSurname;
                        cmd.Parameters.Add("@ContactNumber", SqlDbType.NVarChar).Value = contactnumber;
                        cmd.Parameters.Add("@ReservationNotes", SqlDbType.NVarChar).Value = reservationnotes;
                        cmd.Parameters.Add("@TotalNoOfGuests", SqlDbType.Int).Value = noofguests;
                        cmd.Parameters.Add("@ReservationDate", SqlDbType.DateTime).Value = reservationdate;
                        cmd.Parameters.Add("@ReservationTime", SqlDbType.Int).Value = reservationtime;
                        cmd.Parameters.Add("@Deposit", SqlDbType.Decimal).Value = iDeposit;
                        cmd.Parameters.Add("@NoOfSeats", SqlDbType.Int).Value = iseats;
                        cmd.Parameters.Add("@NoOfHighChairs", SqlDbType.Int).Value = ihigh;
                        cmd.Parameters.Add("@NoOfWheelChairs", SqlDbType.Int).Value = iwheel;
                        cmd.Parameters.Add("@NoOfPrams", SqlDbType.Int).Value = iprams;

                        try
                        {
                            if (cmd.Connection.State == ConnectionState.Closed)
                            {
                                cmd.Connection.Open();
                            }
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            cmd.Connection.Close();

                        }
                    }
                }
            }
            catch (Exception expr_335)
            {
                Exception ex = expr_335;
            }
        }
        public static string GetServerConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["tcbServerConnectionString"].ConnectionString;
        }
        public void sendEmail(string strToAddress, string strToName, string strFromAddress, string strFromName, string strSubject, string strBody, bool isHTML)
        {
            try
            {
                string gmailid = ConfigurationManager.AppSettings["emailID"].ToString();
                string password = ConfigurationManager.AppSettings["password"].ToString();
                var fromAddress = new MailAddress(gmailid, strFromName);
                var toAddress = new MailAddress(strToAddress, strToName);
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = strSubject,
                    Body = @strBody
                })
                {
                    message.IsBodyHtml = isHTML;
                    smtp.Send(message);
                }




                //MailMessage mailMessage = new MailMessage();
                //MailAddress from = new MailAddress(strFromAddress, strFromName);
                //MailAddress to = new MailAddress(strToAddress, strToName);
                //MailMessage mailMessage2 = new MailMessage(from, to);
                //mailMessage2.Body = strBody;
                //mailMessage2.Subject = strSubject;
                //mailMessage2.IsBodyHtml = isHTML;
                //SmtpClient smtpClient = new SmtpClient();
                //if (MyProject.Computer.Name.StartsWith("YBWKS") | MyProject.Computer.Name.StartsWith("IIS") | HttpContext.Current.Request.Url.ToString().Contains("yellowbus"))
                //{
                //    smtpClient.Host = ConfigurationManager.AppSettings["yellowbusMailServer"];
                //}
                //else
                //{
                //    smtpClient.Host = ConfigurationManager.AppSettings["paoloMailServer"];
                //}
                //smtpClient.Send(mailMessage2);
            }
            catch (Exception expr_C9)
            {
                //ProjectData.SetProjectError(expr_C9);
                //ProjectData.ClearProjectError();
            }
        }
        private string GetReservationEmailBody(string strName, string strTelephone, string strResCode)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<div>Dear " + strName);
            stringBuilder.Append("<br/><br/>Thank you for making a reservation at THE Chinese Buffet.");
            stringBuilder.Append("<br/>Your reservation code is <b>" + strResCode + "</b><br/>");
            stringBuilder.Append("<br/>If you would prefer to amend your booking, you can telephone <b>" + strTelephone + "</b><br/>");
            stringBuilder.Append("<br/><br/>Customer services<br/>");
            stringBuilder.Append("THE Chinese Buffet");
            return stringBuilder.ToString();
        }

        public static string GetLatestConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
        }

        public void completeReservation(int intRestaurantID, string strFullname, string strMobile, string strEmail, DateTime dReservationDate, string strHour, string strMinute, int intGuest, string strNotes, string strPostcode, string connectionString)
        {
            try
            {
                string text = strHour + ":" + strMinute + ":00";
                string value = string.Concat(new string[]
        {
            dReservationDate.Year.ToString(),
            "-",
            dReservationDate.Month.ToString().PadLeft(2, '0'),
            "-",
            dReservationDate.Day.ToString().PadLeft(2, '0'),
            " ",
            text
        });
                string text2 = string.Concat(new string[]
        {
            dReservationDate.Day.ToString().PadLeft(2, '0'),
            "/",
            dReservationDate.Month.ToString().PadLeft(2, '0'),
            "/",
            dReservationDate.Year.ToString(),
            " ",
            text
        });
                strMobile = strMobile.Replace(" ", "");
                if (strMobile.Substring(0, 3).ToString() == "+44")
                {
                    strMobile = strMobile.Substring(3);
                }
                if (strMobile.Substring(0, 1) != "0")
                {
                    strMobile = "0" + strMobile;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM tblPeople WHERE MobileNo=@Mobile", con))
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        if (sqlCommand.Connection.State == ConnectionState.Closed)
                        {
                            sqlCommand.Connection.Open();
                        }
                        sqlCommand.Parameters.AddWithValue("@Mobile", strMobile);
                        if (Convert.ToInt16(sqlCommand.ExecuteScalar()) == 0)
                        {
                            using (SqlCommand sqlCommand2 = new SqlCommand("INSERT INTO tblPeople (MobileNo,Name,RestaurantID,Email) VALUES (@Mobile,@Name,@RestaurantID,@Email)", con))
                            {
                                sqlCommand2.CommandType = CommandType.Text;
                                sqlCommand2.Parameters.AddWithValue("@Mobile", strMobile);
                                sqlCommand2.Parameters.AddWithValue("@Name", strFullname);
                                sqlCommand2.Parameters.AddWithValue("@RestaurantID", intRestaurantID);
                                sqlCommand2.Parameters.AddWithValue("@Email", strEmail);
                                try
                                {
                                    if (sqlCommand2.Connection.State == ConnectionState.Closed)
                                    {
                                        sqlCommand2.Connection.Open();
                                    }
                                    sqlCommand2.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {

                                }
                                finally
                                {
                                    sqlCommand2.Connection.Close();

                                }
                            }
                        }
                    }
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand3 = new SqlCommand("INSERT INTO tblEvent(MobileNo,EventDetail,EventDate,RestaurantID) VALUES(@MobileNo,@EventDetail,@EventDate,@RestaurantID)", con))
                    {
                        sqlCommand3.CommandType = CommandType.Text;
                        sqlCommand3.Parameters.AddWithValue("@MobileNo", strMobile);
                        sqlCommand3.Parameters.AddWithValue("@EventDetail", "Online Reservation");
                        sqlCommand3.Parameters.AddWithValue("@EventDate", value);
                        sqlCommand3.Parameters.AddWithValue("@RestaurantID", intRestaurantID);
                        try
                        {
                            if (sqlCommand3.Connection.State == ConnectionState.Closed)
                            {
                                sqlCommand3.Connection.Open();
                            }
                            sqlCommand3.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            sqlCommand3.Connection.Close();

                        }
                    }
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand4 = new SqlCommand("INSERT INTO tblSendFreeSMS (SendTo,MessageToSend,SMSFrom) VALUES (@SendTo,@MessageToSend,@SMSFrom)", con))
                    {
                        sqlCommand4.CommandType = CommandType.Text;
                        string text3 = "";
                        string value2 = "";
                        switch (intRestaurantID)
                        {
                            case 1:
                                text3 = "Bolton";
                                value2 = "1204388222";
                                break;
                            case 2:
                                text3 = "Wigan";
                                value2 = "1942820277";
                                break;
                        }
                        sqlCommand4.Parameters.AddWithValue("@SendTo", value2);
                        sqlCommand4.Parameters.AddWithValue("@MessageToSend", string.Concat(new string[]
                        {
                            "Reservation made via TCB website (",
                            text3,
                            ") - Name: ",
                            strFullname,
                            " Guests: ",
                            intGuest.ToString(),
                            ", Date:",
                            text2,
                            ", Mob:",
                            strMobile
                        }));
                        sqlCommand4.Parameters.AddWithValue("@SMSFrom", "88");
                        try
                        {
                            if (sqlCommand4.Connection.State == ConnectionState.Closed)
                            {
                                sqlCommand4.Connection.Open();
                            }
                            sqlCommand4.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            sqlCommand4.Connection.Close();

                        }
                    }
                }
            }
            catch (Exception expr_3BB)
            {
                //ProjectData.SetProjectError(expr_3BB);
                //ProjectData.ClearProjectError();
            }
        }
        public bool SetReservationConfirmed(int iReservationID, string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("usp_TCB_NR_SetReservationConfirmed", con))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@iReservationID", iReservationID);
                    try
                    {
                        if (sqlCommand.Connection.State == ConnectionState.Closed)
                        {
                            sqlCommand.Connection.Open();
                        }
                        sqlCommand.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        sqlCommand.Connection.Close();

                    }
                }
            }
            return true;
        }
    }

    #region "SagePayClass"
    public class clsSagePay : IDisposable
    {
        private string mstrYourSiteFQDN;

        private string mstrYourSiteInternalFQDN;

        private string strCurrency;

        private string strTransactionType;

        private string strPartnerID;

        private SqlConnection moConn;

        private string mstrVendorName;

        private string mstrConnectTo;

        private string strProtocol;

        private bool disposedValue;

        public string YourSiteInternalFQDN
        {
            get
            {
                return this.mstrYourSiteInternalFQDN;
            }
        }

        public string YourSiteFQDN
        {
            get
            {
                return this.mstrYourSiteFQDN;
            }
        }

        public string VendorName
        {
            get
            {
                return this.mstrVendorName;
            }
        }

        public string ConnectTo
        {
            get
            {
                return this.mstrConnectTo;
            }
        }

        public string Curreny
        {
            get
            {
                return this.strCurrency;
            }
        }

        public string TransactionType
        {
            get
            {
                return this.strTransactionType;
            }
        }

        public string PartnerID
        {
            get
            {
                return this.strPartnerID;
            }
        }

        public string Protocol
        {
            get
            {
                return this.strProtocol;
            }
        }

        public clsSagePay(string site)
        {
            this.strCurrency = "GBP";
            this.strTransactionType = "PAYMENT";
            this.strPartnerID = "";
            this.strProtocol = "3.00";
            this.disposedValue = false;
            this.openConnection();
            this.loadSagePayParams(site);
        }

        private void loadSagePayParams(string site)
        {
            string str;
            if (site.ToLower().Contains("www"))
            {
                str = "LIVE";
            }
            else
            {
                str = "TEST";
            }

            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tblSagePay WHERE SystemToUse='" + str + "'", this.moConn))
            {

                DataTable dataTable = new DataTable();
                IEnumerator enumerator = dataTable.Rows.GetEnumerator();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                {
                    sqlDataAdapter.Fill(dataTable);
                }
                try
                {
                    enumerator = dataTable.Rows.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        DataRow dataRow = (DataRow)enumerator.Current;
                        this.mstrVendorName = dataRow["VendorName"].ToString();
                        this.mstrConnectTo = dataRow["SystemToUse"].ToString().ToUpper();
                        this.mstrYourSiteFQDN = dataRow["RedirectURL"].ToString();
                        this.mstrYourSiteInternalFQDN = dataRow["RedirectURL"].ToString();
                    }
                }
                finally
                {
                    //IEnumerator enumerator;
                    if (enumerator is IDisposable)
                    {
                        (enumerator as IDisposable).Dispose();
                    }
                }
            }
        }

        public string getRedirectBaseURLbyClientID(string ClientID)
        {
            string result = "";
            using (SqlCommand sqlCommand = new SqlCommand("spGetSagePayRedirectURL", this.moConn))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                result = sqlCommand.ExecuteScalar().ToString();
            }
            return result;
        }

        public string SystemURL(string strConnectTo, string strType)
        {
            string result = "";
            if (strConnectTo.ToString() == "LIVE")
            {
                if (strType.ToString() == "abort")
                {
                    result = "https://live.sagepay.com/gateway/service/abort.vsp";
                }
                else if (strType.ToString() == "authorise")
                {
                    result = "https://live.sagepay.com/gateway/service/authorise.vsp";
                }
                else if (strType.ToString() == "cancel")
                {
                    result = "https://live.sagepay.com/gateway/service/cancel.vsp";
                }
                else if (strType.ToString() == "purchase")
                {
                    result = "https://live.sagepay.com/gateway/service/vspserver-register.vsp";
                }
                else if (strType.ToString() == "refund")
                {
                    result = "https://live.sagepay.com/gateway/service/refund.vsp";
                }
                else if (strType.ToString() == "release")
                {
                    result = "https://live.sagepay.com/gateway/service/release.vsp";
                }
                else if (strType.ToString() == "repeat")
                {
                    result = "https://live.sagepay.com/gateway/service/repeat.vsp";
                }
                else if (strType.ToString() == "void")
                {
                    result = "https://live.sagepay.com/gateway/service/void.vsp";
                }
                else if (strType.ToString() == "3dcallback")
                {
                    result = "https://live.sagepay.com/gateway/service/direct3dcallback.vsp";
                }
                else if (strType.ToString() == "showpost")
                {
                    result = "https://test.sagepay.com/showpost/showpost.asp";
                }
            }
            else if (strConnectTo.ToString() == "TEST")
            {
                if (strType.ToString() == "abort")
                {
                    result = "https://test.sagepay.com/gateway/service/abort.vsp";
                }
                else if (strType.ToString() == "authorise")
                {
                    result = "https://test.sagepay.com/gateway/service/authorise.vsp";
                }
                else if (strType.ToString() == "cancel")
                {
                    result = "https://test.sagepay.com/gateway/service/cancel.vsp";
                }
                else if (strType.ToString() == "purchase")
                {
                    result = "https://test.sagepay.com/gateway/service/vspserver-register.vsp";
                }
                else if (strType.ToString() == "refund")
                {
                    result = "https://test.sagepay.com/gateway/service/refund.vsp";
                }
                else if (strType.ToString() == "release")
                {
                    result = "https://test.sagepay.com/gateway/service/release.vsp";
                }
                else if (strType == "repeat")
                {
                    result = "https://test.sagepay.com/gateway/service/repeat.vsp";
                }
                else if (strType.ToString() == "void")
                {
                    result = "https://test.sagepay.com/gateway/service/void.vsp";
                }
                else if (strType.ToString() == "3dcallback")
                {
                    result = "https://test.sagepay.com/gateway/service/direct3dcallback.vsp";
                }
                else if (strType.ToString() == "showpost")
                {
                    result = "https://test.sagepay.com/showpost/showpost.asp";
                }
            }
            else if (strType.ToString() == "abort")
            {
                result = "https://test.sagepay.com/simulator/vspserverGateway.asp?Service=VendorAbortTx";
            }
            else if (strType.ToString() == "authorise")
            {
                result = "https://test.sagepay.com/simulator/vspserverGateway.asp?Service=VendorAuthoriseTx";
            }
            else if (strType.ToString() == "cancel")
            {
                result = "https://test.sagepay.com/simulator/vspserverGateway.asp?Service=VendorCancelTx";
            }
            else if (strType.ToString() == "purchase")
            {
                result = "https://test.sagepay.com/simulator/VSPServerGateway.asp?Service=VendorRegisterTx";
            }
            else if (strType.ToString() == "refund")
            {
                result = "https://test.sagepay.com/simulator/vspserverGateway.asp?Service=VendorRefundTx";
            }
            else if (strType.ToString() == "release")
            {
                result = "https://test.sagepay.com/simulator/vspserverGateway.asp?Service=VendorReleaseTx";
            }
            else if (strType.ToString() == "repeat")
            {
                result = "https://test.sagepay.com/simulator/vspserverGateway.asp?Service=VendorRepeatTx";
            }
            else if (strType.ToString() == "void")
            {
                result = "https://test.sagepay.com/simulator/vspserverGateway.asp?Service=VendorVoidTx";
            }
            else if (strType.ToString() == "3dcallback")
            {
                result = "https://test.sagepay.com/simulator/vspserverCallback.asp";
            }
            else if (strType == "showpost")
            {
                result = "https://test.sagepay.com/showpost/showpost.asp";
            }
            return result;
        }

        private void openConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            this.moConn = new SqlConnection(connectionString);
            try
            {
                this.moConn.Open();
            }
            catch (Exception expr_2E)
            {
                //ProjectData.SetProjectError(expr_2E);
                Exception ex = expr_2E;
                throw ex;
            }
        }

        private void DestroyConnection(SqlConnection objConn)
        {
            try
            {
                if (objConn != null && objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            catch (Exception expr_14)
            {
                //ProjectData.SetProjectError(expr_14);
                Exception ex = expr_14;
                throw ex;
            }
        }

        public string cleanInput(string strRawText, string strType)
        {
            string text = "";
            int i = 0;
            string @string;
            bool flag;
            if (strType == "Number")
            {
                @string = "0123456789.";
                flag = false;
            }
            else if (strType == "VendorTxCode")
            {
                @string = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_.";
                flag = false;
            }
            else
            {
                @string = " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.,'/{}@():?-_&£$=%~<>*+\"\r\n";
                flag = true;
            }
            checked
            {
                while (i <= strRawText.Length)
                {
                    string text2 = strRawText.Substring( i, 1);
                    byte[] asciiBytes = Encoding.ASCII.GetBytes(text2);
                    if (text2.IndexOf(@string) != 0)//  Strings.InStr(@string, text2, CompareMethod.Binary) != 0)
                    {
                        text += text2;
                    }
                    else if (flag && asciiBytes[0] >= 191)
                    {
                        text += text2;
                    }
                    i++;
                }
                return text.ToString();
            }
        }

        public string SQLSafe(string strRawText)
        {
            string text = "";
            checked
            {
                for (int i = 0; i <= strRawText.Length-1; i++)
                {
                    if (strRawText.Substring(i, 1) == "'")
                    {
                        text += "''";
                        if (i != strRawText.Length)
                        {
                            if (strRawText.Substring(i+1, 1) == "'")
                            {
                                i++;
                            }
                        }
                    }
                    else
                    {
                        text += strRawText.Substring(i, 1);
                    }
                }
                return text.ToString();
            }
        }

        //public int countColons(string strSource)
        //{
        //    checked
        //    {
        //        int num;
        //        if (Operators.CompareString(strSource, "", false) == 0)
        //        {
        //            num = 0;
        //        }
        //        else
        //        {
        //            int num2 = 1;
        //            num = 0;
        //            while (num2 != 0)
        //            {
        //                num2 = Strings.InStr(num2 + 1, strSource, ":", CompareMethod.Binary);
        //                if (num2 != 0)
        //                {
        //                    num++;
        //                }
        //            }
        //        }
        //        return num;
        //    }
        //}

        //public bool validateBasket(string strThisBasket)
        //{
        //    bool result = false;
        //    if (Strings.Len(strThisBasket) > 0 & Strings.InStr(strThisBasket, ":", CompareMethod.Binary) != 0)
        //    {
        //        string text = Strings.Left(strThisBasket, checked(Strings.InStr(strThisBasket, ":", CompareMethod.Binary) - 1));
        //        if (Versioned.IsNumeric(text))
        //        {
        //            text = Conversions.ToString(Conversions.ToInteger(text));
        //            if ((double)this.countColons(strThisBasket) == Conversions.ToDouble(text) * 5.0 + Conversions.ToDouble(text))
        //            {
        //                result = true;
        //            }
        //        }
        //    }
        //    return result;
        //}

        //public string URLDecode(string strString)
        //{
        //    string text = "";
        //    int arg_0E_0 = 1;
        //    int num = Strings.Len(strString);
        //    checked
        //    {
        //        for (int i = arg_0E_0; i <= num; i++)
        //        {
        //            if (Operators.CompareString(Strings.Mid(strString, i, 1), "%", false) == 0)
        //            {
        //                text += Conversions.ToString(Strings.Chr(Conversions.ToInteger("&H" + Strings.Mid(strString, i + 1, 2))));
        //                i += 2;
        //            }
        //            else if (Operators.CompareString(Strings.Mid(strString, i, 1), "+", false) == 0)
        //            {
        //                text += " ";
        //            }
        //            else
        //            {
        //                text += Strings.Mid(strString, i, 1);
        //            }
        //        }
        //        return text;
        //    }
        //}

        public string URLEncode(string strString)
        {
            return HttpUtility.UrlEncode(strString, Encoding.GetEncoding("ISO-8859-15"));
        }

        //public object mySQLDate(DateTime dateASP)
        //{
        //    return string.Concat(new string[]
        //{
        //    Conversions.ToString(DateAndTime.DatePart("yyyy", dateASP, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1)),
        //    "-",
        //    Strings.Right("00" + Conversions.ToString(DateAndTime.DatePart("m", dateASP, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1)), 2),
        //    "-",
        //    Strings.Right("00" + Conversions.ToString(DateAndTime.DatePart("d", dateASP, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1)), 2),
        //    " ",
        //    Strings.Right("00" + Conversions.ToString(DateAndTime.DatePart("h", dateASP, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1)), 2),
        //    ":",
        //    Strings.Right("00" + Conversions.ToString(DateAndTime.DatePart("n", dateASP, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1)), 2),
        //    ":",
        //    Strings.Right("00" + Conversions.ToString(DateAndTime.DatePart("s", dateASP, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1)), 2)
        //});
        //}

        //public string findField(string strFieldName, string strThisResponse)
        //{
        //    string[] array = new string[2];
        //    string result = "";
        //    array = Strings.Split(strThisResponse, "\r\n", -1, CompareMethod.Binary);
        //    int arg_2B_0 = Information.LBound(array, 1);
        //    int num = Information.UBound(array, 1);
        //    checked
        //    {
        //        for (int i = arg_2B_0; i <= num; i++)
        //        {
        //            if (Strings.InStr(array[i], strFieldName + "=", CompareMethod.Binary) == 1)
        //            {
        //                result = Strings.Mid(array[i], Strings.Len(strFieldName) + 2);
        //                break;
        //            }
        //        }
        //        return result;
        //    }
        //}

        //public string nullToStr(object dbRecord)
        //{
        //    if (dbRecord == DBNull.Value)
        //    {
        //        return "";
        //    }
        //    return dbRecord.ToString();
        //}

        //public string createSagePayBasket(int iOrderID)
        //{
        //    clsOrders clsOrders = new clsOrders();
        //    string text = "";
        //    int num = 0;
        //    try
        //    {
        //        IEnumerator enumerator = clsOrders.GetTakeawayOrderItems(iOrderID).Tables[0].Rows.GetEnumerator();
        //        while (enumerator.MoveNext())
        //        {
        //            DataRow dataRow = (DataRow)enumerator.Current;
        //            checked
        //            {
        //                num++;
        //                text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(text + ":", dataRow["Description"]), ":"), Conversions.ToString(dataRow["iQty"])));
        //            }
        //            if (DateAndTime.Now.Year < 2010)
        //            {
        //                text = text + ":" + Strings.FormatNumber((double)Conversions.ToSingle(dataRow["fPrice"]) / 1.15, 2, TriState.True, TriState.False, TriState.False);
        //                text = text + ":" + Strings.FormatNumber(Conversions.ToSingle(dataRow["fPrice"]) * 3f / 23f, 2, TriState.True, TriState.False, TriState.False);
        //            }
        //            else
        //            {
        //                text = text + ":" + Strings.FormatNumber((double)Conversions.ToSingle(dataRow["fPrice"]) / 1.175, 2, TriState.True, TriState.False, TriState.False);
        //                text = text + ":" + Strings.FormatNumber(Conversions.ToSingle(dataRow["fPrice"]) * 7f / 47f, 2, TriState.True, TriState.False, TriState.False);
        //            }
        //            text = text + ":" + Strings.FormatNumber(Conversions.ToSingle(dataRow["fPrice"]), 2, TriState.True, TriState.False, TriState.False);
        //            text = text + ":" + Strings.FormatNumber(Conversions.ToSingle(dataRow["fPrice"]) * (float)Conversions.ToInteger(dataRow["iQty"]), 2, TriState.True, TriState.False, TriState.False);
        //        }
        //    }
        //    finally
        //    {
        //        IEnumerator enumerator;
        //        if (enumerator is IDisposable)
        //        {
        //            (enumerator as IDisposable).Dispose();
        //        }
        //    }
        //    float num2 = Conversions.ToSingle("0");
        //    text = string.Concat(new string[]
        //{
        //    Conversions.ToString(checked(num + 1)),
        //    text,
        //    ":Delivery:1:",
        //    Strings.FormatNumber(num2, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault),
        //    ":---:",
        //    Strings.FormatNumber(num2, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault),
        //    ":",
        //    Strings.FormatNumber(num2, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
        //});
        //    return text;
        //}

        public void sendConfirmationEmail(string vendorTXCode, int clientID)
        {
            string gmailid = ConfigurationManager.AppSettings["emailID"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();
            var fromAddress = new MailAddress(gmailid, "The Chinese Buffet Team");
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, password)
            };
            //if (MyProject.Computer.Name.StartsWith("YBWKS") | MyProject.Computer.Name.StartsWith("IIS") | HttpContext.Current.Request.Url.ToString().Contains("yellowbus"))
            //{
            //    smtpClient.Host = ConfigurationManager.AppSettings["yellowbusMailServer"];
            //}
            //else
            //{
            //    smtpClient.Host = ConfigurationManager.AppSettings["paoloMailServer"];
            //}
            MailMessage mailMessage = new MailMessage();
            //clsGeneric clsGeneric = new clsGeneric();
            string text = "";
            string text2 = "";
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            int value = 1;
            float num = 0f;
            float num2 = 0f;
            string right = "";
            string right2 = "";
            string text3 = "";
            text3 = "development@yellowbus.co.uk";
            int iTakeawayID;
            using (SqlCommand sqlCommand = new SqlCommand("spGetCustomerDetailsBySagePayVenderTXCode", this.moConn))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@SagePay_VendorTXCode", vendorTXCode);
                DataTable dataTable = new DataTable();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                {
                    sqlDataAdapter.Fill(dataTable);
                }
                text = dataTable.Rows[0]["vCustomerEmail"].ToString();
                text2 = dataTable.Rows[0]["vCustomerName"].ToString();
                str2 = dataTable.Rows[0]["vHouseNumber"].ToString();
                str3 = dataTable.Rows[0]["vFullAddress"].ToString();
                str7 = dataTable.Rows[0]["vPostCode"].ToString();
                str8 = dataTable.Rows[0]["vCustomerPhone"].ToString();
                iTakeawayID = Convert.ToInt16(dataTable.Rows[0]["iTakeAwayId"]);
                if (dataTable.Rows[0]["fOrderTotal"] != DBNull.Value)
                {
                    //num = Conversions.ToSingle(dataTable.Rows[0]["fOrderTotal"]);
                }
                if (dataTable.Rows[0]["fOrderTotal"] != DBNull.Value)
                {
                    //float num3 = Conversions.ToSingle(dataTable.Rows[0]["fOrderTotal"]);
                }
            }
            string address = "sales@thechinesebuffet.com";
            string text4 = "THE Chinese Buffet";
            mailMessage.From = new MailAddress(address, text4);
            mailMessage.Subject = "Payment Confirmation";
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            stringBuilder.Append("<table><tr><td><img src='http://www.thechinesebuffet.com/images/logo.jpg' /><br /><br />");
            stringBuilder.Append("Dear " + text2 + "<br/><br/>");
            stringBuilder.Append("Thank you for your order at " + text4 + " <br/><br/>");
            stringBuilder.Append("Your order reference is :" + vendorTXCode + ". Please quote this in any correspondence with us.");
            stringBuilder2.Append(text2 + " " + str + " has placed an order. <br/><br/>");
            stringBuilder2.Append("Their order reference is : " + vendorTXCode);
            clsOrders clsOrders = new clsOrders();
            stringBuilder3.Append("<br/><br/><table width='500'style='border:solid 1px black; font-family:Verdana;' >");
            stringBuilder3.Append("<tr style='background-color:#CAF6D8;'><th width='300'>Product</th><th width='100'>Quantity</th><th width='100'>Price</th></tr>");
            IEnumerator enumerator = clsOrders.GetTakeawayOrderItems(iTakeawayID).Tables[0].Rows.GetEnumerator();
            try
            {
                
                while (enumerator.MoveNext())
                {
                    DataRow dataRow = (DataRow)enumerator.Current;
                    stringBuilder3.Append("<tr ><td style='border:solid 1px black;  text-align:center;'>"+ dataRow["Description"]+ right2+ right+ " </td><td style='border:solid 1px black;  text-align:center;'>"+ dataRow["iQty"]+ "</td><td style='border:solid 1px black;  text-align:center;'>"+ Convert.ToDouble(dataRow["fTotalPrice"]).ToString("£0.00")+ "</td></tr>");
                }
            }
            finally
            {
                //IEnumerator enumerator;
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
            stringBuilder3.Append("<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>");
            stringBuilder3.Append("<tr><td>&nbsp;</td><td style='background-color:#CAF6D8;  text-align:left;'>Sub Total<td align='right'>" + num.ToString("£0.00") + "</td></tr>");
            stringBuilder3.Append("<tr><td>&nbsp;</td><td style='background-color:#CAF6D8;  text-align:left;'>Delivery Cost<td align='right'>" + num2.ToString("£0.00") + "</td></tr>");
            stringBuilder3.Append("<tr><td>&nbsp;</td><td style='background-color:#CAF6D8;  text-align:left;'>Grand Total<td align='right'>" + (num + num2).ToString("£0.00") + "</td></tr>");
            stringBuilder3.Append("</table>");
            stringBuilder3.Append("<br /><br />");
            stringBuilder3.Append("<table width='500' style='border:solid 1px black; font-family:Verdana;'>");
            stringBuilder3.Append("<tr style='background-color:#CAF6D8;'>");
            stringBuilder3.Append("<td colspan='2'>Address <i>(As provided on the website)</i></td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>House No :</td>");
            stringBuilder3.Append("<td align='left'>" + str2 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Address Line 1 :</td>");
            stringBuilder3.Append("<td align='left'>" + str3 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Address Line 2 :</td>");
            stringBuilder3.Append("<td align='left'>" + str4 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Area :</td>");
            stringBuilder3.Append("<td align='left'>" + str5 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Town / City :</td>");
            stringBuilder3.Append("<td align='left'>" + str6 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Post Code :</td>");
            stringBuilder3.Append("<td align='left'>" + str7 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Email :</td>");
            stringBuilder3.Append("<td align='left'>" + text + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Tel No :</td>");
            stringBuilder3.Append("<td align='left'>" + str8 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("</table>");
            stringBuilder3.Append("<br /><br /><table style='font-size:9px;color:#999999;'><tr><td>STUDENT DISCOUNTS<br />");
            stringBuilder3.Append("Students receive 10% discount off their bill on Mondays and Tuesdays. You must produce your student id with photo. This promotion may be cancelled at any time without prior notice. All promotion offers applies to our general terms and conditions<br /><br />");
            stringBuilder3.Append("ST HELENS £5 CODE<br />");
            stringBuilder3.Append("Codes can be redeemed at our St Helensbranch 12 hours after receiving the code. MUST BE A MINIMUM of 2 dinning. Text messages are charged at your networks standard rate. Only one code per mobilenumber per week. Limited codes are available. Also applies to Our General promotional offers terms and conditions below.<br /><br />");
            stringBuilder3.Append("For more Terms and Conditions, <a href='http://www.thechinesebuffet.com/restaurants/bolton/terms_and_conditions.html'>click here</a>.<br />");
            stringBuilder3.Append("<br /><br /><img src='http://www.thechinesebuffet.com/images/logo.jpg' /></td></tr></table>");
            mailMessage.IsBodyHtml = true;
            try
            {
                mailMessage.To.Add(text);
                mailMessage.Body = stringBuilder.ToString() + stringBuilder3.ToString();
                smtpClient.Send(mailMessage);
            }
            catch (Exception expr_7C5)
            {
                //ProjectData.SetProjectError(expr_7C5);
                Exception eX = expr_7C5;
                //clsGeneric.SendYBError(eX, clientID, "", "clsSagePay", "sendConfirmationEmail:sendCustomerEmail");
                //ProjectData.ClearProjectError();
            }
            mailMessage.To.Clear();
            try
            {
                if (text3 == "")
                {
                    mailMessage.To.Add("development@yellowbus.co.uk");
                    mailMessage.Subject = "Client's admin email address is not set, client id = " + value.ToString();
                }
                else
                {
                    mailMessage.To.Add(text3);
                    mailMessage.Bcc.Add("development@yellowbus.co.uk");
                }
                mailMessage.Body = stringBuilder2.ToString() + stringBuilder3.ToString();
                smtpClient.Send(mailMessage);
            }
            catch (Exception expr_86F)
            {
                //ProjectData.SetProjectError(expr_86F);
                Exception eX2 = expr_86F;
                //clsGeneric.SendYBError(eX2, clientID, "", "clsSagePay", "sendConfirmationEmail:SendAdminEmail");
                //ProjectData.ClearProjectError();
            }
            mailMessage.Dispose();
            smtpClient = null;
        }

        public void sendConfirmationOfDepositEmail(string vendorTXCode, int clientID)
        {
            string gmailid = ConfigurationManager.AppSettings["emailID"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();
            var fromAddress = new MailAddress(gmailid, "The Chinese Buffet Team");
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, password)
            };
            //SmtpClient smtpClient = new SmtpClient();
            //if (MyProject.Computer.Name.StartsWith("YBWKS") | MyProject.Computer.Name.StartsWith("IIS") | HttpContext.Current.Request.Url.ToString().Contains("yellowbus"))
            //{
            //    smtpClient.Host = ConfigurationManager.AppSettings["yellowbusMailServer"];
            //}
            //else
            //{
            //    smtpClient.Host = ConfigurationManager.AppSettings["paoloMailServer"];
            //}
            //smtpClient.Host = "smtp.gmail.com";
            MailMessage mailMessage = new MailMessage();
            //clsGeneric clsGeneric = new clsGeneric();
            //functions functions = new functions();
            string text = "";
            string text2 = "";
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            int value = 1;
            float num = 0f;
            string text3 = "";
            string text4 = "";
            string str9 = "";
            string str10 = "";
            string str11 = "";
            string str12 = "";
            text3 = "development@yellowbus.co.uk";
            using (SqlCommand sqlCommand = new SqlCommand("spGetCustomerDetailsBySagePayVenderTXCodeV2", this.moConn))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@SagePay_VendorTXCode", vendorTXCode);
                DataTable dataTable = new DataTable();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                {
                    sqlDataAdapter.Fill(dataTable);
                }
                text = dataTable.Rows[0]["vCustomerEmail"].ToString();
                if (dataTable.Rows[0]["UniqueOrderID"] != DBNull.Value)
                {
                    text4 = dataTable.Rows[0]["UniqueOrderID"].ToString();
                }
                SendSMS(dataTable.Rows[0]["vCustomerPhone"].ToString().TrimStart(new char[]
            {
                '0'
            }), "Thank you for your reservation at TCB, your unique reference code is " + text4);
                text2 = dataTable.Rows[0]["vCustomerName"].ToString();
                str2 = dataTable.Rows[0]["vHouseNumber"].ToString();
                str3 = dataTable.Rows[0]["vFullAddress"].ToString();
                str12 = dataTable.Rows[0]["AdditionalNotes"].ToString();
                str7 = dataTable.Rows[0]["vPostCode"].ToString();
                str8 = dataTable.Rows[0]["vCustomerPhone"].ToString();
                str9 = dataTable.Rows[0]["DateTimeReservation"].ToString();
                str10 = dataTable.Rows[0]["NumberOfGuests"].ToString();
                str11 = dataTable.Rows[0]["Restaurant"].ToString();
                int num2 = Convert.ToInt16(dataTable.Rows[0]["iTakeAwayId"]);
                if (dataTable.Rows[0]["fOrderTotal"] != DBNull.Value)
                {
                    num = Convert.ToSingle(dataTable.Rows[0]["fOrderTotal"]);
                }
                if (dataTable.Rows[0]["fOrderTotal"] != DBNull.Value)
                {
                    float num3 = Convert.ToSingle(dataTable.Rows[0]["fOrderTotal"]);
                }
            }
            string address = "sales@thechinesebuffet.com";
            string text5 = "THE Chinese Buffet";
            mailMessage.From = new MailAddress(address, text5);
            mailMessage.Subject = "Payment Confirmation " + text4;
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            stringBuilder.Append("Dear " + text2 + "<br/><br/>");
            stringBuilder.Append("Thank you for your deposit at " + text5 + " <br/><br/>");
            var str21 = string.Empty;
            if (text4 != "")
                str21 = text4;
            else
                str21 = vendorTXCode;
            stringBuilder.Append("Your deposit / order reference is :"+ str21 + ". Please quote this in any correspondence with us. <br/>");
            stringBuilder.Append("Reservation Date/Time :" + str9 + "<br/>");
            stringBuilder.Append("No of Guests :" + str10 + "<br/>");
            stringBuilder.Append("Restaurant :" + str11 + "<br/>");
            stringBuilder2.Append(text2 + " " + str + " has made an online reservation and placed a deposit. <br/><br/>");
            stringBuilder2.Append(string.Concat(new string[]
        {
            "Their UniqueOrderID is <b>",
            text4,
            "</b> and Their deposit / order reference is : ",
            vendorTXCode,
            "<br/>"
        }));
            stringBuilder2.Append("Reservation Date/Time :" + str9 + "<br/>");
            stringBuilder2.Append("No of Guests :" + str10 + "<br/>");
            stringBuilder2.Append("Restaurant :" + str11 + "<br/>");
            clsOrders clsOrders = new clsOrders();
            stringBuilder3.Append("<br/><br/><table width='500'style='border:solid 1px black; font-family:Verdana;' >");
            stringBuilder3.Append("<tr><td style='background-color:#CAF6D8;  text-align:left;'>Deposit: <td align='right'>" + num.ToString("£0.00") + "</td></tr>");
            stringBuilder3.Append("</table>");
            stringBuilder3.Append("<br /><br />");
            stringBuilder3.Append("<table width='500' style='border:solid 1px black; font-family:Verdana;'>");
            stringBuilder3.Append("<tr style='background-color:#CAF6D8;'>");
            stringBuilder3.Append("<td colspan='2'>Address <i>(As provided on the website)</i></td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>House No :</td>");
            stringBuilder3.Append("<td align='left'>" + str2 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Address Line 1 :</td>");
            stringBuilder3.Append("<td align='left'>" + str3 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Address Line 2 :</td>");
            stringBuilder3.Append("<td align='left'>" + str4 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Area :</td>");
            stringBuilder3.Append("<td align='left'>" + str5 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Town / City :</td>");
            stringBuilder3.Append("<td align='left'>" + str6 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Post Code :</td>");
            stringBuilder3.Append("<td align='left'>" + str7 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Email :</td>");
            stringBuilder3.Append("<td align='left'>" + text + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Tel No :</td>");
            stringBuilder3.Append("<td align='left'>" + str8 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("<tr>");
            stringBuilder3.Append("<td align='left' width='30%'>Notes :</td>");
            stringBuilder3.Append("<td align='left'>" + str12 + "</td>");
            stringBuilder3.Append("</tr>");
            stringBuilder3.Append("</table>");
            mailMessage.IsBodyHtml = true;
            try
            {
                mailMessage.To.Add(text);
                mailMessage.Body = stringBuilder.ToString() + stringBuilder3.ToString();
                smtpClient.Send(mailMessage);
            }
            catch (Exception expr_87B)
            {
                //ProjectData.SetProjectError(expr_87B);
                Exception eX = expr_87B;
                //clsGeneric.SendYBError(eX, clientID, "", "clsSagePay", "sendConfirmationEmail:sendCustomerEmail");
                //ProjectData.ClearProjectError();
            }
            mailMessage.To.Clear();
            try
            {
                if (text3 ==  "")
                {
                    mailMessage.To.Add("development@yellowbus.co.uk");
                    mailMessage.Subject = "Client's admin email address is not set, client id = " + value.ToString();
                }
                else
                {
                    mailMessage.To.Add(text3);
                    mailMessage.Bcc.Add("development@yellowbus.co.uk");
                }
                mailMessage.Body = stringBuilder2.ToString() + stringBuilder3.ToString();
                smtpClient.Send(mailMessage);
            }
            catch (Exception expr_92C)
            {
                //ProjectData.SetProjectError(expr_92C);
                Exception eX2 = expr_92C;
                //clsGeneric.SendYBError(eX2, clientID, "", "clsSagePay", "sendConfirmationEmail:SendAdminEmail");
                //ProjectData.ClearProjectError();
            }
            mailMessage.Dispose();
            smtpClient = null;
        }
        public void SendSMS(string strSendTo, string strMessageToSend)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO tblSendFreeSMS (SendTo, MessageToSend,SMSFrom) VALUES(@SendTo, @MessageToSend,'ChineseBuffet')", this.moConn))
            {
                sqlCommand.Parameters.AddWithValue("@SendTo", strSendTo);
                sqlCommand.Parameters.AddWithValue("@MessageToSend", strMessageToSend);
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception expr_3E)
                {
                    //ProjectData.SetProjectError(expr_3E);
                    Exception ex = expr_3E;
                    //functions.logError("Error storing new SMS ", ex.Message, ex.StackTrace.ToString());
                    //ProjectData.ClearProjectError();
                }
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                this.DestroyConnection(this.moConn);
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class clsPayments
    {
        public int InsertPayment(int iOrderID, string vTransactionID, DateTime dtPaymentDate, float fPrice, bool bIsSuccess, string vFaultReason, int iPaymentType, string SagePay_SecurityKey = "", string SagePay_VendorTXCode = "")
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            int result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "usp_TCB_NS_PaymentInsert";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iTakeawayOrderID", SqlDbType.Int).Value = iOrderID;
                sqlCommand.Parameters.Add("@vTransactionID", SqlDbType.VarChar).Value = vTransactionID;
                sqlCommand.Parameters.Add("@dtPaymentDate", SqlDbType.DateTime).Value = dtPaymentDate;
                sqlCommand.Parameters.Add("@fPaymentPrice", SqlDbType.Float).Value = fPrice;
                sqlCommand.Parameters.Add("@bIsSuccess", SqlDbType.Bit).Value = bIsSuccess;
                sqlCommand.Parameters.Add("@vFaultReason", SqlDbType.VarChar).Value = vFaultReason;
                sqlCommand.Parameters.Add("@iPaymentType", SqlDbType.Int).Value = iPaymentType;
                sqlCommand.Parameters.Add("@securityKey", SqlDbType.NVarChar).Value = SagePay_SecurityKey;
                sqlCommand.Parameters.Add("@vendorTXCode", SqlDbType.NVarChar).Value = SagePay_VendorTXCode;
                SqlParameter sqlParameter = new SqlParameter("@iOutput", SqlDbType.Int);
                sqlParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.ExecuteNonQuery();
                result = Convert.ToInt16(sqlParameter.Value);
            }
            catch (Exception expr_171)
            {
                Exception ex = expr_171;
                throw new Exception("Error occured while inserting payment" + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet GetSinglePayment(int iPaymentID)
        {
            DataSet dataSet = new DataSet();
            SqlConnection sqlConnection = new SqlConnection();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            DataSet result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "spGetSinglePayment";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iPaymentID", SqlDbType.Int).Value = iPaymentID;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                result = dataSet;
            }
            catch (Exception expr_89)
            {
                Exception ex = expr_89;
                throw new Exception("Error occured while loading payment " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public bool IsDuplicateID(string vTransactionID)
        {
            DataSet dataSet = new DataSet();
            SqlConnection sqlConnection = new SqlConnection();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            bool result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "spIsPaymentDuplicated";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@vTransactionID", SqlDbType.VarChar).Value = vTransactionID;
                SqlParameter sqlParameter = new SqlParameter("@bOut", SqlDbType.Bit);
                sqlParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.ExecuteScalar();
                result = Convert.ToBoolean(sqlParameter.Value);
            }
            catch (Exception expr_AC)
            {
                Exception ex = expr_AC;
                throw new Exception("Error occured while checking duplication " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public string getSagePaySecurityKeyForComparison(string VendorTXCode, string TransactionID)
        {
            string result = "";
            using (SqlConnection sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("spGetSagePaySecurityCode", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@SagePay_TransactionID", TransactionID);
                    sqlCommand.Parameters.AddWithValue("@SagePay_VendorTXCode", VendorTXCode);
                    result = sqlCommand.ExecuteScalar().ToString();
                }
            }
            return result;
        }

        public void UpdatePaymentStatus(bool bStatus, int iOrderID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "spPaymentStatusUpdate";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iOrderID", SqlDbType.Int).Value = iOrderID;
                sqlCommand.Parameters.Add("@bStatus", SqlDbType.Bit).Value = bStatus;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception expr_87)
            {
                Exception ex = expr_87;
                throw new Exception("Error occured while updating payment : " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public void UpdatePaymentStatus(string SagePay_TransactionID, string SagePay_VendorTXCode, bool bSuccess, string FailureReason = "")
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString; 
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "spPaymentStatusUpdateAlt";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@SagePay_TransactionID", SqlDbType.VarChar).Value = SagePay_TransactionID;
                sqlCommand.Parameters.Add("@SagePay_VendorTXCode", SqlDbType.VarChar).Value = SagePay_VendorTXCode;
                sqlCommand.Parameters.Add("@Success", SqlDbType.Bit).Value = bSuccess;
                sqlCommand.Parameters.Add("@FailureReason", SqlDbType.VarChar).Value = FailureReason;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception expr_B4)
            {
                Exception ex = expr_B4;
                throw new Exception("Error occured while updating payment : " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public string getSagePayPaymentStatusMessage(string VendorTXCode)
        {
            string result = "";
            using (SqlConnection sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("spGetSagePayPaymentStatusMessage", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@SagePay_VendorTXCode", VendorTXCode);
                    result = sqlCommand.ExecuteScalar().ToString();
                }
            }
            return result;
        }
    }

    public class clsOrders
    {
        public int InsertOrder(int iResturantID, string vCustomerName, string vHouseNumber, string vFullAddress, string PostCode, int collectionTime, string vCustomerPhone, string vCustomerEmail, double fOrderTotal, bool bReservation = false)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            int result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "usp_TCB_NS_TakeawayOrderInsertv2";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iResturantID", SqlDbType.Int).Value = iResturantID;
                sqlCommand.Parameters.Add("@vCustomerName", SqlDbType.VarChar).Value = vCustomerName;
                sqlCommand.Parameters.Add("@vHouseNumber", SqlDbType.VarChar).Value = vHouseNumber;
                sqlCommand.Parameters.Add("@vFullAddress", SqlDbType.VarChar).Value = vFullAddress;
                sqlCommand.Parameters.Add("@vPostCode", SqlDbType.VarChar).Value = PostCode;
                sqlCommand.Parameters.Add("@collectionTime", SqlDbType.Int).Value = collectionTime;
                sqlCommand.Parameters.Add("@vCustomerPhone", SqlDbType.VarChar).Value = vCustomerPhone;
                sqlCommand.Parameters.Add("@vCustomerEmail", SqlDbType.VarChar).Value = vCustomerEmail;
                sqlCommand.Parameters.Add("@fOrderTotal", SqlDbType.Float).Value = fOrderTotal;
                sqlCommand.Parameters.Add("@bReservation", SqlDbType.Bit).Value = bReservation;
                SqlParameter sqlParameter = new SqlParameter("@iOutput", SqlDbType.Int);
                sqlParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.ExecuteNonQuery();
                result = Convert.ToInt16(sqlParameter.Value);
            }
            catch (Exception expr_186)
            {
                //ProjectData.SetProjectError(expr_186);
                Exception ex = expr_186;
                throw new Exception("Error occured while inserting order" + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public int InsertOrderItem(int iResturantID, int iTakeawayOrderID, int iProductID, int iQty, float fPrice, float fTotalPrice)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            int result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "usp_TCB_NS_TakeawyOrderItemInsert";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iTakeawayOrderID", SqlDbType.Int).Value = iTakeawayOrderID;
                sqlCommand.Parameters.Add("@iProductID", SqlDbType.Int).Value = iProductID;
                sqlCommand.Parameters.Add("@iQty", SqlDbType.Int).Value = iQty;
                sqlCommand.Parameters.Add("@fPrice", SqlDbType.Float).Value = fPrice;
                sqlCommand.Parameters.Add("@fTotalPrice", SqlDbType.Float).Value = fTotalPrice;
                SqlParameter sqlParameter = new SqlParameter("@iOutput", SqlDbType.Int);
                sqlParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.ExecuteNonQuery();
                result = Convert.ToInt16(sqlParameter.Value);
            }
            catch (Exception expr_10E)
            {
                
                Exception ex = expr_10E;
                throw new Exception("Error occured while inserting order items" + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet GetAllNotDownloadedOrders(int iClientID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            DataSet result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "spGetAllNotDownloadedOrders";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iClientID", SqlDbType.Int).Value = iClientID;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                result = dataSet;
            }
            catch (Exception expr_89)
            {
                Exception ex = expr_89;
                throw new Exception("Error occured while updating order " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet loadOrdersRegister(int CurrentPage, int pageSize, string whrString, string sortColumn, string sortDirection, int iClientID)
        {
            DataSet dataSet = new DataSet();
            SqlConnection sqlConnection = new SqlConnection();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            DataSet result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "spLoadOrdersRegister";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@CurrentPage", SqlDbType.Int).Value = CurrentPage;
                sqlCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                sqlCommand.Parameters.Add("@whrString", SqlDbType.VarChar).Value = whrString;
                sqlCommand.Parameters.Add("@sortColumn", SqlDbType.VarChar).Value = sortColumn;
                sqlCommand.Parameters.Add("@sortDirection", SqlDbType.VarChar).Value = sortDirection;
                sqlCommand.Parameters.Add("@iClientID", SqlDbType.Int).Value = iClientID;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                result = dataSet;
            }
            catch (Exception expr_111)
            {
                Exception ex = expr_111;
                throw new Exception("Error occured while loading orders " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet GetAllProductsInOrder(int iOrderID)
        {
            DataSet dataSet = new DataSet();
            SqlConnection sqlConnection = new SqlConnection();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            DataSet result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "spGetAllProductsByOrderID";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iOrderID", SqlDbType.Int).Value = iOrderID;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                result = dataSet;
            }
            catch (Exception expr_89)
            {
                Exception ex = expr_89;
                throw new Exception("Error occured while loading products in a order " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet GetTakeawayOrderItems(int iTakeawayID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            DataSet result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "spGetOrderItemsByOrderID";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iOrderID", SqlDbType.Int).Value = iTakeawayID;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                result = dataSet;
            }
            catch (Exception expr_89)
            {
                Exception ex = expr_89;
                throw new Exception("Error occured while downloadig orders " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet GetOrderbyOrderID(int iOrderID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            DataSet result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "usp_tcb_NS_GetOrderByID";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iOrderID", SqlDbType.Int).Value = iOrderID;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                result = dataSet;
            }
            catch (Exception expr_89)
            {
                Exception ex = expr_89;
                throw new Exception("Error occured while loading takeaway order " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public float GetOrderTotalbyOrderID(int iOrderID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            float result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "usp_tcb_NS_GetOrderTotalByID";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iOrderID", SqlDbType.Int).Value = iOrderID;
                result = Convert.ToSingle(sqlCommand.ExecuteScalar());
            }
            catch (Exception expr_70)
            {
                Exception ex = expr_70;
                throw new Exception("Error occured while loading takeaway order total" + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet GetPendingOrders(string sFilter)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            DataSet result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "usp_TCB_NR_GetPendingOrders";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@sFilter", SqlDbType.VarChar).Value = sFilter;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                result = dataSet;
            }
            catch (Exception expr_85)
            {
                Exception ex = expr_85;
                throw new Exception("Error occured while loading pending orders " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet GetRequestedSuppliers(int iID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            DataSet result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "usp_TCB_NR_GetSuppliersRequested";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@poNumber", SqlDbType.Int).Value = iID;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                result = dataSet;
            }
            catch (Exception expr_89)
            {
                Exception ex = expr_89;
                throw new Exception("Error occured while loading pending orders " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public bool InsertRequestedSupplier(int iPO, int iSuppleirID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            bool result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "usp_TCB_NR_InsertRequestedSupplier";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iPO", SqlDbType.Int).Value = iPO;
                sqlCommand.Parameters.Add("@iSuppleirID", SqlDbType.Int).Value = iSuppleirID;
                sqlCommand.ExecuteScalar();
                result = true;
            }
            catch (SqlException expr_9E)
            {
                SqlException ex = expr_9E;
                throw new Exception("Error occured while inserting requested supplier " + ex.ToString());
            }
            catch (Exception expr_BD)
            {
                Exception ex2 = expr_BD;
                throw new Exception("Error occured while inserting requested supplier " + ex2.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet GetSupplierPendingOrders(int SupplierId)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            string value = "Pending";
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            DataSet result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "usp_TCB_MF_GetSupplierPendingOrders";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@sFilter", SqlDbType.VarChar).Value = value;
                sqlCommand.Parameters.AddWithValue("@SupplierId", SupplierId);
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                result = dataSet;
            }
            catch (Exception expr_A5)
            {
                Exception ex = expr_A5;
                throw new Exception("Error occured while loading pending orders " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet GetSingleOrder(int iID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            DataSet result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "usp_TCB_NR_GetSingleOrder";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iID", SqlDbType.Int).Value = iID;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                result = dataSet;
            }
            catch (Exception expr_89)
            {
                Exception ex = expr_89;
                throw new Exception("Error occured while loading single order " + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet GetSingleOrderSupplier(int iID, int iSupplierId)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            DataSet result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "usp_TCB_MF_GetSingleOrderSupplier";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iID", SqlDbType.Int).Value = iID;
                sqlCommand.Parameters.Add("@SupplierID", SqlDbType.Int).Value = iSupplierId;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                result = dataSet;
            }
            catch (Exception expr_A6)
            {
                Exception ex = expr_A6;
                throw new Exception("Error occured while loading single order supplier" + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        //public bool HasSupplierPrice(int ProductId, int SupplierId, ref string sPrice)
        //{
        //    try
        //    {
        //        DataTable dataTable = new DataTable();
        //        string cmdText = string.Concat(new string[]
        //        {
        //            "SELECT top 1 ProductPrice  FROM tblSupplierProductPrice WHERE productFK = ",
        //            ProductId.ToString(),
        //            " AND SupplierFK = ",
        //            SupplierId.ToString(),
        //            " AND DelInd = 0  order by DateCreated desc"
        //        });
        //        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString))
        //        {
        //            using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
        //            {
        //                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
        //                {
        //                    sqlDataAdapter.Fill(dataTable);
        //                }
        //            }
        //        }
        //        try
        //        {
        //            IEnumerator enumerator = dataTable.Rows.GetEnumerator();
        //            while (enumerator.MoveNext())
        //            {
        //                DataRow dataRow = (DataRow)enumerator.Current;
        //                sPrice = dataRow["ProductPrice"].ToString();
        //            }
        //        }
        //        finally
        //        {
        //            IEnumerator enumerator;
        //            if (enumerator is IDisposable)
        //            {
        //                (enumerator as IDisposable).Dispose();
        //            }
        //        }
        //    }
        //    catch (SqlException expr_FE)
        //    {
        //        SqlException ex = expr_FE;
        //        throw new Exception("Error occured while loading single order supplier" + ex.ToString());
        //    }
        //    catch (Exception expr_11D)
        //    {
        //        Exception ex2 = expr_11D;
        //        throw new Exception("Error occured while loading single order supplier" + ex2.ToString());
        //    }
        //    bool result;
        //    return result;
        //}

        public long IsReservationOrder(string strTransactionID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            long result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "spIsReservationOrderCheckByTransactionID";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@vTransactionID", SqlDbType.VarChar).Value = strTransactionID;
                result = Convert.ToInt64(sqlCommand.ExecuteScalar());
            }
            catch (Exception expr_6C)
            {
                Exception ex = expr_6C;
                throw new Exception("Error occured while getting the ordertype" + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        public DataSet getReservationByReservationID(int iReservationID)
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand sqlCommand2 = new SqlCommand("spGetResevationBYReservationID", sqlConnection);
            try
            {
                sqlCommand2.CommandType = CommandType.StoredProcedure;
                sqlCommand2.Parameters.AddWithValue("@iReservationID", iReservationID);
                sqlDataAdapter.SelectCommand = sqlCommand2;
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception expr_7B)
            {
                //ProjectData.SetProjectError(expr_7B);
                //ProjectData.ClearProjectError();
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return dataSet;
        }

        public void UpdateReservationByOrder(int iOrderID, int iReservationID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "updateReservationOrderID";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iReservationID", SqlDbType.Int).Value = iReservationID;
                sqlCommand.Parameters.Add("@iOrderID", SqlDbType.Int).Value = iOrderID;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception expr_87)
            {
                Exception ex = expr_87;
                throw new Exception("Error occured while updating reservation" + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public void UpdateReservationNotes(int iReservationID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "spUpdateReservationNotes";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@iReservationID", SqlDbType.Int).Value = iReservationID;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception expr_6B)
            {
                //ProjectData.SetProjectError(expr_6B);
                Exception ex = expr_6B;
                //throw new Exception("Error occured while updating reservation" + ex.ToString());
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
    #endregion
}