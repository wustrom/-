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
    public partial class ElectronicCardOper : SingleTon<ElectronicCardOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<ElectronicCard> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCard>();
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
        public ElectronicCard SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCard>();
            query.Where(p => p.ElectronicId == KeyId);
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
        public List<ElectronicCard> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCard>();
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
            var query = new LambdaQuery<ElectronicCard>();
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
            var delete = new LambdaDelete<ElectronicCard>();
            delete.Where(p => p.ElectronicId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="electroniccard">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(ElectronicCard electroniccard, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<ElectronicCard>();
            if (!electroniccard.ElectronicId.IsNullOrEmpty())
            {
                update.Where(p => p.ElectronicId == electroniccard.ElectronicId);
            }
            if (!electroniccard.CardPassword.IsNullOrEmpty())
            {
                update.Set(p => p.CardPassword == electroniccard.CardPassword);
            }
            if (!electroniccard.CaerMoney.IsNullOrEmpty())
            {
                update.Set(p => p.CaerMoney == electroniccard.CaerMoney);
            }
            if (!electroniccard.ElectronicTypeId.IsNullOrEmpty())
            {
                update.Set(p => p.ElectronicTypeId == electroniccard.ElectronicTypeId);
            }
            if (!electroniccard.IsUser.IsNullOrEmpty())
            {
                update.Set(p => p.IsUser == electroniccard.IsUser);
            }
            if (!electroniccard.IsDelete.IsNullOrEmpty())
            {
                update.Set(p => p.IsDelete == electroniccard.IsDelete);
            }
            if (!electroniccard.UserId.IsNullOrEmpty())
            {
                update.Set(p => p.UserId == electroniccard.UserId);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="electroniccard">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(ElectronicCard electroniccard, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<ElectronicCard>();
            if (!electroniccard.CardPassword.IsNullOrEmpty())
            {
                insert.Insert(p => p.CardPassword == electroniccard.CardPassword);
            }
            if (!electroniccard.CaerMoney.IsNullOrEmpty())
            {
                insert.Insert(p => p.CaerMoney == electroniccard.CaerMoney);
            }
            if (!electroniccard.ElectronicTypeId.IsNullOrEmpty())
            {
                insert.Insert(p => p.ElectronicTypeId == electroniccard.ElectronicTypeId);
            }
            if (!electroniccard.IsUser.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsUser == electroniccard.IsUser);
            }
            if (!electroniccard.IsDelete.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsDelete == electroniccard.IsDelete);
            }
            if (!electroniccard.UserId.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserId == electroniccard.UserId);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
