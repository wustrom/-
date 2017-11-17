using Common;
using Common.Result;
using DbOpertion.Models;
using DbOpertion.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Cache
{
    /// <summary>
    /// 购物记录缓存
    /// </summary>
    public partial class Cache_Consumption : SingleTon<Cache_Consumption>
    {
        /// <summary>
        /// 根据用户Id查找消费记录
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public Tuple<List<PayRecord>, List<Store>> SelectConsumptionPageByUserId(int UserId, int start, int PageSize)
        {
            return ConsumptionOper.Instance.SelectPageByUserId(UserId, start, PageSize);
        }

        /// <summary>
        /// 根据用户Id查找最后消费记录
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public Tuple<PayRecord, Store> SelcctLastConsumptionByUserId(int UserId)
        {
            return ConsumptionOper.Instance.SelectByUserIdLast(UserId);
        }


        /// <summary>
        /// 根据用户Id查找消费记录
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public List<PayRecordInfo> SelcctConsumptionByUserId(int CardId)
        {
            return ConsumptionOper.Instance.SelectByUserAndCardId(CardId);
        }

    }
}
