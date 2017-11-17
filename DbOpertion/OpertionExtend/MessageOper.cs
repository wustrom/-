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
        /// 查询消息列表
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">主键</param>
        /// <param name="start">开始条数</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public List<Message> SelectByPage(string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<Message>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.MessageName.Contains(SearchKey) || p.MessageContains.Contains(SearchKey));
            }
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryPageList(start, PageSize);
        }
        /// <summary>
        /// 根据分页筛选消息数据
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <returns></returns>
        public int SelectMessageCount(string SearchKey)
        {
            var query = new LambdaQuery<Message>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.MessageName.Contains(SearchKey) || p.MessageContains.Contains(SearchKey));
            }
            return query.GetQueryCount();
        }
        /// <summary>
        /// 筛选重复的消息
        /// </summary>
        /// <returns></returns>
        public List<Message> SelectrMessageByName(string MessageName, string MessageContains)
        {
            var qurey = new LambdaQuery<Message>();
            qurey.Where(p => p.MessageName == MessageName && p.MessageContains == MessageContains);
            return qurey.GetQueryList();
        }

        /// <summary>
        /// 更新消息内容
        /// </summary>
        /// <param name="message"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Update_Message(Message message, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<Message>();
            if (!message.MessageID.IsNullOrEmpty())
            {
                update.Where(p => p.MessageID == message.MessageID);
            }
            if (!message.MessageContains.IsNullOrEmpty())
            {
                update.Set(p => p.MessageContains == message.MessageContains);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据Id查询相应的信息
        /// </summary>
        /// <param name="MessageID">消息Id</param>
        /// <returns></returns>
        public List<Message> SelectById(int MessageID)
        {
            var query = new LambdaQuery<Message>();
            query.Where(p => p.MessageID == MessageID);
            return query.GetQueryList();
        }

        /// <summary>
        /// 根据Id多选删除数据
        /// </summary>
        /// <param name="KeyId"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool DeleteByIds(List<int> KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = new LambdaDelete<Message>();
            delete.Where(p => p.MessageID.ContainsIn(KeyId));
            return delete.GetDeleteResult();
        }
    }
}
