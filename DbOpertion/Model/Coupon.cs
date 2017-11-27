using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class Coupon
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 CouponId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CouponName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CouponDetails { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CouponDescribe { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? ExpirationDay { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? IsDelete { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? Forever { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "CouponId";
        }

    }
}
