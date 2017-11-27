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
using System.Data.SqlClient;

namespace DbOpertion.Operation
{
    public partial class PayRecordOper : SingleTon<PayRecordOper>
    {
        /// <summary>
        /// 筛选用户数据根据Id
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public PayRecord SelectByUserIdAndTime(int? UserId, DateTime? time)
        {
            var query = new LambdaQuery<PayRecord>();
            query.Where(p => p.UserId == UserId && p.ShopTime == time);
            return query.GetQueryList().FirstOrDefault();
        }
    }
}
