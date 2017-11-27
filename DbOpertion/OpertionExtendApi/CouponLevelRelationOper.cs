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
    public partial class CouponLevelRelationOper : SingleTon<CouponLevelRelationOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<CouponLevelRelation> SelectByLevelId(int LevelID, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CouponLevelRelation>();
            query.Where(p => p.MembershipLevelId == LevelID);
            return query.GetQueryList(connection, transaction);
        }
    }
}
