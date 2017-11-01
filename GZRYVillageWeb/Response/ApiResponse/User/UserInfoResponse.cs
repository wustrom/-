using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Response.ApiResponse
{
    /// <summary>
    /// 用户Id应答
    /// </summary>
    public class UserInfoResponse
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserInfoResponse(TUser user)
        {
            this.UserName = user.UserName;
            this.UserNickName = user.UserNickName;
            this.UserSex = user.Sex.Value;
            this.Phone = user.UserPhone;
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        public bool UserSex { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
    }
}