using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace WebAPI.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config này dùng để lọc ra trang admin thì dùng cơ chế xác thực token, còn trang user thì dùng cơ chế xác thực cookie
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //không hiện thị các giá trị mặc định của Serializer
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
           new DefaultContractResolver { IgnoreSerializableAttribute = true };

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}