using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Common.Attribute.Constant;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// MemberCard验证请求
    /// </summary>
    public class MemberCardVaildRequest : UserTokenRequest
    {
        /// <summary>
        /// 卡名称
        /// </summary>
        [Required(ErrorMessage = "请输入卡名")]
        public string CardName { get; set; }

        /// <summary>
        /// 卡密
        /// </summary>
        [Required(ErrorMessage = "请输入卡密")]
        public string CardPassword { get; set; }

        /// <summary>
        /// 是否活跃
        /// </summary>
        [BoolValid(ErrorMessage = "请上传是否活跃参数")]
        public bool? Active { get; set; }
    }
}