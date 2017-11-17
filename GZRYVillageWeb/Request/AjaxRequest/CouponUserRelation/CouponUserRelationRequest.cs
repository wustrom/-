using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest
{
    public class CouponUserRelationRequest
    {
        /// <summary>
        ///用户优惠券关系Id
        /// </summary>
        public Int32 CouponUserRelationId { get; set; }
        /// <summary>
        ///用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        ///优惠券Id
        /// </summary>
        public Int32? CouponId { get; set; }
        /// <summary>
        ///优惠券名称
        /// </summary>
        public String CouponName { get; set; }
        /// <summary>
        ///优惠券描述
        /// </summary>
        public String CouponDescribe { get; set; }
        /// <summary>
        ///激活日期
        /// </summary>
        public DateTime? ReleaseDate { get; set; }
        /// <summary>
        ///有效期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        ///是否使用
        /// </summary>
        public Boolean? IsUsed { get; set; }
        /// <summary>
        ///是否有效
        /// </summary>
        public Boolean? IsDelete { get; set; }
      
        
    }
}