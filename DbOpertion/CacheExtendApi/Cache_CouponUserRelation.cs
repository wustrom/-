using Common;
using Common.Extend;
using Common.Helper;
using Common.Result;
using DbOpertion.Models;
using DbOpertion.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Cache
{
    /// <summary>
    /// 优惠卷用户关系缓存
    /// </summary>
    public partial class Cache_CouponUserRelation : SingleTon<Cache_CouponUserRelation>
    {
        /// <summary>
        /// 根据用户Id筛选优惠券
        /// </summary>
        /// <returns></returns>
        public Coupon SelectCoupon(int UserRelation)
        {
            var model = CouponUserRelationOper.Instance.SelectById(UserRelation);
            if (model != null && model.CouponId != null)
            {
                var coupon = CouponOper.Instance.SelectById(model.CouponId.Value).FirstOrDefault();
                return coupon;
            }
            return null;
        }


    }
}
