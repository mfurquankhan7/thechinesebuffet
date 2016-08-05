using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Reflection;
using System.Drawing;
//using tcb.services;

namespace tcb.web.Extensions
{
    public static class ExtensionMethods
    {
        //public static string installpath = "";

        //public static string toGridJson(this List<object> obj, string tablename)
        //{
        //    return toGridJson(obj, tablename, -1);
        //}

        //public static string toGridJson(this List<object> obj, string tablename, int numberofrecords)
        //{
        //    var json = string.Empty;
        //    StringBuilder oRecords = new StringBuilder();
        //    string[] fieldinfo;
        //    json = GridConfig.getgrid(tablename, out fieldinfo);
        //    for (int i = 0; i <= obj.Count - 1; i++)
        //    {

        //        if (numberofrecords != -1)//this is to exit the loop if number of records is specified
        //        {
        //            if (i >= numberofrecords)
        //                break;
        //        }

        //        if (i > 0) oRecords.Append(",");
        //        oRecords.Append("{");
        //        for (int j = 0; j <= fieldinfo.Length - 1; j++)
        //        {
        //            if (j > 0) oRecords.Append(",");
        //            oRecords.Append("\"" + fieldinfo[j].ToLower() + "\":\"" + GetPropValue(obj[i], fieldinfo[j]) + "\"");
        //        }
        //        oRecords.Append("}");
        //    }
        //    return "{\"status\":\"1\", \"fields\": [" + json + "], \"records\": [" + oRecords.ToString() + "]}";
        //}


        //public static object GetPropValue(object src, string propName)
        //{
        //    return src.GetType().GetProperty(propName).GetValue(src, null);
        //}
    }
}