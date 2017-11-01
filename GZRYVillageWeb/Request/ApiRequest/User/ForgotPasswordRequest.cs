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
        [PassWordValid(ErrorMessage = "密码少于6位或者出现特异字符必须数字和字母混合")]
        public string PassWord { get; set; }
    }
}