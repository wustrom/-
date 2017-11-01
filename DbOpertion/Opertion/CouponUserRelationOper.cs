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
    public partial class CouponUserRelationOper : SingleTon<CouponUserRelationOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<CouponUserRelation> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CouponUserRelation>();
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
        public CouponUserRelation SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CouponUserRelation>();
            query.Where(p => p.CouponUserRelationId == KeyId);
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
        public List<CouponUserRelation> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CouponUserRelation>();
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
            var query = new LambdaQuery<CouponUserRelation>();
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
            var delete = new LambdaDelete<CouponUserRelation>();
            delete.Where(p => p.CouponUserRelationId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="couponuserrelation">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(CouponUserRelation couponuserrelation, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<CouponUserRelation>();
            if (!couponuserrelation.CouponUserRelationId.IsNullOrEmpty())
            {
                update.Where(p => p.CouponUserRelationId == couponuserrelation.CouponUserRelationId);
            }
            if (!couponuserrelation.UserId.IsNullOrEmpty())
            {
                update.Set(p => p.UserId == couponuserrelation.UserId);
            }
            if (!couponuserrelation.CouponName.IsNullOrEmpty())
            {
                update.Set(p => p.CouponName == couponuserrelation.CouponName);
            }
            if (!couponuserrelation.CouponDescribe.IsNullOrEmpty())
            {
                update.Set(p => p.CouponDescribe == couponuserrelation.CouponDescribe);
            }
            if (!couponuserrelation.ReleaseDate.IsNullOrEmpty())
            {
                update.Set(p => p.ReleaseDate == couponuserrelation.ReleaseDate);
            }
            if (!couponuserrelation.ExpirationDate.IsNullOrEmpty())
            {
                update.Set(p => p.ExpirationDate == couponuserrelation.ExpirationDate);
            }
            if (!couponuserrelation.IsUsed.IsNullOrEmpty())
            {
                update.Set(p => p.IsUsed == couponuserrelation.IsUsed);
            }
            if (!couponuserrelation.IsDelete.IsNullOrEmpty())
            {
                update.Set(p => p.IsDelete == couponuserrelation.IsDelete);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="couponuserrelation">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(CouponUserRelation couponuserrelation, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<CouponUserRelation>();
            if (!couponuserrelation.UserId.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserId == couponuserrelation.UserId);
            }
            if (!couponuserrelation.CouponName.IsNullOrEmpty())
            {
                insert.Insert(p => p.CouponName == couponuserrelation.CouponName);
            }
            if (!couponuserrelation.CouponDescribe.IsNullOrEmpty())
            {
                insert.Insert(p => p.CouponDescribe == couponuserrelation.CouponDescribe);
            }
            if (!couponuserrelation.ReleaseDate.IsNullOrEmpty())
            {
                insert.Insert(p => p.ReleaseDate == couponuserrelation.ReleaseDate);
            }
            if (!couponuserrelation.ExpirationDate.IsNullOrEmpty())
            {
                insert.Insert(p => p.ExpirationDate == couponuserrelation.ExpirationDate);
            }
            if (!couponuserrelation.IsUsed.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsUsed == couponuserrelation.IsUsed);
            }
            if (!couponuserrelation.IsDelete.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsDelete == couponuserrelation.IsDelete);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
