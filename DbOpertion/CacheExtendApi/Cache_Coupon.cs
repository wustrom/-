using Common;
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
    /// 优惠卷缓存
    /// </summary>
    public partial class Cache_Coupon : SingleTon<Cache_Coupon>
    {
        /// <summary>
        /// 根据用户Id筛选优惠券
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public List<CouponUserRelation> SelectByUserId(int UserId)
        {
            return CouponUserRelationOper.Instance.SelectPhoneByUserId(UserId);
        }

        /// <summary>
        /// 根据用户Id筛选优惠券
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public List<CouponUserRelation> SelectPageByUserId(int UserId, int start, int PageSize)
        {
            return CouponUserRelationOper.Instance.SelectPagePhoneByUserId(UserId, start, PageSize);
        }
    }
}
