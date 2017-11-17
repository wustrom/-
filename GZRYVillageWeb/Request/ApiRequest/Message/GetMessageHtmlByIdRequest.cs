using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest.Message
{
    /// <summary>
    /// 根据Id获取信息Html请求
    /// </summary>
    public class GetMessageHtmlByIdRequest
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public int MessageId { get; set; }
    }
}