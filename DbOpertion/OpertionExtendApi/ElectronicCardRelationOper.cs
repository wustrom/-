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
    public partial class ElectronicCardRelationOper : SingleTon<ElectronicCardRelationOper>
    {
        /// <summary>
        /// 根据用户Id进行筛选
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns>对象列表</returns>
        public List<ElectronicCardRelation> SelectByUserId(int UserID, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCardRelation>();
            query.Where(p => p.UserId == UserID);
            return query.GetQueryList(connection, transaction);
        }

        /// <summary>
        /// 根据用户Id与电子储存卡ID查询有无对应关系
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="ElectronicCardId">电子储存卡ID</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns>对象列表</returns>
        public ElectronicCardRelation SelectByUserIdAndElectronicId(int UserID, int ElectronicCardId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCardRelation>();
            query.Where(p => p.UserId == UserID && p.ElectronicCardId == ElectronicCardId);
            return query.GetQueryList(connection, transaction).FirstOrDefault();
        }

        /// <summary>
        /// 根据用户Id与电子储存卡ID建立关系
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="ElectronicCardId">电子储存卡ID</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns>是否成功</returns>
        public bool SetUserRelation(int UserID, int ElectronicCardId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            
            var date = DateTime.Now;
            var insert = new LambdaInsert<ElectronicCardRelation>();
            insert.Insert(p => p.UserId == UserID && p.ElectronicCardId == ElectronicCardId && p.CreatTime == date);
            return insert.GetInsertResult(connection, transaction);
        }
    }
}
