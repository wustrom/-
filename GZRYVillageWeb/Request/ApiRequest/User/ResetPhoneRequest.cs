using Common.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 重置密码请求
    /// </summary>
    public class ResetPhoneRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [PhoneValid]
        public string Phone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不能为空", AllowEmptyStrings = false)]
        public string Code { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [PhoneValid]
        public string NewPhone { get; set; }
    }
}