using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// MemberCard验证
    /// </summary>
    public class SetActiveCardRequest : UserTokenRequest
    {
        /// <summary>
        /// 卡片ID
        /// </summary>
        public int CardID { get; set; }
    }
}