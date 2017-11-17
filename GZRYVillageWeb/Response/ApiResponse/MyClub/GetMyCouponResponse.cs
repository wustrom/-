using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Response.ApiResponse
{
    /// <summary>
    /// 获得电子储值卡列表请求
    /// </summary>
    public class GetMyCouponResponse
    {
        /// <summary>
        /// 获得电子储值卡列表请求
        /// </summary>
        public GetMyCouponResponse(CouponUserRelation relation)
        {
            //优惠券名称
            this.CouponName = relation.CouponName;
            //优惠券简述
            this.CouponDescribe = relation.CouponDescribe;
            //到期时间
            this.ExpirationDate = relation.ExpirationDate == null ?null:relation.ExpirationDate.Value.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 优惠券简述
        /// </summary>
        public String CouponDescribe { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        public string ExpirationDate { get; set; }
    }
}