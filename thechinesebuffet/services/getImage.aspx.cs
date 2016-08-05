using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
namespace tcb.web.services
{
    public partial class getImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = string.Empty;
            string path = string.Empty;
            byte[] imgBlob = null;
            string contenttype = string.Empty;
            try
            {
                string id = Request.QueryString["id"].ToString();
                string connectionString = GetLatestConnectionString();
                
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
                                //imgBlob = ObjectToByteArray(reader["imageBLOB"]);
                                //contenttype = reader["contentType"].ToString();
                                Response.Clear();
                                Response.ContentType = reader["contentType"].ToString();
                                Response.BinaryWrite((byte[])reader["imageBLOB"]);
                                Response.End();  
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
            catch (Exception ex)
            {
                path = (Server.MapPath("~") + "\\images\\no-image-uploaded.jpg");
            }
            finally
            {
                //Response.Clear();
                //Response.ContentType = contenttype;
                //// Set image height and width to be loaded on web page
                //byte[] buffer = imgBlob;
                //Response.OutputStream.Write(buffer, 0, buffer.Length);
                //Response.End();  
            }
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
        public static string GetLatestConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["tcbLatestConnectionString"].ConnectionString;
        }
        byte[] getResizedImage(String path, int width, int height)
        {
            Bitmap imgIn = new Bitmap(path);
            double y = imgIn.Height;
            double x = imgIn.Width;

            double factor = 1;
            if (width > 0)
            {
                factor = width / x;
            }
            else if (height > 0)
            {
                factor = height / y;
            }
            System.IO.MemoryStream outStream = new System.IO.MemoryStream();
            Bitmap imgOut = new Bitmap((int)(x * factor), (int)(y * factor));

            // Set DPI of image (xDpi, yDpi)
            imgOut.SetResolution(72, 72);

            Graphics g = Graphics.FromImage(imgOut);
            g.Clear(Color.White);
            g.DrawImage(imgIn, new Rectangle(0, 0, (int)(factor * x), (int)(factor * y)),
              new Rectangle(0, 0, (int)x, (int)y), GraphicsUnit.Pixel);

            imgOut.Save(outStream, getImageFormat(path));
            return outStream.ToArray();
        }

        string getContentType(string path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return "Image/bmp";
                case ".gif": return "Image/gif";
                case ".jpg": return "Image/jpeg";
                case ".png": return "Image/png";
                default: break;
            }
            return "";
        }

        ImageFormat getImageFormat(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".jpg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                default: break;
            }
            return ImageFormat.Jpeg;
        }

    }
}