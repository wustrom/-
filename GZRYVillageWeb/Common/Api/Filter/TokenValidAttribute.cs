using Common.Enum_My;
using Common.Result;
using DbOpertion.Cache;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Common.Api.Filter
{
    /// <summary>
    /// 用户Token验证特性
    /// </summary>
    public class TokenValidAttribute : ValidationAttribute
    {
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowEmpty { get; set; }

        /// <summary>
        /// 用户Token验证
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            this.ErrorMessage = Enum_Message.TokenInvalidMessage.Enum_GetString();
            if (AllowEmpty == true)
            {
                return true;
            }
            if (value != null)
            {
                Token token = Cache_TUser.Instance.GetUserToken(value.ToString());
                if (token != null)
                {
                    var usermodel = Cache_TUser.Instance.GetUserInfo(token);
                    if (usermodel == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}