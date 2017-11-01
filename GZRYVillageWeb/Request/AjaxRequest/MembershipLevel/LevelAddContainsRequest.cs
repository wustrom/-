using Common.Attribute;
using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest.MembershipLevel
{
    public class LevelAddContainsRequest
    {
        /// <summary>
        /// 会员等级Id
        /// </summary>
        [IntValid(AllowZero = false, ErrorMessage = "会员等级Id不能为空")]
        public int MembershipLevelId { get; set; }

        /// <summary>
        /// 优惠内容
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "优惠内容不能为空")]
        public string CouponContains { get; set; }
    }
}