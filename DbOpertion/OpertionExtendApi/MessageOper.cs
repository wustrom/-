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
        public List<Message> SelectWithOutHtml(IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Message>();
            query.Select(p => new { p.MessageID, p.MessageName, p.MessageImage, p.MessageDescribe });
            return query.GetQueryList(connection, transaction);
        }

    }
}
