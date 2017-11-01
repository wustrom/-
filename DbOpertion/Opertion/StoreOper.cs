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
    public partial class StoreOper : SingleTon<StoreOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<Store> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Store>();
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
        public Store SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Store>();
            query.Where(p => p.StoreId == KeyId);
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
        public List<Store> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Store>();
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
            var query = new LambdaQuery<Store>();
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
            var delete = new LambdaDelete<Store>();
            delete.Where(p => p.StoreId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="store">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(Store store, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<Store>();
            if (!store.StoreId.IsNullOrEmpty())
            {
                update.Where(p => p.StoreId == store.StoreId);
            }
            if (!store.Coord.IsNullOrEmpty())
            {
                update.Set(p => p.Coord == store.Coord);
            }
            if (!store.Adress.IsNullOrEmpty())
            {
                update.Set(p => p.Adress == store.Adress);
            }
            if (!store.Phone.IsNullOrEmpty())
            {
                update.Set(p => p.Phone == store.Phone);
            }
            if (!store.Sales.IsNullOrEmpty())
            {
                update.Set(p => p.Sales == store.Sales);
            }
            if (!store.IsDelete.IsNullOrEmpty())
            {
                update.Set(p => p.IsDelete == store.IsDelete);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="store">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(Store store, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<Store>();
            if (!store.Coord.IsNullOrEmpty())
            {
                insert.Insert(p => p.Coord == store.Coord);
            }
            if (!store.Adress.IsNullOrEmpty())
            {
                insert.Insert(p => p.Adress == store.Adress);
            }
            if (!store.Phone.IsNullOrEmpty())
            {
                insert.Insert(p => p.Phone == store.Phone);
            }
            if (!store.Sales.IsNullOrEmpty())
            {
                insert.Insert(p => p.Sales == store.Sales);
            }
            if (!store.IsDelete.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsDelete == store.IsDelete);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
