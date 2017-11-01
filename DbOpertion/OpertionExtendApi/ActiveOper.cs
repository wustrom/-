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
        /// 根据用户Id筛选数据
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public Active SelectByUserId(int UserId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Active>();
            query.Where(p => p.ActiceUserId == UserId);
            return query.GetQueryList(connection, transaction).FirstOrDefault();
        }

        /// <summary>
        /// 根据CardId筛选数据
        /// </summary>
        /// <param name="CardId">卡片ID</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public Active SelectByCardId(int CardId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Active>();
            query.Where(p => p.ActiveCardId == CardId);
            return query.GetQueryList(connection, transaction).FirstOrDefault();
        }

        /// <summary>
        /// 根据用户ID更新活跃卡
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="CardId">卡片ID</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public bool UpdateCardByUserId(int UserId, int CardId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Active>();
            query.Where(p => p.ActiceUserId == UserId);
            var active = query.GetQueryList(connection, transaction).FirstOrDefault();
            bool result = false;
            if (active == null)
            {
                var Insert = new LambdaInsert<Active>();
                Insert.Insert(p => p.ActiceUserId == UserId && p.ActiveCardId == CardId);
                result = Insert.GetInsertResult(connection, transaction);
            }
            else
            {
                var Update = new LambdaUpdate<Active>();
                Update.Where(p => p.ActiceUserId == UserId);
                Update.Set(p => p.ActiveCardId == CardId);
                result = Update.GetUpdateResult(connection, transaction);
            }
            return result;
        }


    }
}
