using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class MemberCouponRelation
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 CouponContainsId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? MembershipLevelId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CouponContains { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "CouponContainsId";
        }

    }
}
