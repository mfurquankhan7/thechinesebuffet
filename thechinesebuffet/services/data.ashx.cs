using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
//using tcb.services;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;

//using tcb.entities.Entities;

namespace tcb.web.services
{
    /// <summary>
    /// Summary description for data
    /// </summary>
    public class data : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            var method = context.Request["method"];
            //service program = new service();
            //Type thisType = program.GetType();
            //MethodInfo theMethod = thisType.GetMethod(method);
            //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            ////{"Could not load type 'tcb.entities.tcb_latestContext' from assembly 'tcb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.":"tcb.entities.tcb_latestContext"}
            //var retval = javaScriptSerializer.Serialize(theMethod.Invoke(program, new object[] { context }));
            ////Console.WriteLine(retval);
            //context.Response.Write(retval);

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string outputToReturn = string.Empty;
            try
            {
                switch (context.Request["method"])
                {
                    case "getAddress":
                        outputToReturn = getAddress(context);
                        break;
                    case "bookTable":
                        outputToReturn = bookTable(context);
                        break;
                    case "getAllRestaurants":
                        outputToReturn = getAllRestaurants(context);
                        break;
                    case "getMenus":
                        outputToReturn = getMenus(context);
                        break;
                    case "BookingConfirm":
                        outputToReturn = BookingConfirm(context);
                        break;
                    case "getItems":
                        outputToReturn = getItems(context);
                        break;
                    case "getItemMenus":
                        outputToReturn = getItemMenus(context);
                        break;
                    case "getGallery":
                        outputToReturn = getGallery(context);
                        break;
                    case "getSpecialOffers":
                        outputToReturn = getSpecialOffers(context);
                        break;
                    case "getAllNews":
                        outputToReturn = getAllNews(context);
                        break;
                    case "contactUs":
                        outputToReturn = contactUs(context);
                        break;
                    case "loadjobvacancies":
                        outputToReturn = loadjobvacancies(context);
                        break;
                    case "registernewuser":
                        outputToReturn = registernewuser(context);
                        break;
                    case "login":
                        outputToReturn = login(context);
                        break;
                    case "applyjob":
                        outputToReturn = applyjob(context);
                        break;
                    case "couldnotbook":
                        outputToReturn = couldnotbook(context);
                        break;
                    case "paydeposit":
                        outputToReturn = paydeposit(context);
                        break;
                    case "returnFromSagePay":
                        outputToReturn = returnFromSagePay(context);
                        break;


                }
            }
            catch (Exception ex)
            {
                outputToReturn = "{\"status\":\"0\",\"Message\": \"" + ex.Message + "\"}";
                context.Response.Write(javaScriptSerializer.Serialize(outputToReturn));
            }

            var retval = javaScriptSerializer.Serialize(outputToReturn);
            context.Response.Write(retval);
        }
        public string returnFromSagePay(HttpContext context)
        {
            string retVal = string.Empty;

            retVal = "{\"status\":\"1\", \"Message\": \"Successfully Called this Handler\"}";
            return retVal;
        }
        public string paydeposit(HttpContext context)
        {
            string retVal = string.Empty;
            try
            {
                StringBuilder sb = new StringBuilder();
                Dictionary<string, string> data = jsondata();
                int RestaurantFK = Convert.ToInt16(data["BookingRestaurantID"].ToString());
                string BookingRestaurantName = data["BookingRestaurantName"].ToString();
                string Fullname = data["BookingFirstName"].ToString() + " " + data["BookingSurName"].ToString();
                string Mobile = data["BookingMobile"].ToString();
                string Email = data["BookingEmail"].ToString();
                DateTime DateTimeReservation = DateTime.Parse(data["BookingDate"].ToString());
                int NumberOfGuests = Convert.ToInt16(data["BookingNoOfGuests"].ToString());
                string BookingTimeSlot = data["BookingTimeSlot"].ToString();
                string hour = BookingTimeSlot.Substring(0, 2);
                string minute = BookingTimeSlot.Substring(2, 2);
                string AdditionalNotes = data["BookingNotes"].ToString();
                string vPostcode = data["BookingPostalCode"].ToString();
                string sForename = data["BookingFirstName"].ToString();
                string sSurname = data["BookingSurName"].ToString();
                string sipAddress = data["BookingIpAddress"].ToString();
                int iDeposit = Convert.ToInt16(data["BookingDeposit"].ToString());
                int iseats = Convert.ToInt16(data["BookingNoOfGuests"].ToString());
                int ihigh = Convert.ToInt16(data["BookingNoOfHighChairs"].ToString());
                int iwheel = Convert.ToInt16(data["BookingNoOfWheelChairs"].ToString());
                int iprams = Convert.ToInt16(data["BookingNoOfPramsSeat"].ToString());
                string BookingAddressLine1 = data["BookingAddressLine1"].ToString();
                string BookingAddressLine2 = data["BookingAddressLine2"].ToString();
                string BookingCity = data["BookingCity"].ToString();
                string vCode = data["UniqueOrderID"].ToString();
                Random generator = new Random();
                string r = generator.Next(100000, 999999).ToString();
                vCode = r;
                string restContactNumber = string.Empty;
                string restName = string.Empty;
                string content = string.Empty;
                int output = 0;
                string connectionString = GetLatestConnectionString();
                string fulladdress = BookingAddressLine1 + " " + BookingAddressLine2 + " " + BookingCity;
                int num2 = Convert.ToInt32(iseats) - Convert.ToInt32(ihigh) - Convert.ToInt32(iprams);
                double num3 = 5.0;

                int num4 = InsertOrder(RestaurantFK, Fullname, "", fulladdress, vPostcode, Convert.ToInt16(BookingTimeSlot), Mobile, Email, Convert.ToInt64(iDeposit), true);
                if (num4 > 0)
                {
                    var ReservationOrderID = num4;
                    var IsReservationOrder = true;
                    var num = Convert.ToInt32((double)num2 * num3);
                    InsertOrderItem(RestaurantFK, num4, 0, num2, 5f, Convert.ToSingle(num));
                }
                

                int reservationID = makeReservation(RestaurantFK, Fullname, Mobile, Email, DateTimeReservation, hour, minute, NumberOfGuests, AdditionalNotes, vPostcode, sForename, sSurname, sipAddress, iDeposit, iseats, ihigh, iwheel, iprams, vCode, connectionString);
                UpdateReservationByOrder(num4, reservationID);
                //completeReservation(RestaurantFK, Fullname, Mobile, Email, DateTimeReservation, hour, minute, NumberOfGuests, AdditionalNotes, vPostcode, connectionString);
                //SetReservationConfirmed(reservationID, connectionString);
                Guid gid = Guid.NewGuid();
                retVal = processSagePay(iDeposit, BookingRestaurantName, NumberOfGuests, sSurname, sForename, BookingAddressLine1, BookingAddressLine2, BookingCity, vPostcode, Mobile, Email, "1-"+gid.ToString(),num4);
            }
            catch (Exception ex)
            {
                retVal = "{\"status\":\"0\", \"Message\": \""+ex.Message+"\"}";
            }
            return retVal;
        }
        public string processSagePay(int iDeposit, string BookingRestaurantName, int NumberOfGuests, string sSurname, string sForename, string BookingAddressLine1, string BookingAddressLine2, string BookingCity, string vPostcode, string Mobile,string Email,string vendorTxCode, int iOrderID)
        {
            string retVal = string.Empty;
            string sagePayEnv = ConfigurationManager.AppSettings["sagePayEnv"].ToString();
            string URL = "https://test.sagepay.com/gateway/service/vspserver-register.vsp";
            if(sagePayEnv == "live")
                URL = "https://live.sagepay.com/gateway/service/vspserver-register.vsp";
            string VPSProtocol = "3.00";
            string TxType = "PAYMENT";
            string Vendor = "thechinesebuffe";
            string VendorTxCode = vendorTxCode;
            string Amount = iDeposit.ToString(); ;
            string Currency = "GBP";
            string Description = "Booking at " + BookingRestaurantName + " for " + NumberOfGuests + " number of people(s)";
            string NotificationURL = "http://thechinesebuffet.com/Notification.aspx/";
            string Token = "";//The Token provided during the token registration phase
            string BillingSurname = sSurname;
            string BillingFirstnames = sForename;
            string BillingAddress1 = BookingAddressLine1;
            string BillingAddress2 = BookingAddressLine2;
            string BillingCity = BookingCity;
            string BillingPostCode = vPostcode;
            string BillingCountry = "GB";
            string BillingState = "";
            string BillingPhone = Mobile;
            string DeliverySurname = sSurname;
            string DeliveryFirstnames = sForename;
            string DeliveryAddress1 = BookingAddressLine1;
            string DeliveryAddress2 = BookingAddressLine2;
            string DeliveryCity = BookingCity;
            string DeliveryPostCode = vPostcode;
            string DeliveryCountry = "GB";
            string DeliveryState = "";
            string DeliveryPhone = Mobile;
            string CustomerEMail = Email;
            string Basket = "";
            int ApplyAVSCV2 = 0;
            int Apply3DSecure = 0;
            string CustomerXML = "";//This can be used to supply information on the customer for purposes such as fraud screening.
            string SurchargeXML = "";//Use this field to override current surcharge settings in “My Sage Pay” for the current transaction. Percentage and fixed amount surcharges can be set for different payment types.
            string VendorData = "TheChineseBuffetLtd";
            string ReferrerID = "";
            string Language = "EN";
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("VPSProtocol", VPSProtocol);
            nvc.Add("TxType", TxType);
            nvc.Add("Vendor", Vendor);
            nvc.Add("VendorTxCode", VendorTxCode);
            nvc.Add("Amount", Amount);
            nvc.Add("Currency", Currency);
            nvc.Add("Description", Description);
            nvc.Add("NotificationURL", NotificationURL);
            nvc.Add("BillingSurname", BillingSurname);
            nvc.Add("BillingFirstnames", BillingFirstnames);
            nvc.Add("BillingAddress1", BillingAddress1);
            nvc.Add("BillingAddress2", BillingAddress2);
            nvc.Add("BillingCity", BillingCity);
            nvc.Add("BillingPostCode", BillingPostCode);
            nvc.Add("BillingCountry", BillingCountry);
            nvc.Add("BillingPhone", BillingPhone);
            nvc.Add("DeliverySurname", DeliverySurname);
            nvc.Add("DeliveryFirstnames", DeliveryFirstnames);
            nvc.Add("DeliveryAddress1", DeliveryAddress1);
            nvc.Add("DeliveryAddress2", DeliveryAddress2);
            nvc.Add("DeliveryCity", DeliveryCity);
            nvc.Add("DeliveryPostCode", DeliveryPostCode);
            nvc.Add("DeliveryCountry", DeliveryCountry);
            nvc.Add("DeliveryPhone", DeliveryPhone);
            nvc.Add("CustomerEMail", CustomerEMail);

            nvc.Add("VendorData", VendorData);
            nvc.Add("ApplyAVSCV2", ApplyAVSCV2.ToString());
            nvc.Add("Apply3DSecure", Apply3DSecure.ToString());
            nvc.Add("Language", Language);
            string query = ToQueryString(nvc);
            var request = (HttpWebRequest)WebRequest.Create(URL);

            var postData = query;
            var _data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = _data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(_data, 0, _data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //var cls = new clsSagePay(HttpContext.Current.Request.Url.AbsoluteUri);
            //string str = "[VPSProtocol=3.00  Status=OK  StatusDetail=2014 : The Transaction was Registered Successfully.  VPSTxId={7A77A0E0-9453-EDD8-3043-C3E5776465EF}  SecurityKey=GPW3GJVIZQ  NextURL=https://testcheckout.sagepay.com/gateway/service/cardselection?vpstxid={7A77A0E0-9453-EDD8-3043-C3E5776465EF}]";
            var keyValuePairs = responseString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(x => x.Split('='))
                .Where(x => x.Length == 2 || x.Length == 2)
                .ToDictionary(x => x.First(), x => x.Last());
            var nexturl = responseString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[5].ToString().Replace("NextURL=", "");
            if (keyValuePairs["Status"] == "OK")
            {
                //"{\"status\":\"1\",\"Message\": \"You have successfully applied for the job\"}";
                retVal = "{\"status\":\"1\",\"statusstr\": \"" + keyValuePairs["Status"] + "\",\"statusdetail\":\"" + keyValuePairs["StatusDetail"] + "\",\"vpstxid\":\"" + keyValuePairs["VPSTxId"] + "\",\"securitykey\":\"" + keyValuePairs["SecurityKey"] + "\",\"nexturl\":\"" + nexturl + "\"}";
                InsertPayment(iOrderID, keyValuePairs["VPSTxId"], DateTime.UtcNow, Convert.ToInt64(Amount), true, "", 4, keyValuePairs["SecurityKey"], vendorTxCode);
            }
            else if (keyValuePairs["Status"] == "INVALID")
            {
                retVal = "{\"status\":\"0\",\"statusstr\": \"" + keyValuePairs["Status"] + "\",\"statusdetail\":\"" + keyValuePairs["StatusDetail"] + "\",\"vpstxid\":\"" + keyValuePairs["VPSTxId"] + "\"}";
                InsertPayment(iOrderID, keyValuePairs["VPSTxId"], DateTime.UtcNow, Convert.ToInt64(Amount), true, "", 4, "", vendorTxCode);
            }
            else if (keyValuePairs["Status"] == "OK REPEATED")
            {
                retVal = "{\"status\":\"1\",\"statusstr\": \"" + keyValuePairs["Status"] + "\",\"statusdetail\":\"" + keyValuePairs["StatusDetail"] + "\",\"vpstxid\":\"" + keyValuePairs["VPSTxId"] + "\",\"securitykey\":\"" + keyValuePairs["SecurityKey"] + "\",\"nexturl\":\"" + nexturl + "\"}";
                InsertPayment(iOrderID, keyValuePairs["VPSTxId"], DateTime.UtcNow, Convert.ToInt64(Amount), true, "", 4, keyValuePairs["SecurityKey"], vendorTxCode);
            }
            else if (keyValuePairs["Status"] == "MALFORMED")
            {
                retVal = "{\"status\":\"0\",\"statusstr\": \"" + keyValuePairs["Status"] + "\",\"statusdetail\":\"" + keyValuePairs["StatusDetail"] + "\",\"vpstxid\":\"" + keyValuePairs["VPSTxId"] + "\"}";
                InsertPayment(iOrderID, keyValuePairs["VPSTxId"], DateTime.UtcNow, Convert.ToInt64(Amount), true, "", 4, "", vendorTxCode);
            }
            else if (keyValuePairs["Status"] == "ERROR")
            {
                retVal = "{\"status\":\"0\",\"statusstr\": \"" + keyValuePairs["Status"] + "\",\"statusdetail\":\"" + keyValuePairs["StatusDetail"] + "\",\"vpstxid\":\"" + keyValuePairs["VPSTxId"] + "\"}";
                InsertPayment(iOrderID, keyValuePairs["VPSTxId"], DateTime.UtcNow, Convert.ToInt64(Amount), true, "", 4, "", vendorTxCode);
            }
            return retVal;
        }
        private string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }
        public string couldnotbook(HttpContext context)
        {
            string retVal = string.Empty;
            Dictionary<string, string> data = jsondata();
            string restID = data["RestID"].ToString();
            string mobileNumber = data["mobilenumber"].ToString();
            string name = data["name"].ToString();
            string BookingRestaurantName = data["restName"].ToString();
            var subject = name + " Could not book a table";
            string gmailid = ConfigurationManager.AppSettings["emailID"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<table><tr><td><img src='http://www.thechinesebuffet.com/images/logo.jpg' /><br /><br />");
            stringBuilder.Append("<br /><br />" + name + " was trying to reserve a table at " + BookingRestaurantName + ", THE Chinese Buffet.");
            stringBuilder.Append("<br /><br />Following are the details<br/>");
            stringBuilder.Append("<br />Name : " + name + "<br />");
            stringBuilder.Append("<br />Mobile Number : " + mobileNumber + "<br />");
            stringBuilder.Append("<br />Restaurant Name : " + BookingRestaurantName + "<br />");
            stringBuilder.Append("<br /><br /></td></tr></table>");
            var body = stringBuilder.ToString();

            if (BookingRestaurantName.ToLower() == "bolton")
            {
                sendMail(gmailid, password, "boltononlinedeposit@thechinesebuffet.com", subject, body);
                //sendEmail("boltononlinedeposit@thechinesebuffet.com", "Bolton Manager", "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01204 388222", reservationID.ToString()), true);
            }
            else if (BookingRestaurantName.ToLower() == "wigan")
            {
                //sendEmail("wiganonlinedeposit@thechinesebuffet.com", "Wigan Manager" , "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01942 820277", reservationID.ToString()), true);
                sendMail(gmailid, password, "wiganonlinedeposit@thechinesebuffet.com", subject, body);
                //sendMail(gmailid, password, "mohit@simplex-services.com", subject, body);
            }
            else if (BookingRestaurantName.ToLower() == "wrexham")
            {
                //sendEmail("wrexhamonlinedeposit@thechinesebuffet.com", "Wrexham Manager ", "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01978 266898", reservationID.ToString()), true);
                sendMail(gmailid, password, "wrexhamonlinedeposit@thechinesebuffet.com", subject, body);
                //sendMail(gmailid, password, "mohit@simplex-services.com", subject, body);
            }
            else if (BookingRestaurantName.ToLower() == "bradford")
            {
                //sendEmail("bradfordonlinedeposit@thechinesebuffet.com", "Bradford Manager ", "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01274 733777", reservationID.ToString()), true);
                sendMail(gmailid, password, "bradfordonlinedeposit@thechinesebuffet.com", subject, body);
                //sendMail(gmailid, password, "mohit@simplex-services.com", subject, body);
            }
            else if (BookingRestaurantName.ToLower() == "wakefield")
            {
                //sendEmail("wakefieldonlinedeposit@thechinesebuffet.com", "Wakefield Manager ", "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01924 339322", reservationID.ToString()), true);
                sendMail(gmailid, password, "wakefieldonlinedeposit@thechinesebuffet.com", subject, body);
                //sendMail(gmailid, password, "mohit@simplex-services.com", subject, body);
            }
            else if (BookingRestaurantName.ToLower() == "st helens")
            {
                //sendEmail("sthelensonlinedeposit@thechinesebuffet.com", "ST Helens Manager " , "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01744 610002", reservationID.ToString()), true);
                sendMail(gmailid, password, "sthelensonlinedeposit@thechinesebuffet.com", subject, body);
                //sendMail(gmailid, password, "mohit@simplex-services.com", subject, body);
            }
            else if (BookingRestaurantName.ToLower() == "preston")
            {
                //sendEmail("prestononlinedeposit@thechinesebuffet.com", "Preston Manager " , "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01772 883088", reservationID.ToString()), true);
                sendMail(gmailid, password, "prestononlinedeposit@thechinesebuffet.com", subject, body);
                //sendMail(gmailid, password, "mohit@simplex-services.com", subject, body);
            }
            else if (BookingRestaurantName.ToLower() == "halifax")
            {
                //sendEmail("halifaxonlinedeposit@thechinesebuffet.com", "Halifax Manager " , "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01422 354001", reservationID.ToString()), true);
                sendMail(gmailid, password, "halifaxonlinedeposit@thechinesebuffet.com", subject, body);
                //sendMail(gmailid, password, "mohit@simplex-services.com", subject, body);
            }
            else if (BookingRestaurantName.ToLower() == "darlington")
            {
                //sendEmail("darlingtononlinedeposit@thechinesebuffet.com", "Darlington Manager ", "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01422 354001", reservationID.ToString()), true);
                sendMail(gmailid, password, "darlingtononlinedeposit@thechinesebuffet.com", subject, body);
                //sendMail(gmailid, password, "mohit@simplex-services.com", subject, body);
            }
            return retVal;
        }
        public string applyjob(HttpContext context)
        {
            string retVal = string.Empty;
            Dictionary<string, string> data = jsondata();
            string email = data["email"].ToString();
            string personaldetails = data["personaldetails"].ToString();
            string careerhistory = data["careerhistory"].ToString();
            string experience = data["experience"].ToString();
            string achievements = data["achievements"].ToString();
            string personalprofile = data["personalprofile"].ToString();
            string education = data["education"].ToString();
            string reference = data["reference"].ToString();
            string jobtitle = data["jobtitle"].ToString();
            string location = data["location"].ToString();
            string gmailid = ConfigurationManager.AppSettings["emailID"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();
            string toAddress1 = ConfigurationManager.AppSettings["toAddress"].ToString();
            string subject = "Job Application";
            string bodyForTCB = "<table><tr><td><b>Job Title :</b><td></tr><tr><td>";
            bodyForTCB += jobtitle + "</td></tr><tr><td><b>Restaurant :</b><td></tr><tr><td>";
            bodyForTCB += location + "</td></tr><tr><td><b>Personal Details :</b><td></tr><tr><td>";
            bodyForTCB += personaldetails + "</td></tr><tr><td><b>Career History :</b></td></tr><tr><td>";
            bodyForTCB += careerhistory + "</td></tr><tr><td><b>Experience :</b></td></tr><tr><td>";
            bodyForTCB += experience + "</td></tr><tr><td><b>Achievements :</b></td></tr><tr><td>";
            bodyForTCB += achievements + "</td></tr><tr><td><b>Personal Profile</b></td></tr><tr><td>";
            bodyForTCB += personalprofile + "</td></tr><tr><td><b>Education :</b></td></tr><tr><td>";
            bodyForTCB += education + "</td></tr><tr><td><b>References :</b></td></tr><tr><td>";
            bodyForTCB += reference + "</td></tr></table>";
            string bodyForApplicant = "Thank you for expressing your desire to work for THE Chinese Buffet. We normally get lots of applications from the applicants and we normally get in touch only with successful candidates. We keep the applicants details in the system for future opportunities.";
            try
            {
                sendMail(gmailid, password, toAddress1, subject, bodyForTCB);
                sendMail(gmailid, password, "gaurav@simplex-services.com", subject, bodyForTCB);
                sendMail(gmailid, password, "mfurquankhan7@gmail.com", subject, bodyForTCB);
                sendMail(gmailid, password, "shweta.baxi@digiqom.com", subject, bodyForTCB);
                sendMail(gmailid, password, "mohit@simplex-services.com", subject, bodyForTCB);
                sendMail(gmailid, password, email, subject, bodyForApplicant);

            }
            catch (Exception ex) { return "{\"status\":\"0\",\"Message\": \"" + ex.Message + "\"}"; }
            return "{\"status\":\"1\",\"Message\": \"You have successfully applied for the job\"}";
        }
        public string login(HttpContext context)
        {
            string retVal = string.Empty;
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> data = jsondata();
            var username = data["uname"].ToString();
            var password = data["password"].ToString();
            var queryString = "select * from tblWebUser where cUsername = '" + username + "' and cPassword ='" + password + "'";
            var connectionString = GetLatestConnectionString();
            var cnt = 0;
            var name = string.Empty;
            bool found = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        name = reader["cName"].ToString();
                        found = true;
                    }
                    if (found)
                        retVal = "{\"status\":\"1\", \"name\": \"" + name + "\"}";
                    else
                        retVal = "{\"status\":\"0\", \"message\": \"No user found\"}";
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }

            return retVal;
        }
        public string registernewuser(HttpContext context)
        {
            string retVal = string.Empty;
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> data = jsondata();
            var name = data["name"].ToString();
            var username = data["username"].ToString();
            var email = data["email"].ToString();
            var mobile = data["mobile"].ToString();
            var address = data["address"].ToString();
            var password = data["password"].ToString();
            var connectionString = GetLatestConnectionString();
            string stmt = "INSERT INTO dbo.tblWebUser(cName,cUsername,cPassword,cProfileType,cProfilePic) VALUES(@cName, @cUsername,@cPassword,@cProfileType,@cProfilePic)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(stmt, connection);
                connection.Open();
                cmd.Parameters.Add("@cName", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@cUsername", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@cPassword", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@cProfileType", SqlDbType.Int);
                cmd.Parameters.Add("@cProfilePic", SqlDbType.VarChar, 255);

                cmd.Parameters["@cName"].Value = name;
                cmd.Parameters["@cUsername"].Value = username;
                cmd.Parameters["@cPassword"].Value = password;
                cmd.Parameters["@cProfileType"].Value = 1;
                cmd.Parameters["@cProfilePic"].Value = username + ".png";
                cmd.ExecuteNonQuery();
            }
            return "{\"status\":\"1\", \"message\": \"Success\"}"; ;
        }

        public string loadjobvacancies(HttpContext context)
        {
            try
            {
                string retVal = string.Empty;
                Dictionary<string, string> data = jsondata();
                string connectionString = GetLatestConnectionString();
                StringBuilder sb = new StringBuilder();
                string template = "\"title\":\"{0}\", \"desc\":\"{1}\", \"location\":\"{2}\", \"RestaurandID\":\"{3}\"";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_TCB_CP_loadJobVacancies", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        int cnt = 0;
                        try
                        {
                            while (reader.Read())
                            {
                                string title = reader["jobtitle"].ToString();
                                string desc = reader["jobdescription"].ToString().Replace("\"", "\'");
                                string location = reader["location"].ToString();
                                int restID = Convert.ToInt16(reader["RestaurantID"].ToString());
                                if (cnt > 0) sb.Append(",");
                                sb.Append("{" + string.Format(template, title, desc, location, restID) + "}");
                                cnt++;
                            }
                            retVal = "{\"status\":\"1\", \"records\": [" + sb.ToString() + "]}";


                        }
                        finally
                        {
                            // Always call Close when done reading.
                            reader.Close();
                        }
                        cmd.ExecuteNonQuery();
                    }
                }


                return retVal;
            }
            catch (Exception ex)
            {
                return "{\"status\":\"0\", \"message\": \"" + ex.Message + "\"}";
            }

        }

        public string contactUs(HttpContext context)
        {
            string retVal = string.Empty;
            Dictionary<string, string> data = jsondata();
            string name = data["name"].ToString();
            string email = data["email"].ToString();
            string mobile = data["mobile"].ToString();
            string notes = data["notes"].ToString();
            string gmailid = ConfigurationManager.AppSettings["emailID"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();
            string toAddress1 = ConfigurationManager.AppSettings["toAddress"].ToString();
            string subject = "Contact Request from " + name;
            string body = "Name : " + name + "\n" + "Email : " + email + "\n" + "Contact number : " + mobile + "\n" + notes;
            try
            {
                sendMail(gmailid, password, toAddress1, subject, body);
            }
            catch (Exception ex) { return "{\"status\":\"0\",\"Message\": \"" + ex.Message + "\"}"; }
            return "{\"status\":\"1\",\"Message\": \"Success\"}";
        }
        public void sendMail(string gmailid, string password, string toAddress1, string subject, string body)
        {
            var fromAddress = new MailAddress(gmailid, "The Chinese Buffet Contact Us Team");
            var toAddress = new MailAddress(toAddress1, "Deb");
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
                Subject = subject,
                Body = @body
            })
            {
                message.IsBodyHtml = true;
                smtp.Send(message);
            }
        }

        public string getGallery(HttpContext context)
        {
            string retVal = string.Empty;
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> data = jsondata();
            var restID = Convert.ToInt16(data["RestID"].ToString());
            var queryString = "select * from tblGallery where nLocationId = " + restID;
            var connectionString = GetLatestConnectionString();
            var cnt = 0;
            string template = "\"nTypeId\":\"{0}\", \"cImageBigURL\":\"{1}\", \"cImageURL\":\"{1}\"";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (cnt > 0) sb.Append(",");
                        sb.Append("{" + string.Format(template, reader["nTypeId"].ToString(), reader["cImageBigURL"].ToString(), reader["cImageURL"]) + "}");
                        cnt++;
                    }
                    retVal = "{\"status\":\"1\", \"records\": [" + sb.ToString() + "]}";
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }

            return retVal;
        }

        public string getAllNews(HttpContext context)
        {
            try
            {
                string retVal = string.Empty;
                Dictionary<string, string> data = jsondata();
                string connectionString = GetLatestConnectionString();
                StringBuilder sb = new StringBuilder();
                string template = "\"Title\":\"{0}\", \"Content\":\"{1}\", \"Date\":\"{2}\", \"imageID\":\"{3}\"";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_TCB_NR_GetAllNews", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        int cnt = 0;
                        try
                        {
                            while (reader.Read())
                            {
                                string Title = reader["Title"].ToString();
                                string Content = reader["Content"].ToString().Replace("\"", "\'");
                                string Date = reader["Date"].ToString();
                                int imageID = Convert.ToInt16(reader["imageID"]);
                                string imageBLOB = getImageBlobByID(imageID);
                                if (cnt > 0) sb.Append(",");
                                sb.Append("{" + string.Format(template, Title, Content, Date, imageID) + "}");
                                cnt++;
                            }
                            retVal = "{\"status\":\"1\", \"records\": [" + sb.ToString() + "]}";


                        }
                        finally
                        {
                            // Always call Close when done reading.
                            reader.Close();
                        }
                        cmd.ExecuteNonQuery();
                    }
                }


                return retVal;
            }
            catch (Exception ex)
            {
                return "{\"status\":\"0\", \"message\": \"" + ex.Message + "\"}";
            }

        }

        public string getSpecialOffers(HttpContext context)
        {
            try
            {
                string retVal = string.Empty;
                Dictionary<string, string> data = jsondata();
                string connectionString = GetLatestConnectionString();
                StringBuilder sb = new StringBuilder();
                string template = "\"Title\":\"{0}\", \"Content\":\"{1}\", \"Date\":\"{2}\", \"imageID\":\"{3}\"";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_PK_GetSitePromotions", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        int cnt = 0;
                        try
                        {
                            while (reader.Read())
                            {
                                string Title = reader["Title"].ToString();
                                string Content = reader["Content"].ToString();
                                string Date = reader["Date"].ToString();
                                int imageID = Convert.ToInt16(reader["imageID"]);
                                string imageBLOB = getImageBlobByID(imageID);
                                if (cnt > 0) sb.Append(",");
                                sb.Append("{" + string.Format(template, Title, Content, Date, imageID) + "}");
                                cnt++;
                            }
                            retVal = "{\"status\":\"1\", \"records\": [" + sb.ToString() + "]}";


                        }
                        finally
                        {
                            // Always call Close when done reading.
                            reader.Close();
                        }
                        cmd.ExecuteNonQuery();
                    }
                }


                return retVal;
            }
            catch (Exception ex)
            {
                return "{\"status\":\"0\", \"message\": \"" + ex.Message + "\"}";
            }

        }
        public string getImageBlobByID(int id)
        {
            string connectionString = GetLatestConnectionString();
            string imgBlob = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_CP_loadImageByImageID", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@imageID", SqlDbType.VarChar).Value = id;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            imgBlob = BinaryToText(ObjectToByteArray(reader["imageBLOB"]));
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
            return imgBlob;
        }
        public string BinaryToText(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public string BookingConfirm(HttpContext contex)
        {
            string retVal = string.Empty;
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> data = jsondata();
            int RestaurantFK = Convert.ToInt16(data["BookingRestaurantID"].ToString());
            string BookingRestaurantName = data["BookingRestaurantName"].ToString();
            string Fullname = data["BookingFirstName"].ToString() + " " + data["BookingSurName"].ToString();
            string Mobile = data["BookingMobile"].ToString();
            string Email = data["BookingEmail"].ToString();
            DateTime DateTimeReservation = DateTime.Parse(data["BookingDate"].ToString());
            int NumberOfGuests = Convert.ToInt16(data["BookingNoOfGuests"].ToString());
            string BookingTimeSlot = data["BookingTimeSlot"].ToString();
            string hour = BookingTimeSlot.Substring(0, 2);
            string minute = BookingTimeSlot.Substring(2, 2);
            string AdditionalNotes = data["BookingNotes"].ToString();
            string vPostcode = data["BookingPostalCode"].ToString();
            string sForename = data["BookingFirstName"].ToString();
            string sSurname = data["BookingSurName"].ToString();
            string sipAddress = data["BookingIpAddress"].ToString();
            int iDeposit = Convert.ToInt16(data["BookingDeposit"].ToString());
            int iseats = Convert.ToInt16(data["BookingNoOfGuests"].ToString());
            int ihigh = Convert.ToInt16(data["BookingNoOfHighChairs"].ToString());
            int iwheel = Convert.ToInt16(data["BookingNoOfWheelChairs"].ToString());
            int iprams = Convert.ToInt16(data["BookingNoOfPramsSeat"].ToString());
            string vCode = data["UniqueOrderID"].ToString();
            Random generator = new Random();
            string r = generator.Next(100000, 999999).ToString();
            vCode = r;
            string restContactNumber = string.Empty;
            string restName = string.Empty;
            string content = string.Empty;
            int output = 0;
            string connectionString = GetLatestConnectionString();
            //string connectionString = GetTempConnectionString();//this string is used for showing the demo reservation
            int reservationID = makeReservation(RestaurantFK, Fullname, Mobile, Email, DateTimeReservation, hour, minute, NumberOfGuests, AdditionalNotes, vPostcode, sForename, sSurname, sipAddress, iDeposit, iseats, ihigh, iwheel, iprams, vCode, connectionString);
            //if (base.Request.Url.AbsoluteUri.ToString().Contains("thechinesebuffet.com"))
            //{
            //    service.CreateTillReservation(this.ddlRestaurant.SelectedItem.Text.Replace("'", "''"), this.txtBillingFirstnames.Text.Replace("'", "''"), this.txtBillingSurname.Text.Replace("'", "''"), this.txtBillingPhone.Text.Replace("'", "''"), this.txtBillingNotes.Text.Replace("'", "''"), this.lblSeats.Text, reservationDate, Convert.ToString(this.Session["selectedTime"]), "0", this.ddlGuests.SelectedValue, this.ddlHighChairs.SelectedValue, this.ddlWheelChairs.SelectedValue, this.ddlPrams.SelectedValue, "EAAA8C5D-F7BD-4243-BD6C-EB6BE4C37E23");
            //}
            //CreateTillReservation(, this.txtBillingFirstnames.Text.Replace("'", "''"), this.txtBillingSurname.Text.Replace("'", "''"), this.txtBillingPhone.Text.Replace("'", "''"), this.txtBillingNotes.Text.Replace("'", "''"), this.lblSeats.Text, reservationDate, Convert.ToString(this.Session["selectedTime"]), "0", this.ddlGuests.SelectedValue, this.ddlHighChairs.SelectedValue, this.ddlWheelChairs.SelectedValue, this.ddlPrams.SelectedValue, "EAAA8C5D-F7BD-4243-BD6C-EB6BE4C37E23");
            //SoapCreateTillReservation();
            completeReservation(RestaurantFK, Fullname, Mobile, Email, DateTimeReservation, hour, minute, NumberOfGuests, AdditionalNotes, vPostcode, connectionString);
            SetReservationConfirmed(reservationID, connectionString);
            insertEntriesInTillSystem(RestaurantFK, BookingRestaurantName, sForename, sSurname, Mobile, AdditionalNotes, iseats, DateTimeReservation, Convert.ToInt16(BookingTimeSlot), iDeposit, iseats, ihigh, iwheel, iprams, connectionString);
            informConcernedPeopleNoDepositPaid(BookingRestaurantName, sSurname, vCode, sForename, Email);
            content = "Due to no deposit being paid, your reservation is not guaranteed, Please contact THE Chinese Buffet on THE Chinese Buffet Bolton on 01204 388222 to confirm your reservation. <br />Your reservation code is: <b>" + vCode + "</b>";
            var queryString = "select * from tblRestaurants where RestaurantID = " + RestaurantFK;
            var cnt = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        restContactNumber = reader["Tel"].ToString();
                        restName = reader["Restaurant"].ToString();
                    }

                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            retVal = "{\"status\":\"0\",\"Output\": \"" + vCode + "\",\"ContactNumber\": \"" + restContactNumber + "\",\"RestaurantName\": \"" + restName + "\",\"Content\": \"" + content + "\"}";
            return retVal;
        }
        public void informConcernedPeopleNoDepositPaid(string BookingRestaurantName,string sSurname, string reservationID,string sForename,string Email) 
        {
            if (BookingRestaurantName.ToLower() == "bolton")
            {
                var content = "Due to no deposit being paid, your reservation is not guaranteed, Please contact THE Chinese Buffet on THE Chinese Buffet Bolton on 01204 388222 to confirm your reservation. <br />Your reservation code is: <b>" + reservationID + "</b>";
                sendEmail("boltononlinedeposit@thechinesebuffet.com", "Bolton Manager " + sSurname, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01204 388222", reservationID.ToString()), true);
                //sendEmail("gaurav@simplex-services.com", "Bolton Manager " + sSurname, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01204 388222", reservationID.ToString()), true);
                sendEmail(Email.Trim(), sForename.Trim() + " " + sSurname.Trim(), "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet", GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01204 388222", reservationID.ToString()), true);
            }
            else if (BookingRestaurantName.ToLower() == "wigan")
            {
                var content = "Due to no deposit being paid, your reservation is not guaranteed, Please contact THE Chinese Buffet on THE Chinese Buffet Wigan on 01942 820277 to confirm your reservation. <br />Your reservation code is: <b>" + reservationID + "</b>";
                sendEmail("wiganonlinedeposit@thechinesebuffet.com", "Wigan Manager " + sSurname.Trim(), "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01942 820277", reservationID.ToString()), true);
                sendEmail(Email.Trim(), sForename.Trim() + " " + sSurname.Trim(), "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet", GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01942 820277", reservationID.ToString()), true);
            }
            else if (BookingRestaurantName.ToLower() == "wrexham")
            {
                var content = "Due to no deposit being paid, your reservation is not guaranteed, Please contact THE Chinese Buffet on THE Chinese Buffet Wrexham on 01978 266898 to confirm your reservation. <br />Your reservation code is: <b>" + reservationID + "</b>";
                sendEmail("wrexhamonlinedeposit@thechinesebuffet.com", "Wrexham Manager " + sSurname, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01978 266898", reservationID.ToString()), true);
                sendEmail(Email.Trim(), sForename.Trim() + " " + sSurname.Trim(), "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet", GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01978 266898", reservationID.ToString()), true);
            }
            else if (BookingRestaurantName.ToLower() == "bradford")
            {
                var content = "Due to no deposit being paid, your reservation is not guaranteed, Please contact THE Chinese Buffet on THE Chinese Buffet Bradford on 01274 733777 to confirm your reservation. <br />Your reservation code is: <b>" + reservationID + "</b>";
                sendEmail("bradfordonlinedeposit@thechinesebuffet.com", "Bradford Manager " + sSurname, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01274 733777", reservationID.ToString()), true);
                sendEmail(Email.Trim(), sForename.Trim() + " " + sSurname.Trim(), "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet", GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01274 733777", reservationID.ToString()), true);
            }
            else if (BookingRestaurantName.ToLower() == "wakefield")
            {
                var content = "Due to no deposit being paid, your reservation is not guaranteed, Please contact THE Chinese Buffet on THE Chinese Buffet Wakefield on 01924 339322 to confirm your reservation. <br />Your reservation code is: <b>" + reservationID + "</b>";
                sendEmail("wakefieldonlinedeposit@thechinesebuffet.com", "Wakefield Manager " + sSurname, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01924 339322", reservationID.ToString()), true);
                sendEmail(Email.Trim(), sForename.Trim() + " " + sSurname.Trim(), "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet", GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01924 339322", reservationID.ToString()), true);
            }
            else if (BookingRestaurantName.ToLower() == "st helens")
            {
                var content = "Due to no deposit being paid, your reservation is not guaranteed, Please contact THE Chinese Buffet on THE Chinese Buffet ST Helens on 01744 610002 to confirm your reservation. <br />Your reservation code is: <b>" + reservationID + "</b>";
                sendEmail("sthelensonlinedeposit@thechinesebuffet.com", "ST Helens Manager " + sSurname, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01744 610002", reservationID.ToString()), true);
                sendEmail(Email.Trim(), sForename.Trim() + " " + sSurname.Trim(), "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet", GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01744 610002", reservationID.ToString()), true);
            }
            else if (BookingRestaurantName.ToLower() == "preston")
            {
                var content = "Due to no deposit being paid, your reservation is not guaranteed, Please contact THE Chinese Buffet on THE Chinese Buffet Preston on 01772 883088 to confirm your reservation. <br />Your reservation code is: <b>" + reservationID + "</b>";
                sendEmail("prestononlinedeposit@thechinesebuffet.com", "Preston Manager " + sSurname, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01772 883088", reservationID.ToString()), true);
                sendEmail(Email.Trim(), sForename.Trim() + " " + sSurname.Trim(), "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet", GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01772 883088", reservationID.ToString()), true);
            }
            else if (BookingRestaurantName.ToLower() == "halifax")
            {
                var content = "Due to no deposit being paid, your reservation is not guaranteed, Please contact THE Chinese Buffet on THE Chinese Buffet Halifax on 01422 354001 to confirm your reservation. <br />Your reservation code is: <b>" + reservationID + "</b>";
                sendEmail("halifaxonlinedeposit@thechinesebuffet.com", "Halifax Manager " + sSurname, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01422 354001", reservationID.ToString()), true);
                sendEmail(Email.Trim(), sForename.Trim() + " " + sSurname.Trim(), "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet", GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01422 354001", reservationID.ToString()), true);
            }
            else if (BookingRestaurantName.ToLower() == "darlington")
            {
                var content = "Due to no deposit being paid, your reservation is not guaranteed, Please contact THE Chinese Buffet on THE Chinese Buffet Halifax on 01422 354001 to confirm your reservation. <br />Your reservation code is: <b>" + reservationID + "</b>";
                sendEmail("darlingtononlinedeposit@thechinesebuffet.com", "Darlington Manager " + sSurname, "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet Reservation Code = " + reservationID, GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01422 354001", reservationID.ToString()), true);
                sendEmail(Email.Trim(), sForename.Trim() + " " + sSurname.Trim(), "sales@thechinesebuffet.com", "THE Chinese Buffet", "Online Reservation at THE Chinese Buffet", GetReservationEmailBody(sForename.Trim() + " " + sSurname.Trim(), "01325 488889", reservationID.ToString()), true);
            }
        }
        public string getItemMenus(HttpContext context)
        {
            string retVal = string.Empty;
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> data = jsondata();
            var RestID = Convert.ToInt16(data["RestID"].ToString());
            var connectionString = GetLatestConnectionString();
            var menuID = 0;
            List<int> menuIds = new List<int>();
            var cnt = 0;
            string template = "\"id\":\"{0}\", \"name\":\"{1}\"";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_TCB_CP_LoadLiveMenus", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = RestID;
                        //cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                menuID = Convert.ToInt16(reader["MenuID"]);
                                menuIds.Add(menuID);
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
                for (var i = 0; i <= menuIds.Count - 1; i++)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_TCB_CP_getMenuItems", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@iMenuID", SqlDbType.Int).Value = menuIds[i];
                            //cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
                            con.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            try
                            {
                                while (reader.Read())
                                {
                                    sb.Append("<h5>" + reader["vProductEnglishName"].ToString() + "</h5>");
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
                }
                retVal = sb.ToString();
            }
            catch (Exception ex)
            {
                retVal = "{\"status\":\"0\", \"message\": \"" + ex.Message + "\"}";
            }

            return retVal;
        }

        public string getItems(HttpContext context)
        {
            string retVal = string.Empty;
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> data = jsondata();
            var menuID = Convert.ToInt16(data["menuID"].ToString());
            var connectionString = GetLatestConnectionString();
            var cnt = 0;
            string template = "\"id\":\"{0}\", \"name\":\"{1}\"";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_TCB_CP_getMenuItems", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@iMenuID", SqlDbType.Int).Value = menuID;
                        //cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                sb.Append("<h5>" + reader["vProductEnglishName"].ToString() + "</h5>");
                            }
                            retVal = sb.ToString();

                        }
                        finally
                        {
                            // Always call Close when done reading.
                            reader.Close();
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = "{\"status\":\"0\", \"message\": \"" + ex.Message + "\"}";
            }

            return retVal;
        }

        public string getMenus(HttpContext context)
        {
            string retVal = string.Empty;
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> data = jsondata();
            var restID = Convert.ToInt16(data["RestID"].ToString());
            var queryString = "select * from tblMenu where RestaurantID = " + restID;
            var connectionString = GetLatestConnectionString();
            var cnt = 0;
            string template = "\"id\":\"{0}\", \"name\":\"{1}\"";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (cnt > 0) sb.Append(",");
                        sb.Append("{" + string.Format(template, reader["MenuID"].ToString(), reader["MenuName"].ToString()) + "}");
                        cnt++;
                    }
                    retVal = "{\"status\":\"1\", \"records\": [" + sb.ToString() + "]}";
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }

            return retVal;
        }
        public string getAllRestaurants(HttpContext context)
        {
            string retVal = string.Empty;
            StringBuilder sb = new StringBuilder();
            var queryString = "select * from tblRestaurants where DisplayOnWebsite = 1";
            var connectionString = GetLatestConnectionString();
            var cnt = 0;
            string template = "\"id\":\"{0}\", \"name\":\"{1}\"";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (cnt > 0) sb.Append(",");
                        sb.Append("{" + string.Format(template, reader["RestaurantID"].ToString(), reader["Restaurant"].ToString()) + "}");
                        cnt++;
                    }
                    retVal = "{\"status\":\"1\", \"records\": [" + sb.ToString() + "]}";
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }

            return retVal;
        }
        public string bookTable(HttpContext context)
        {
            try
            {
                string retVal = string.Empty;
                Dictionary<string, string> data = jsondata();
                var restID = Convert.ToInt16(data["restID"].ToString());
                DateTime bookingDateTime = DateTime.Parse(data["bookingDateTime"].ToString());
                var clientipaddress = data["ipaddress"].ToString();
                var queryString = "select * from tblRestaurants where RestaurantID = " + restID;
                var firstquery = "select * from tblReservation where IpAddress = '" + clientipaddress + "' and dtDateCreated > '" + DateTime.UtcNow.Date + "'";
                string ipaddress = string.Empty;
                string portnumber = string.Empty;
                string connectionString = GetLatestConnectionString();
                StringBuilder sb = new StringBuilder();
                var noofbookings = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(firstquery, connection);
                    command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            noofbookings++;
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
                if (noofbookings >= 2)
                {
                    return "{\"status\":\"-1\", \"Message\": \"Sorry you can't make more than two bookings per day.\"}";
                }
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
                //"Data Source=" + ipaddress + "," + portnumber + ";Initial Catalog=ChiineseTill;User Id=sa;Password=sa@123";
                var cnt = 0;
                string template = "\"Time\":\"{0}\", \"NoOfSpaces\":\"{1}\", \"MaxReservationsAtThisTime\":\"{2}\"";
                decimal reservationPrice = GetReservationPrice(bookingDateTime);
                using (SqlConnection con = new SqlConnection(serverConnString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_MF_GetAvailableReservationTimes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = bookingDateTime;
                        //cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                if (cnt > 0) sb.Append(",");
                                sb.Append("{" + string.Format(template, reader["Time"].ToString(), reader["NoOfSpaces"].ToString(), reader["MaxReservationsAtThisTime"].ToString()) + "}");
                                cnt++;
                            }
                            var maxnondepositcount = ConfigurationManager.AppSettings["maxNonBookingPersonCount"].ToString();
                            retVal = "{\"status\":\"1\", \"records\": [" + sb.ToString() + "],\"maxNonBookingPersonCount\":\"" + maxnondepositcount +"\",\"depositPerPerson\":\"" + reservationPrice +"\"}";
                        }
                        finally
                        {
                            // Always call Close when done reading.
                            reader.Close();
                        }
                        cmd.ExecuteNonQuery();
                    }
                }

                return retVal;
            }
            catch (Exception ex)
            {
                return "{\"status\":\"0\", \"message\": \"" + ex.Message + "\"}";
            }

        }
        public decimal GetReservationPrice(DateTime dtDate)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = GetLatestConnectionString();
            decimal result;
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandText = "USP_NS_GetReservationPrice";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.Add("@dtDate", SqlDbType.DateTime).Value = dtDate;
                decimal num = Convert.ToDecimal(sqlCommand.ExecuteScalar());
                if (decimal.Compare(num, decimal.Zero) <= 0)
                {
                    result = new decimal(5L);
                }
                else
                {
                    result = num;
                }
            }
            catch (Exception expr_90)
            {
                Exception ex = expr_90;
                throw new Exception("Error occured while getting reservation price " + ex.ToString());
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
        public string getAddress(HttpContext context)
        {
            try
            {
                Dictionary<string, string> data = jsondata();
                var locid = Convert.ToInt16(data["locid"].ToString());
                var queryString = "select * from tblRestaurants where RestaurantID = " + locid;
                string cAddress = string.Empty;
                string cContact = string.Empty;
                string cMap = string.Empty;
                string OpeningTimesString = string.Empty;
                string content = string.Empty;
                string connectionString = GetLatestConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            cAddress = reader["AddressLine1"].ToString() + ", " + reader["AddressLine2"].ToString() + " " + reader["PostCode"].ToString();
                            cContact = reader["Tel"].ToString();
                            cMap = reader["Map"].ToString().Replace("\"", "\'");
                            OpeningTimesString = reader["OpeningTimesString"].ToString();
                            content = reader["Content"].ToString().Replace("\"", "\'");
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
                return "{\"status\":\"1\",\"cAddress\": \"" + cAddress + "\",\"cContact\": \"" + cContact + "\",\"cMap\": \"" + cMap + "\",\"OpeningTimesString\": \"" + OpeningTimesString + "\",\"cContent\": \"" + content + "\"}";
                //return string.Format("cAddress :'{0}';cContact:{1};cMap:'{2}';cDays:'{3}';cTimings:'{4}'", cAddress, cContact, cMap, cDays, cTimings);
            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public string Hello(HttpContext context)
        {
            return string.Format("Name :'{0}';Age:{1};Qualification:'{2}'", "Furquan", "25", "B.Tech");
        }
        public static string GetOldConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["tcbConnectionString"].ConnectionString;
        }
        public static string GetLatestConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
        }

        public static string GetTempConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["tcbTempConnectionString"].ConnectionString;
        }

        public static string GetServerConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["tcbServerConnectionString"].ConnectionString;
        }
        private Dictionary<string, string> jsondata()
        {
            string jsonString = String.Empty;

            HttpContext.Current.Request.InputStream.Position = 0;
            using (StreamReader inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            //return inputValues;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region "Existing Codes"
        public int makeReservation(int intRestaurantID, string strFullname, string strMobile, string strEmail, DateTime dReservationDate, string strHour, string strMinute, int intGuest, string strNotes, string strPostcode, string sForname, string sSurname, string sIpAddress, int iDeposit, int iseats, int ihigh, int iwheel, int iprams, string sRand, string connectionString)
        {
            int result;
            int output = 0;
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
                if (strMobile.Substring(0, 3) == "+44")
                {
                    strMobile = strMobile.Substring(3);
                }
                if (strMobile.Substring(0, 1) != "0")
                {
                    strMobile = "0" + strMobile;
                }
                SqlParameter sqlParameter = new SqlParameter("@iOut", SqlDbType.Int);
                sqlParameter.Direction = ParameterDirection.Output;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_TCB_RC_storeReservationv6", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RestaurantFK", intRestaurantID);
                        cmd.Parameters.AddWithValue("@Fullname", "***" + strFullname);
                        cmd.Parameters.AddWithValue("@Mobile", strMobile);
                        cmd.Parameters.AddWithValue("@Email", strEmail);
                        cmd.Parameters.AddWithValue("@DateTimeReservation", value);
                        cmd.Parameters.AddWithValue("@NumberOfGuests", intGuest);
                        cmd.Parameters.AddWithValue("@AdditionalNotes", strNotes);
                        cmd.Parameters.AddWithValue("@vPostcode", strPostcode);
                        cmd.Parameters.AddWithValue("@sForename", sForname);
                        cmd.Parameters.AddWithValue("@sSurname", sSurname);
                        cmd.Parameters.AddWithValue("@sipAddress", sIpAddress);
                        cmd.Parameters.AddWithValue("@iDeposit", iDeposit);
                        cmd.Parameters.AddWithValue("@iseats", iseats);
                        cmd.Parameters.AddWithValue("@ihigh", ihigh);
                        cmd.Parameters.AddWithValue("@iwheel", iwheel);
                        cmd.Parameters.AddWithValue("@iprams", iprams);
                        cmd.Parameters.AddWithValue("@vCode", sRand);


                        SqlParameter outPutID = new SqlParameter();
                        outPutID.ParameterName = "@iOut";
                        outPutID.SqlDbType = SqlDbType.Int;
                        outPutID.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outPutID);
                        //cmd.Parameters.Add("@iOut ", SqlDbType.Int).Direction = ParameterDirection.Output;

                        try
                        {
                            if (cmd.Connection.State == ConnectionState.Closed)
                            {
                                cmd.Connection.Open();
                            }
                            cmd.ExecuteNonQuery();
                            output = (int)outPutID.Value;
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
                string gmailid = ConfigurationManager.AppSettings["emailID"].ToString();
                string password = ConfigurationManager.AppSettings["password"].ToString();
                string toAddress1 = ConfigurationManager.AppSettings["toAddress"].ToString();
                sendEmail("development@yellowbus.co.uk", "Yellowbus", "info@thechinesebuffet.com", "tcb", "error in makeReservation", ex.Message + "\r\n" + ex.StackTrace.ToString(), true);
                throw new Exception("error making reservation on local server : " + ex.Message.ToString());
            }
            return output;
        }
        public void insertEntriesInTillSystem(int restID, string restaurantname, string sForename, string sSurname, string contactnumber, string reservationnotes, int noofguests, DateTime reservationdate, int reservationtime, int iDeposit, int iseats, int ihigh, int iwheel, int iprams, string connectionString)
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

        //public bool CreateTillReservation(string sRestaurantName, string Forename, string Surname, string ContactNumber, string ReservationNotes, string NoOfGuests, string ReservationDate, string ReservationTime, string Deposit, string NoOfSeats, string NoOfHighChairs, string NoOfWheelChairs, string NoOfPrams, string AccessID)
        //{
        //    object[] array = base.Invoke("CreateTillReservation", new object[]
        //    {
        //        sRestaurantName,
        //        Forename,
        //        Surname,
        //        ContactNumber,
        //        ReservationNotes,
        //        NoOfGuests,
        //        ReservationDate,
        //        ReservationTime,
        //        Deposit,
        //        NoOfSeats,
        //        NoOfHighChairs,
        //        NoOfWheelChairs,
        //        NoOfPrams,
        //        AccessID
        //    });
        //    return (bool)array[0];
        //}
        public void SoapCreateTillReservation()
        {
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <HelloWorld xmlns=""http://tempuri.org/"" />
                  </soap:Body>
                </soap:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    Console.WriteLine(soapResult);
                }
            }
        }
        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://tempuri.org/CreateTillReservation");
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
        public string GetReservationEmailBody(string strName, string strTelephone, string strResCode)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<table><tr><td><img src='http://www.thechinesebuffet.com/images/logo.jpg' /><br /><br />");
            stringBuilder.Append("Dear " + strName);
            stringBuilder.Append("<br /><br />Thank you for making a reservation at THE Chinese Buffet.");
            stringBuilder.Append("<br /><br />Your reservation code is " + strResCode);
            stringBuilder.Append("<br />If you would prefer to amend your booking, you can telephone <b>" + strTelephone + "</b><br />");
            stringBuilder.Append("<br /><br />Customer services<br />");
            stringBuilder.Append("THE Chinese Buffet<br /><br />");
            stringBuilder.Append("Tel: " + strTelephone + "<br />");
            stringBuilder.Append("Web: http://www.thechinesebuffet.com </td></tr></table>");
            stringBuilder.Append("<br /><br /><table style='font-size:9px;color:#999999;'><tr><td>STUDENT DISCOUNTS<br />");
            stringBuilder.Append("Students receive 10% discount off their bill on Mondays and Tuesdays. You must produce your student id with photo. This promotion may be cancelled at any time without prior notice. All promotion offers applies to our general terms and conditions<br /><br />");
            stringBuilder.Append("ST HELENS <span class=\"pound\"></span>5 CODE<br />");
            stringBuilder.Append("Codes can be redeemed at our St Helensbranch 12 hours after receiving the code. MUST BE A MINIMUM of 2 dinning. Text messages are charged at your networks standard rate. Only one code per mobilenumber per week. Limited codes are available. Also applies to Our General promotional offers terms and conditions below.<br /><br />");
            stringBuilder.Append("For more Terms and Conditions, <a href='http://www.thechinesebuffet.com/restaurants/bolton/terms_and_conditions.html'>click here</a>.<br />");
            stringBuilder.Append("<br /><br /><img src='http://www.thechinesebuffet.com/images/logo.jpg' /></td></tr></table>");
            return stringBuilder.ToString();
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

        #endregion


        #region "Payment Gateway"
        //public void PayDeposit(object sender, EventArgs e)
        //{
        //    string text = "";
        //    int num = 0;
        //    clsGeneric clsGeneric = new clsGeneric();
        //    if (!this.validateForm())
        //    {
        //        this.lblError.Text = text;
        //        this.pnlError.Visible = true;
        //        return;
        //    }
        //    this.txtBillingFirstnames.Text = clsGeneric.cleanInput(this.txtBillingFirstnames.Text, "Text");
        //    this.txtBillingSurname.Text = clsGeneric.cleanInput(this.txtBillingSurname.Text, "Text");
        //    this.txtBillingAddress1.Text = clsGeneric.cleanInput(this.txtBillingAddress1.Text, "Text");
        //    this.txtBillingAddress2.Text = clsGeneric.cleanInput(this.txtBillingAddress2.Text, "Text");
        //    this.txtBillingCity.Text = clsGeneric.cleanInput(this.txtBillingCity.Text, "Text");
        //    this.txtBillingPostCode.Text = clsGeneric.cleanInput(this.txtBillingPostCode.Text, "Text");
        //    this.txtBillingPhone.Text = clsGeneric.cleanInput(this.txtBillingPhone.Text, "Text");
        //    this.txtCustomerEMail.Text = clsGeneric.cleanInput(this.txtCustomerEMail.Text, "Text");
        //    if (string.IsNullOrEmpty(this.txtBillingFirstnames.Text))
        //    {
        //        text = "Please enter your First Names(s) where requested below.";
        //    }
        //    else if (string.IsNullOrEmpty(this.txtBillingSurname.Text))
        //    {
        //        text = "Please enter your Surname where requested below.";
        //    }
        //    else if (string.IsNullOrEmpty(this.txtBillingAddress1.Text))
        //    {
        //        text = "Please enter your Address Line 1 where requested below.";
        //    }
        //    else if (string.IsNullOrEmpty(this.txtBillingCity.Text))
        //    {
        //        text = "Please enter your City where requested below.";
        //    }
        //    else if (string.IsNullOrEmpty(this.txtBillingPostCode.Text))
        //    {
        //        text = "Please enter your Post Code where requested below.";
        //    }
        //    else if (this.txtCustomerEMail.Text == "")
        //    {
        //        text = "Please enter your e-mail address where requested below.";
        //    }
        //    else if (string.IsNullOrEmpty(this.txtBillingPhone.Text))
        //    {
        //        text = "Please enter your phone number where requested below.";
        //    }
        //    else if (!string.IsNullOrEmpty(this.txtCustomerEMail.Text) && (this.txtCustomerEMail.Text.IndexOf("@") < 1 || this.txtCustomerEMail.Text.IndexOf(".") < 1))
        //    {
        //        text = "The email address entered was invalid.";
        //    }
        //    else
        //    {
        //        this.Session["strBillingFirstnames"] = this.txtBillingFirstnames.Text;
        //        this.Session["strBillingSurname"] = this.txtBillingSurname.Text;
        //        this.Session["strBillingAddress1"] = this.txtBillingAddress1.Text;
        //        this.Session["strBillingAddress2"] = this.txtBillingAddress2.Text;
        //        this.Session["strBillingCity"] = this.txtBillingCity.Text;
        //        this.Session["strBillingPostCode"] = this.txtBillingPostCode.Text;
        //        this.Session["strBillingPhone"] = this.txtBillingPhone.Text;
        //        this.Session["strCustomerEMail"] = this.txtCustomerEMail.Text;
        //        int num2 = Convert.ToInt32(this.ddlGuests.SelectedValue) - Convert.ToInt32(this.ddlHighChairs.SelectedValue) - Convert.ToInt32(this.ddlPrams.SelectedValue);
        //        double num3 = 5.0;
        //        double fOrderTotal = num3 * (double)num2;
        //        clsOrders clsOrders = new clsOrders();
        //        int num4 = clsOrders.InsertOrder(Convert.ToInt32(this.ddlRestaurant.SelectedValue), this.txtBillingFirstnames.Text + " " + this.txtBillingSurname.Text, " ", string.Concat(new string[]
        //        {
        //            this.txtBillingAddress1.Text,
        //            " ",
        //            this.txtBillingAddress2.Text,
        //            " ",
        //            this.txtBillingCity.Text
        //        }), this.txtBillingPostCode.Text, 0, this.txtBillingPhone.Text, this.txtCustomerEMail.Text, fOrderTotal, true);
        //        if (num4 > 0)
        //        {
        //            this.ReservationOrderID = num4;
        //            this.IsReservationOrder = true;
        //            num = Convert.ToInt32((double)num2 * num3);
        //            clsOrders.InsertOrderItem(Convert.ToInt32(this.ddlRestaurant.SelectedValue), num4, 0, num2, 5f, Convert.ToSingle(num));
        //        }
        //        try
        //        {
        //            if (this.validateForm())
        //            {
        //                DateTime dReservationDate = DateTime.Parse(this.txtCollectDate.Text);
        //                using (functions functions = new functions())
        //                {
        //                    string sRand = this.RandomString(4, false);
        //                    string strHour = this.Session["selectedTime"].ToString().Substring(0, 2);
        //                    string strMinute = this.Session["selectedTime"].ToString().Substring(2);
        //                    this.reservationID = functions.makeReservation(Convert.ToInt32(this.ddlRestaurant.SelectedValue), this.txtBillingFirstnames.Text.Trim() + " " + this.txtBillingSurname.Text.Trim(), this.txtBillingPhone.Text.Trim(), this.txtCustomerEMail.Text.Trim(), dReservationDate, strHour, strMinute, Convert.ToInt32(this.ddlGuests.SelectedValue), this.txtBillingNotes.Text.Trim(), this.txtBillingPostCode.Text, this.txtBillingFirstnames.Text.Trim(), this.txtBillingSurname.Text.Trim(), base.Request.UserHostAddress.ToString(), num, Convert.ToInt32(this.lblSeats.Text), Convert.ToInt32(this.ddlHighChairs.SelectedValue), Convert.ToInt32(this.ddlWheelChairs.SelectedValue), Convert.ToInt32(this.ddlPrams.SelectedValue), sRand);
        //                    clsOrders.UpdateReservationByOrder(num4, this.reservationID);
        //                }
        //                this.resetForm();
        //                this.HideAllPanels();
        //                this.pnlForm.Visible = false;
        //                this.pnlSent.Visible = true;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            text = "There was a problem submitting your reservation request. Please try again later. If you would prefer to call to make your booking you can telephone <b>01204 388222</b><br/><br/>";
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(text))
        //    {
        //        this.lblError.Text = text;
        //        this.pnlError.Visible = true;
        //        return;
        //    }
        //    this.Session["order"] = "deposit";
        //    base.Response.Clear();
        //    base.Response.Redirect("~/restaurants/" + this.sRestaurant + "/transactionregistration.html");
        //    base.Response.End();
        //}

        
        #endregion

        #region "Entry of payment"
        public int InsertOrder(int iResturantID, string vCustomerName, string vHouseNumber, string vFullAddress, string PostCode, int collectionTime, string vCustomerPhone, string vCustomerEmail, double fOrderTotal, bool bReservation = false)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = GetLatestConnectionString();
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
            sqlConnection.ConnectionString = GetLatestConnectionString();
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

        public void UpdateReservationByOrder(int iOrderID, int iReservationID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = GetLatestConnectionString();
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
 

        public int InsertPayment(int iOrderID, string vTransactionID, DateTime dtPaymentDate, float fPrice, bool bIsSuccess, string vFaultReason, int iPaymentType, string SagePay_SecurityKey, string SagePay_VendorTXCode)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = new SqlConnection();
            var connectionstring = GetLatestConnectionString();
            sqlConnection.ConnectionString = connectionstring;
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
            var connectionstring = GetLatestConnectionString();
            sqlConnection.ConnectionString = connectionstring;
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
            var connectionstring = GetLatestConnectionString();
            sqlConnection.ConnectionString = connectionstring;
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
            var connectionstring = GetLatestConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
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
            var connectionstring = GetLatestConnectionString();
            sqlConnection.ConnectionString = connectionstring;
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
            var connectionstring = GetLatestConnectionString();
            sqlConnection.ConnectionString = connectionstring;
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
            var connectionstring = GetLatestConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
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

        #endregion

        

    }
}