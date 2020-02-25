using SwaggerTest.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SwaggerTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //注册全局Filter
            config.Filters.Add(new ApiAuthorizationFilterAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //如果使用newtonsoft.json这个库，序列化出来的JSON，包含了为NULL的字段，导致swagger-ui-min-js出现异常，应该忽略为NULL的字段
            //var jsonFormatter = new JsonMediaTypeFommatter();
            //var setting = jsonFormatter.SerializerSettings;
            //setting.NullValueHandling = NullValueHandling.Ignore;
            //config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
        }
    }
}
