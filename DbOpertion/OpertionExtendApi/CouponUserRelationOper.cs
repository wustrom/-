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
        /// 筛选后的数据条数
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="SearchKey">搜索主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<CouponUserRelation> SelectPhoneByUserId(int UserId)
        {
            var query = new LambdaQuery<CouponUserRelation>();
            query.Where(p => p.UserId == UserId && p.ExpirationDate > DateTime.Now && (p.IsUsed == null || p.IsUsed == false));
            return query.GetQueryList();
        }

        /// <summary>
        /// 根据用户ID分页筛选数据条数
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="SearchKey">搜索主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<CouponUserRelation> SelectPagePhoneByUserId(int UserId, int start, int PageSize)
        {
            var query = new LambdaQuery<CouponUserRelation>();
            query.Where(p => p.UserId == UserId && p.ExpirationDate > DateTime.Now && (p.IsUsed == null || p.IsUsed == false));
            return query.GetQueryPageList(start, PageSize);
        }
    }
}
