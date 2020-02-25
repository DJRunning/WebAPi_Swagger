using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwaggerTest.Models
{
    /// <summary>
    /// 统一返回类型
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// True:成功，False:失败
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string Msg { get; set; }
    }
}