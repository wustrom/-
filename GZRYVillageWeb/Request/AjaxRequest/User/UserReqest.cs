using Common.Attribute;
using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest
{
    /// <summary>
    /// 用户请求
    /// </summary>
    public class UserReqest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户Id不能为空")]
        public int UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [UserNameValid(ErrorMessage = "用户名称不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户昵称不能为空")]
        public string UserNickName { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        [PhoneValid]
        public string UserPhone { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        [EmailValid]
        public string UserEmail { get; set; }

        /// <summary>
        /// 用户图片
        /// </summary>
        public string UserImage { get; set; }

        /// <summary>
        /// 消费次数
        /// </summary>
        [IntValid(AllowZero = true, ErrorMessage = "消费次数请输入数字", NotAllowNegative = true)]
        public int ConsumptionTime { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        [Required(AllowEmptyStrings = true, ErrorMessage = "请选择性别")]
        public bool Sex { get; set; }
    }
}