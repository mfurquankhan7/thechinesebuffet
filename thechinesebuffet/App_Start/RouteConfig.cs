using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using System.Xml;

namespace tcb
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.Add("UseridRoute", new Route("login",new CustomRouteHandler("~/v1/user/login.aspx") ));
            try
            {

                string path = HttpContext.Current.Server.MapPath("~/App_Data/reouting.xml");
                var routingdoc = new XmlDocument();
                routingdoc.Load(path);
                XmlNodeList allRoutes = routingdoc.SelectNodes("//Routing//route");
                for (var i=0;i<=allRoutes.Count-1;i++){
                    routes.MapPageRoute(allRoutes[i].Attributes["name"].Value, allRoutes[i].Attributes["key"].Value, allRoutes[i].Attributes["url"].Value);
                }
            }catch(Exception ex) {
                throw ex;
            }
        }
    }
}
