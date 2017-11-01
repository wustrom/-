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
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<MemberCouponRelation> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberCouponRelation>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryList(connection, transaction);
        }


        /// <summary>
        /// 根据主键筛选数据
        /// </summary>
        /// <param name="KeyId">主键Id</param>
        /// <returns>是否成功</returns>
        public MemberCouponRelation SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberCouponRelation>();
            query.Where(p => p.CouponContainsId == KeyId);
            return query.GetQueryList(connection, transaction).FirstOrDefault();
        }

        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<MemberCouponRelation> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberCouponRelation>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryPageList(start, PageSize, connection, transaction);
        }

        /// <summary>
        /// 数据条数
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public int SelectCount(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberCouponRelation>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryCount(connection, transaction);
        }


        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <param name="KeyId">主键Id</param>
        /// <returns>是否成功</returns>
        public bool DeleteById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = new LambdaDelete<MemberCouponRelation>();
            delete.Where(p => p.CouponContainsId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="membercouponrelation">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(MemberCouponRelation membercouponrelation, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<MemberCouponRelation>();
            if (!membercouponrelation.CouponContainsId.IsNullOrEmpty())
            {
                update.Where(p => p.CouponContainsId == membercouponrelation.CouponContainsId);
            }
            if (!membercouponrelation.MembershipLevelId.IsNullOrEmpty())
            {
                update.Set(p => p.MembershipLevelId == membercouponrelation.MembershipLevelId);
            }
            if (!membercouponrelation.CouponContains.IsNullOrEmpty())
            {
                update.Set(p => p.CouponContains == membercouponrelation.CouponContains);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="membercouponrelation">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(MemberCouponRelation membercouponrelation, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<MemberCouponRelation>();
            if (!membercouponrelation.MembershipLevelId.IsNullOrEmpty())
            {
                insert.Insert(p => p.MembershipLevelId == membercouponrelation.MembershipLevelId);
            }
            if (!membercouponrelation.CouponContains.IsNullOrEmpty())
            {
                insert.Insert(p => p.CouponContains == membercouponrelation.CouponContains);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
