using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwaggerTest.Models
{
    /// <summary>
    /// 人实体
    /// </summary>
    public class People
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }
}