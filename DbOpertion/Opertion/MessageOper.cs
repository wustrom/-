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
    public partial class MessageOper : SingleTon<MessageOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<Message> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Message>();
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
        public Message SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Message>();
            query.Where(p => p.MessageID == KeyId);
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
        public List<Message> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Message>();
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
            var query = new LambdaQuery<Message>();
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
            var delete = new LambdaDelete<Message>();
            delete.Where(p => p.MessageID == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="message">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(Message message, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<Message>();
            if (!message.MessageID.IsNullOrEmpty())
            {
                update.Where(p => p.MessageID == message.MessageID);
            }
            if (!message.MessageName.IsNullOrEmpty())
            {
                update.Set(p => p.MessageName == message.MessageName);
            }
            if (!message.MessageContains.IsNullOrEmpty())
            {
                update.Set(p => p.MessageContains == message.MessageContains);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="message">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(Message message, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<Message>();
            if (!message.MessageName.IsNullOrEmpty())
            {
                insert.Insert(p => p.MessageName == message.MessageName);
            }
            if (!message.MessageContains.IsNullOrEmpty())
            {
                insert.Insert(p => p.MessageContains == message.MessageContains);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
