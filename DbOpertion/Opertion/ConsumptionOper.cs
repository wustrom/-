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
    public partial class ConsumptionOper : SingleTon<ConsumptionOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<Consumption> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Consumption>();
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
        public Consumption SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Consumption>();
            query.Where(p => p.ConsumptionId == KeyId);
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
        public List<Consumption> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Consumption>();
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
            var query = new LambdaQuery<Consumption>();
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
            var delete = new LambdaDelete<Consumption>();
            delete.Where(p => p.ConsumptionId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="consumption">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(Consumption consumption, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<Consumption>();
            if (!consumption.ConsumptionId.IsNullOrEmpty())
            {
                update.Where(p => p.ConsumptionId == consumption.ConsumptionId);
            }
            if (!consumption.StoreId.IsNullOrEmpty())
            {
                update.Set(p => p.StoreId == consumption.StoreId);
            }
            if (!consumption.ElectronicId.IsNullOrEmpty())
            {
                update.Set(p => p.ElectronicId == consumption.ElectronicId);
            }
            if (!consumption.UserId.IsNullOrEmpty())
            {
                update.Set(p => p.UserId == consumption.UserId);
            }
            if (!consumption.ShopName.IsNullOrEmpty())
            {
                update.Set(p => p.ShopName == consumption.ShopName);
            }
            if (!consumption.ShopMoney.IsNullOrEmpty())
            {
                update.Set(p => p.ShopMoney == consumption.ShopMoney);
            }
            if (!consumption.ShopTime.IsNullOrEmpty())
            {
                update.Set(p => p.ShopTime == consumption.ShopTime);
            }
            if (!consumption.ShopType.IsNullOrEmpty())
            {
                update.Set(p => p.ShopType == consumption.ShopType);
            }
            if (!consumption.IsDelete.IsNullOrEmpty())
            {
                update.Set(p => p.IsDelete == consumption.IsDelete);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="consumption">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(Consumption consumption, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<Consumption>();
            if (!consumption.StoreId.IsNullOrEmpty())
            {
                insert.Insert(p => p.StoreId == consumption.StoreId);
            }
            if (!consumption.ElectronicId.IsNullOrEmpty())
            {
                insert.Insert(p => p.ElectronicId == consumption.ElectronicId);
            }
            if (!consumption.UserId.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserId == consumption.UserId);
            }
            if (!consumption.ShopName.IsNullOrEmpty())
            {
                insert.Insert(p => p.ShopName == consumption.ShopName);
            }
            if (!consumption.ShopMoney.IsNullOrEmpty())
            {
                insert.Insert(p => p.ShopMoney == consumption.ShopMoney);
            }
            if (!consumption.ShopTime.IsNullOrEmpty())
            {
                insert.Insert(p => p.ShopTime == consumption.ShopTime);
            }
            if (!consumption.ShopType.IsNullOrEmpty())
            {
                insert.Insert(p => p.ShopType == consumption.ShopType);
            }
            if (!consumption.IsDelete.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsDelete == consumption.IsDelete);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
