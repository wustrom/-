using Common;
using Common.Result;
using DbOpertion.Models;
using DbOpertion.Operation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DbOpertion.Cache
{
    public partial class Cache_CouponLevelRelation : SingleTon<Cache_CouponLevelRelation>
    {
        /// <summary>
        /// 设立用户优惠券
        /// </summary>
        /// <param name="LevelID">等级ID</param>
        /// <param name="UserId">用户ID</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>

        public bool UpdateUserCoupon(int LevelID, int UserId, IDbConnection connection, IDbTransaction transaction)
        {
            var levelList = CouponLevelRelationOper.Instance.SelectByLevelId(LevelID, connection, transaction);
            foreach (var item in levelList)
            {
                var coupon = CouponOper.Instance.SelectById(item.CouponId.Value, connection, transaction);
                CouponUserRelation userRelation = new CouponUserRelation();
                userRelation.CouponId = coupon.CouponId;
                userRelation.CouponDescribe = coupon.CouponDescribe;
                userRelation.CouponName = coupon.CouponName;
                userRelation.Forever = coupon.Forever;
                userRelation.ExpirationDate = DateTime.Now.AddDays(coupon.ExpirationDay.Value).Date.AddHours(23).AddMinutes(59).AddMilliseconds(59);
                userRelation.ReleaseDate = DateTime.Now;
                userRelation.UserId = UserId;
                if (!CouponUserRelationOper.Instance.Insert(userRelation, connection, transaction))
                {
                    return false;
                }
            }
            return true;

        }
    }
}
