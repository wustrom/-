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
    public class ElecCardIdRequest : UserTokenRequest
    {
        /// <summary>
        /// 获得卡片Id
        /// </summary>
        [Required(ErrorMessage = "卡片ID不能为空", AllowEmptyStrings = false)]
        public string CardId { get; set; }
    }
}