using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.MvcRequest.MemberShipLevel
{
    public class MemberCouponRelationRequest
    {
        /// <summary>
        /// 等级优惠内容Id
        /// </summary>
        public int CouponContainsId { get; set; }
        /// <summary>
        /// 会员等级Id
        /// </summary>
        public int MembershipLevelId { get; set; }
    }
}