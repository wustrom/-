using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class CouponUserRelation
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 CouponUserRelationId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? UserId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? CouponId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CouponName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CouponDescribe { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? ReleaseDate { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? IsUsed { get; set; }
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
            return "CouponUserRelationId";
        }

    }
}
