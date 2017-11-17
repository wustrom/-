using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 获得用户卡片列表请求
    /// </summary>
    public class GetMyCardListRequest : UserTokenRequest
    {
        /// <summary>
        /// 是否活跃卡
        /// </summary>
        [BoolValid]
        public bool Active { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        public string Host { get; set; }
    }
}