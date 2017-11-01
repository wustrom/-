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
    public partial class ActiveOper : SingleTon<ActiveOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<Active> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Active>();
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
        public Active SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Active>();
            query.Where(p => p.ActiveId == KeyId);
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
        public List<Active> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Active>();
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
            var query = new LambdaQuery<Active>();
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
            var delete = new LambdaDelete<Active>();
            delete.Where(p => p.ActiveId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="active">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(Active active, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<Active>();
            if (!active.ActiveId.IsNullOrEmpty())
            {
                update.Where(p => p.ActiveId == active.ActiveId);
            }
            if (!active.ActiveCardId.IsNullOrEmpty())
            {
                update.Set(p => p.ActiveCardId == active.ActiveCardId);
            }
            if (!active.ActiceUserId.IsNullOrEmpty())
            {
                update.Set(p => p.ActiceUserId == active.ActiceUserId);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="active">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(Active active, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<Active>();
            if (!active.ActiveCardId.IsNullOrEmpty())
            {
                insert.Insert(p => p.ActiveCardId == active.ActiveCardId);
            }
            if (!active.ActiceUserId.IsNullOrEmpty())
            {
                insert.Insert(p => p.ActiceUserId == active.ActiceUserId);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
