using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Models
{
   public class CouponUserRelationInfo
    {
        /// <summary>
        ///用户Id
        /// </summary>
        public Int32? UserId { get; set; }
        /// <summary>
        ///优惠券名称
        /// </summary>
        public String CouponName { get; set; }
        /// <summary>
        ///发放日期
        /// </summary>
        public DateTime? ReleaseDate { get; set; }
        /// <summary>
        ///有效期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
        public String UserNickName { get; set; }
        /// <summary>
        /// 优惠券用户关系Id
        /// </summary>
        public Int32? CouponUserRelationId { get; set; }
    }
}
