using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Response.AjaxResponse
{
    /// <summary>
    /// 用户返回
    /// </summary>
    public class CouponInfoResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coupon"></param>
        public CouponInfoResponse(CouponUserRelation coupon)
        {
            //用户ID
            this.CouponUserRelationId = coupon.CouponUserRelationId;
            //优惠券名称
            this.CouponName = coupon.CouponName;
            //优惠券简介
            this.CouponDescribe = coupon.CouponDescribe;
            //过期时间
            this.ExpirationDate = coupon.ExpirationDate == null ? "无" : coupon.ExpirationDate.Value.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 优惠券用户关系ID
        /// </summary>
        public Int32 CouponUserRelationId { get; set; }
        /// <summary>
        /// 优惠券名称
        /// </summary>
        public String CouponName { get; set; }
        /// <summary>
        /// 优惠券简介
        /// </summary>
        public String CouponDescribe { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public string ExpirationDate { get; set; }
    }
}