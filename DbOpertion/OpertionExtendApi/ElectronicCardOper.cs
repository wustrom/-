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
using Common.Helper;

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
        public List<ElectronicCard> SelectAll(IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCard>();
            return query.GetQueryList(connection, transaction);
        }

        /// <summary>
        /// 验证卡片
        /// </summary>
        /// <param name="CardName">卡名</param>
        /// <param name="CardPassword">卡密码</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public ElectronicCard VaildCard(string CardName, string CardPassword, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            CardPassword = MD5Helper.StrToMD5WithKey(CardPassword);
            var query = new LambdaQuery<ElectronicCard>();
            query.Where(p => p.CardName == CardName && p.CardPassword == CardPassword);
            return query.GetQueryList(connection, transaction).FirstOrDefault();
        }

        /// <summary>
        /// 根据卡片Id列表筛选卡片
        /// </summary>
        /// <param name="CardName">卡名</param>
        /// <param name="CardPassword">卡密码</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public ElectronicCard SelectUserCard(List<int> CardList, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCard>();
            query.Where(p => p.ElectronicId.ContainsIn(CardList));
            return query.GetQueryList(connection, transaction).FirstOrDefault();
        }


    }
}
