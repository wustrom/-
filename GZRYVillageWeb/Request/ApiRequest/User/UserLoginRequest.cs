using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 用户登入请求
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string UserPhone { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
    }
}