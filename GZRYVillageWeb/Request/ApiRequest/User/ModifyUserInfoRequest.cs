using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 修改用户信息请求
    /// </summary>
    public class ModifyUserInfoRequest : UserTokenRequest
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        [Required(ErrorMessage = "用户昵称不能为空", AllowEmptyStrings = false)]
        public string UserNickName { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        [BoolValid(ErrorMessage = "用户昵称不能为空")]
        public bool UserSex { get; set; }
    }
}