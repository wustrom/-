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
    public partial class CouponOper : SingleTon<CouponOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<Coupon> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Coupon>();
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
        public Coupon SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Coupon>();
            query.Where(p => p.CouponId == KeyId);
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
        public List<Coupon> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Coupon>();
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
            var query = new LambdaQuery<Coupon>();
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
            var delete = new LambdaDelete<Coupon>();
            delete.Where(p => p.CouponId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="coupon">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(Coupon coupon, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<Coupon>();
            if (!coupon.CouponId.IsNullOrEmpty())
            {
                update.Where(p => p.CouponId == coupon.CouponId);
            }
            if (!coupon.CouponName.IsNullOrEmpty())
            {
                update.Set(p => p.CouponName == coupon.CouponName);
            }
            if (!coupon.CouponDescribe.IsNullOrEmpty())
            {
                update.Set(p => p.CouponDescribe == coupon.CouponDescribe);
            }
            if (!coupon.ExpirationDay.IsNullOrEmpty())
            {
                update.Set(p => p.ExpirationDay == coupon.ExpirationDay);
            }
            if (!coupon.IsDelete.IsNullOrEmpty())
            {
                update.Set(p => p.IsDelete == coupon.IsDelete);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="coupon">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(Coupon coupon, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<Coupon>();
            if (!coupon.CouponName.IsNullOrEmpty())
            {
                insert.Insert(p => p.CouponName == coupon.CouponName);
            }
            if (!coupon.CouponDescribe.IsNullOrEmpty())
            {
                insert.Insert(p => p.CouponDescribe == coupon.CouponDescribe);
            }
            if (!coupon.ExpirationDay.IsNullOrEmpty())
            {
                insert.Insert(p => p.ExpirationDay == coupon.ExpirationDay);
            }
            if (!coupon.IsDelete.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsDelete == coupon.IsDelete);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
