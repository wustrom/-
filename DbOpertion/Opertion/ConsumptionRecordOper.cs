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
    public partial class ConsumptionRecordOper : SingleTon<ConsumptionRecordOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<ConsumptionRecord> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ConsumptionRecord>();
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
        public ConsumptionRecord SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ConsumptionRecord>();
            query.Where(p => p.ConsumptionRecordId == KeyId);
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
        public List<ConsumptionRecord> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ConsumptionRecord>();
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
            var query = new LambdaQuery<ConsumptionRecord>();
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
            var delete = new LambdaDelete<ConsumptionRecord>();
            delete.Where(p => p.ConsumptionRecordId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="consumptionrecord">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(ConsumptionRecord consumptionrecord, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<ConsumptionRecord>();
            if (!consumptionrecord.ConsumptionRecordId.IsNullOrEmpty())
            {
                update.Where(p => p.ConsumptionRecordId == consumptionrecord.ConsumptionRecordId);
            }
            if (!consumptionrecord.ElectronicId.IsNullOrEmpty())
            {
                update.Set(p => p.ElectronicId == consumptionrecord.ElectronicId);
            }
            if (!consumptionrecord.PayRecordId.IsNullOrEmpty())
            {
                update.Set(p => p.PayRecordId == consumptionrecord.PayRecordId);
            }
            if (!consumptionrecord.PayMoney.IsNullOrEmpty())
            {
                update.Set(p => p.PayMoney == consumptionrecord.PayMoney);
            }
            if (!consumptionrecord.ShopType.IsNullOrEmpty())
            {
                update.Set(p => p.ShopType == consumptionrecord.ShopType);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="consumptionrecord">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(ConsumptionRecord consumptionrecord, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<ConsumptionRecord>();
            if (!consumptionrecord.ElectronicId.IsNullOrEmpty())
            {
                insert.Insert(p => p.ElectronicId == consumptionrecord.ElectronicId);
            }
            if (!consumptionrecord.PayRecordId.IsNullOrEmpty())
            {
                insert.Insert(p => p.PayRecordId == consumptionrecord.PayRecordId);
            }
            if (!consumptionrecord.PayMoney.IsNullOrEmpty())
            {
                insert.Insert(p => p.PayMoney == consumptionrecord.PayMoney);
            }
            if (!consumptionrecord.ShopType.IsNullOrEmpty())
            {
                insert.Insert(p => p.ShopType == consumptionrecord.ShopType);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
