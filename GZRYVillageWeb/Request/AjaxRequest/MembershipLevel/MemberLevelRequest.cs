using Common.Attribute;
using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest
{
    public class MemberLevelRequest
    {
        /// <summary>
        /// 会员等级Id
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户Id不能为空")]
        public int MembershipLevelId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [UserNameValid(ErrorMessage = "等级名称不能为空")]
        public string LevelName { get; set; }

        /// <summary>
        /// 当前等级所需条件
        /// </summary>
        [IntValid(AllowZero = true, ErrorMessage = "当前等级所需条件请输入数字", NotAllowNegative = true)]
        public int LevelMin { get; set; }

        /// <summary>
        /// 下一等级所需条件
        /// </summary>
        [IntValid(AllowZero = true, ErrorMessage = "下一等级所需条件请输入数字", NotAllowNegative = true)]
        public int LevelMax { get; set; }
        /// <summary>
        /// 是否失效
        /// </summary>
        public bool IsDelete { get; set; }
    }
}