using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// MemberCard验证
    /// </summary>
    public class MemberCardRequest
    {
        /// <summary>
        /// 卡名称
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// 卡密
        /// </summary>
        public string CardPassword { get; set; }
    }
}