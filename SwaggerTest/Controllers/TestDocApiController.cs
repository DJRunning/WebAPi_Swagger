using SwaggerTest.Auth;
using SwaggerTest.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwaggerTest.Controllers
{
    /// <summary>
    /// 测试用的
    /// </summary>
    //[ApiAuthorizationFilterAttribute]
    public class TestDocApiController : ApiController
    {
        /// <summary>
        /// 得到所有人
        /// </summary>
        /// <returns>返回所有人</returns>
        [HttpGet]
        [AllowAnonymous]
        public string GetAllPeople()
        {
            return "Peoples";
        }
        /// <summary>
        /// 得到指定人
        /// </summary>
        /// <param name="name">指定人名字</param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK,Description ="接口获取成功",Type =typeof(BaseResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Error", Type = typeof(BaseResponse))]
        [HttpGet]
        //[ApiAuthorizationFilter]

        public HttpResponseMessage GetPeopleByName(string name)
        {
            if (name == "ding")
            {
                return Request.CreateResponse<BaseResponse>(HttpStatusCode.OK, new BaseResponse() { State = true, Code = 0, Msg = name });
            }
            else
            {
                return Request.CreateResponse<BaseResponse>(HttpStatusCode.BadRequest, new BaseResponse() { State = false, Code = -99, Msg = "Errrr" });
            }
        }
        /// <summary>
        /// Post提交
        /// </summary>
        /// <param name="p">人的信息</param>
        /// <returns>提交是否成功</returns>
        [HttpPost]
        public bool CommitPeople(People p)
        {
            return true;
        }
        /// <summary>
        /// delete 删除指定人员
        /// </summary>
        /// <param name="name">人员名字</param>
        /// <returns>是否删除成功</returns>
        public bool DelPeople(string name)
        {
            return true;
        }
    }
   

}
