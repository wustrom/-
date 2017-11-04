using Common.Attribute;
using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 邮箱注册请求
    /// </summary>
    public class MailRegisterRequest
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
        /// 用户密码
        /// </summary>
        [PassWordValid]
        public string PassWord { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [UserNameValid]
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Required(ErrorMessage = "用户昵称不能为空", AllowEmptyStrings = false)]
        public string UserNickName { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        [Required(ErrorMessage = "用户性别不能为空", AllowEmptyStrings = false)]
        public bool UserSex { get; set; }

    }
}