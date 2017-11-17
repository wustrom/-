using Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.LambdaOpertion;
using Common.Extend;
using DbOpertion.Models;

namespace DbOpertion.Operation
{
    public partial class CouponUserRelationOper : SingleTon<CouponUserRelationOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<CouponUserRelation> SelectByUserId(int UserId, string SearchKey, string Key, bool desc = true)
        {
            var query = new LambdaQuery<CouponUserRelation>();
            query.Where(p => p.UserId == UserId);
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CouponDescribe.Contains(SearchKey) || p.CouponName.Contains(SearchKey) || p.ExpirationDate.Contains(SearchKey));
            }
            return query.GetQueryList();
        }

        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="SearchKey">搜索主键</param>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<CouponUserRelation> SelectPageByUserId(int UserId, string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<CouponUserRelation>();
            query.Where(p => p.UserId == UserId);
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CouponDescribe.Contains(SearchKey) || p.CouponName.Contains(SearchKey) || p.ExpirationDate.Contains(SearchKey));
            }
            return query.GetQueryPageList(start, PageSize);
        }

        /// <summary>
        /// 筛选后的数据条数
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="SearchKey">搜索主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public int SelectSearchCount(int UserId, string SearchKey, string Key, bool desc = true)
        {
            var query = new LambdaQuery<CouponUserRelation>();
            query.Where(p => p.UserId == UserId);
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CouponDescribe.Contains(SearchKey) || p.CouponName.Contains(SearchKey) || p.ExpirationDate.Contains(SearchKey));
            }
            return query.GetQueryCount();
        }

    }
}
