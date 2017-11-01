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
    public class UserIdResponse
    {
        /// <summary>
        /// 用户ID应答
        /// </summary>
        /// <param name="user">用户模型</param>
        public UserIdResponse(TUser user)
        {
            this.UserId = user.UserId;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
    }
}