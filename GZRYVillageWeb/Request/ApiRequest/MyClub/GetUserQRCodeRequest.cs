using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 获得用户二维码请求
    /// </summary>
    public class GetUserQRCodeRequest : UserTokenRequest
    {
        /// <summary>
        /// 是否活跃卡
        /// </summary>
        [BoolValid]
        public bool Active { get; set; }
    }
}