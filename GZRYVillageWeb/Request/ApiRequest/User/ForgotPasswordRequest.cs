using Common.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 忘记密码请求
    /// </summary>
    public class ForgotPasswordRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [PhoneValid]
        public string Phone { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [PassWordValid]
        public string PassWord { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不能为空", AllowEmptyStrings = false)]
        public string Code { get; set; }
    }
}