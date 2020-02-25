using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SwaggerTest.Auth
{
    /// <summary>
    /// Token 过滤器
    /// </summary>
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ApiAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// 认证和授权
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //如果用户方位的Action带有AllowAnonymousAttribute，则不进行验证
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                base.OnAuthorization(actionContext);
                return;
            }
            if (Thread.CurrentPrincipal!=null&&Thread.CurrentPrincipal.Identity.IsAuthenticated)//授权过的
            {
                base.OnAuthorization(actionContext);
                return;
            }
            string authParam = null;
            var authorization = actionContext.Request.Headers.Authorization;

            IEnumerable<string> c = null;
            var _token = actionContext.Request.Headers.TryGetValues("Auth-Token", out c);//获取Headers中指定参数的值

            if (authorization != null && authorization.Scheme == "Basic")
            {
                authParam = authorization.Parameter;//authParam:获取请求中经过Base64编码的（用户：密码）
            }
            if (string.IsNullOrEmpty(authParam))
            {
                Challenge(actionContext);
                return;
            }
            authParam = Encoding.Default.GetString(Convert.FromBase64String(authParam));
            var authToken = authParam.Split(':');
            if (authToken.Length<2)
            {
                Challenge(actionContext);
                return;
            }
            if (!IsValid(authToken[0],authToken[1]))
            {
                Challenge(actionContext);
                return;
            }

            //授权 IPrincipal
            var principal = new GenericPrincipal(new GenericIdentity(authToken[0]), null);
            Thread.CurrentPrincipal = principal;
            base.OnAuthorization(actionContext);
        }
       /// <summary>
       /// 在浏览器中弹出输入用户名和密码
       /// </summary>
       /// <param name="actionContext"></param>
        void Challenge(HttpActionContext actionContext)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));
        }
        /// <summary>
        /// 权限验证，可重写
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public virtual bool IsValid(string userName, string userPassword)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userPassword))

                return false;
            else
                return true;

        }
    }
}