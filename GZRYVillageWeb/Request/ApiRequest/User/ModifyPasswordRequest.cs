using Common.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 修改密码请求
    /// </summary>
    public class ModifyPasswordRequest : UserTokenRequest
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        [PassWordValid]
        public string NewPassword { get; set; }

    }
}