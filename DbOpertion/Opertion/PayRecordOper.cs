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
    public partial class PayRecordOper : SingleTon<PayRecordOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<PayRecord> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<PayRecord>();
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
        public PayRecord SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<PayRecord>();
            query.Where(p => p.PayRecordId == KeyId);
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
        public List<PayRecord> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<PayRecord>();
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
            var query = new LambdaQuery<PayRecord>();
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
            var delete = new LambdaDelete<PayRecord>();
            delete.Where(p => p.PayRecordId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="payrecord">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(PayRecord payrecord, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<PayRecord>();
            if (!payrecord.PayRecordId.IsNullOrEmpty())
            {
                update.Where(p => p.PayRecordId == payrecord.PayRecordId);
            }
            if (!payrecord.StoreID.IsNullOrEmpty())
            {
                update.Set(p => p.StoreID == payrecord.StoreID);
            }
            if (!payrecord.UserId.IsNullOrEmpty())
            {
                update.Set(p => p.UserId == payrecord.UserId);
            }
            if (!payrecord.ShopMoney.IsNullOrEmpty())
            {
                update.Set(p => p.ShopMoney == payrecord.ShopMoney);
            }
            if (!payrecord.ShopTime.IsNullOrEmpty())
            {
                update.Set(p => p.ShopTime == payrecord.ShopTime);
            }
            if (!payrecord.ShopItem.IsNullOrEmpty())
            {
                update.Set(p => p.ShopItem == payrecord.ShopItem);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="payrecord">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(PayRecord payrecord, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<PayRecord>();
            if (!payrecord.StoreID.IsNullOrEmpty())
            {
                insert.Insert(p => p.StoreID == payrecord.StoreID);
            }
            if (!payrecord.UserId.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserId == payrecord.UserId);
            }
            if (!payrecord.ShopMoney.IsNullOrEmpty())
            {
                insert.Insert(p => p.ShopMoney == payrecord.ShopMoney);
            }
            if (!payrecord.ShopTime.IsNullOrEmpty())
            {
                insert.Insert(p => p.ShopTime == payrecord.ShopTime);
            }
            if (!payrecord.ShopItem.IsNullOrEmpty())
            {
                insert.Insert(p => p.ShopItem == payrecord.ShopItem);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
