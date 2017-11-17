using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 电子储值卡请求
    /// </summary>
    public class ElecRequest : UserTokenRequest
    {
        /// <summary>
        /// 卡片Id
        /// </summary>
        [IntValid(ErrorMessage = "卡片Id不能为空")]
        public int? CardId { get; set; }
    }
}