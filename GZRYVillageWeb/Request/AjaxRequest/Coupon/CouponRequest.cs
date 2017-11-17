using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest
{
    /// <summary>
    /// 优惠券信息
    /// </summary>
    public class CouponRequest
    {
        /// <summary>
        /// 优惠券Id
        /// </summary>
        public int CouponId { get; set; }
        /// <summary>
        /// 优惠券名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "优惠券名称不能为空")]
        public string CouponName { get; set; }

        /// <summary>
        /// 优惠券描述
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入优惠券描述")]
        public string CouponDescribe { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入有效期")]
        [IntValid(AllowZero = true, ErrorMessage = "有效期请输入数字")]
        public int? ExpirationDay { get; set; }
    }
}