using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class CouponLevelRelation
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 CouponLevelRelationId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? CouponId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? MembershipLevelId { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "CouponLevelRelationId";
        }

    }
}
