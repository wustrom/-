using GZRYVillageWeb.Common.Api.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 用户登入请求
    /// </summary>
    public class TokenAndPageRequest : UserTokenRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo { get; set; }
    }
}