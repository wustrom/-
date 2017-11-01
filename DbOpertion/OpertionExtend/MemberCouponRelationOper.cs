using Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.LambdaOpertion;
using Common.Extend;
using System.Data;
using DbOpertion.Models;

namespace DbOpertion.Operation
{
    public partial class MemberCouponRelationOper : SingleTon<MemberCouponRelationOper>
    {
        /// <summary>
        /// 根据会员卡等级Id查询对应的优惠内容
        /// </summary>
        /// <param name="MembershipLevelId">会员等级Id</param>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">排序主键</param>
        /// <param name="start">开始条数</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public List<MemberCouponRelation> SelectMemberCouponRelationList(int MembershipLevelId, string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<MemberCouponRelation>();
            query.Where(p => p.MembershipLevelId == MembershipLevelId);
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CouponContains.Contains(SearchKey) || p.MembershipLevelId.Contains(SearchKey));
            }
            return query.GetQueryPageList(start, PageSize);

        }
        /// <summary>
        /// 根据分页筛选优惠内容的数目
        /// </summary>
        /// <param name="MembershipLevelId"></param>
        /// <param name="SearchKey"></param>
        /// <returns></returns>
        public int SelectMemCouponRelationListCount(int MembershipLevelId, string SearchKey)
        {
            var query = new LambdaQuery<MemberCouponRelation>();
            query.Where(p => p.MembershipLevelId == MembershipLevelId);
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CouponContains.Contains(SearchKey) || p.MembershipLevelId.Contains(SearchKey));
            }
            return query.GetQueryCount();
        }
        /// <summary>
        /// 根据优惠内容Id查询等级对应的优惠内容
        /// </summary>
        /// <param name="MembershipLevelId"></param>
        /// <returns></returns>
        public List<MemberCouponRelation> SelectById(int CouponContainsId)
        {
            var query = new LambdaQuery<MemberCouponRelation>();
            query.Where(p => p.CouponContainsId == CouponContainsId);
            return query.GetQueryList();
        }
        /// <summary>
        /// 根据优惠信息Id删除对应的优惠信息
        /// </summary>
        /// <param name="CouponContainsId"></param>
        /// <returns></returns>
        public bool Delete_MemberCouponRelatioById(int CouponContainsId)
        {
            var delete = new LambdaDelete<MemberCouponRelation>();
            delete.Where(p => p.CouponContainsId == CouponContainsId);
            return delete.GetDeleteResult();

        }
        /// <summary>
        /// 筛选同一等级下重复的优惠内容
        /// </summary>
        /// <param name="CouponContains"></param>
        /// <returns></returns>
        public List<MemberCouponRelation> Chenk_MemberCouponRelation(string CouponContains)
        {
            var query = new LambdaQuery<MemberCouponRelation>();
            query.Where(p => p.CouponContains == CouponContains);
            return query.GetQueryList();
        }
    }
}
