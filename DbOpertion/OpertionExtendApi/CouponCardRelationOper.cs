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
    public partial class CouponCardRelationOper : SingleTon<CouponCardRelationOper>
    {
        /// <summary>
        /// 根据CardTypeId筛选数据
        /// </summary>
        /// <param name="CardId">主键Id</param>
        /// <returns>是否成功</returns>
        public List<CouponCardRelation> SelectByCardTypeId(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CouponCardRelation>();
            query.Where(p => p.MemberShipTypeId == KeyId);
            return query.GetQueryList(connection, transaction);
        }
    }
}
