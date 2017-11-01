using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace GZRYVillageWeb
{
    /// <summary>
    /// WebApi配置
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="config">配置</param>
        public static void Register(HttpConfiguration config)
        {
            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            ).RouteHandler = new SessionControllerRouteHandler();
        }
    }



    /// <summary>
    /// Session路由处理
    /// </summary>
    partial class SessionRouteHandler : HttpControllerHandler, IRequiresSessionState
    {
        /// <summary>
        /// Session路由处理
        /// </summary>
        /// <param name="routeData"></param>
        public SessionRouteHandler(RouteData routeData)
            : base(routeData)
        {
        }
    }
    /// <summary>
    /// Session控制器路由处理
    /// </summary>
    partial class SessionControllerRouteHandler : HttpControllerRouteHandler
    {
        /// <summary>
        /// 获得Http处理
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new SessionRouteHandler(requestContext.RouteData);
        }
    }
}
