using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Attribute
{
    /// <summary>
    /// 用户名称验证
    /// </summary>
    public class UserNameValidAttribute : ValidationAttribute
    {

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowEmpty { get; set; }


        public override bool IsValid(object value)
        {
            this.ErrorMessage = "用户名不能为空";
            if (AllowEmpty)
            {
                return false;
            }
            if (value == null)
            {
                return false;
            }
            else
            {
                if (value.ToString().Length > 25)
                {
                    this.ErrorMessage = "用户名不能超过25位";
                    return false;
                }
                //电信手机号码正则        
                string dianxin = @"^1[3578][01379]\d{8}$";
                Regex dReg = new Regex(dianxin);
                //联通手机号正则        
                string liantong = @"^1[34578][01256]\d{8}$";
                Regex tReg = new Regex(liantong);
                //移动手机号正则        
                string yidong = @"^(134[012345678]\d{7}|1[34578][012356789]\d{8})$";
                Regex yReg = new Regex(yidong);
                string Email = @"^(\w)+(\.\w+)*@(\w)+((\.\w{2,3}){1,3})$";
                Regex eReg = new Regex(Email);
                if (dReg.IsMatch(value.ToString()) || tReg.IsMatch(value.ToString()) || yReg.IsMatch(value.ToString()) || eReg.IsMatch(value.ToString()))
                {
                    this.ErrorMessage = "用户名不能为手机号或邮箱号";
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
