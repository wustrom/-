using Common.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 绑定电子储存卡请求
    /// </summary>
    public class BindElecCardRequest : UserTokenRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required(ErrorMessage = "卡名不能为空", AllowEmptyStrings = false)]
        public string CardName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空", AllowEmptyStrings = false)]
        public string PassWord { get; set; }
    }
}