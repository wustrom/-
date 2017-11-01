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
    public partial class CouponCardRelationOper : SingleTon<CouponCardRelationOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<CouponCardRelation> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CouponCardRelation>();
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
        public CouponCardRelation SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CouponCardRelation>();
            query.Where(p => p.CouponCardRelationId == KeyId);
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
        public List<CouponCardRelation> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CouponCardRelation>();
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
            var query = new LambdaQuery<CouponCardRelation>();
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
            var delete = new LambdaDelete<CouponCardRelation>();
            delete.Where(p => p.CouponCardRelationId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="couponcardrelation">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(CouponCardRelation couponcardrelation, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<CouponCardRelation>();
            if (!couponcardrelation.CouponCardRelationId.IsNullOrEmpty())
            {
                update.Where(p => p.CouponCardRelationId == couponcardrelation.CouponCardRelationId);
            }
            if (!couponcardrelation.MemberShipTypeId.IsNullOrEmpty())
            {
                update.Set(p => p.MemberShipTypeId == couponcardrelation.MemberShipTypeId);
            }
            if (!couponcardrelation.CouponId.IsNullOrEmpty())
            {
                update.Set(p => p.CouponId == couponcardrelation.CouponId);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="couponcardrelation">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(CouponCardRelation couponcardrelation, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<CouponCardRelation>();
            if (!couponcardrelation.MemberShipTypeId.IsNullOrEmpty())
            {
                insert.Insert(p => p.MemberShipTypeId == couponcardrelation.MemberShipTypeId);
            }
            if (!couponcardrelation.CouponId.IsNullOrEmpty())
            {
                insert.Insert(p => p.CouponId == couponcardrelation.CouponId);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
