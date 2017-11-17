using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 电子储值卡请求
    /// </summary>
    public class ElecCardIdRequest : UserTokenRequest
    {
        /// <summary>
        /// 获得卡片Id
        /// </summary>
        public string CardId { get; set; }
    }
}