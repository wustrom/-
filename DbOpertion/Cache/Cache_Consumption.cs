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
        /// 消费记录
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public List<Consumption> Consuption_List(int UserId, string searchKey, string Key, int Start, int PageSize, DataTablesOrderDir? desc)
        {
            return ConsumptionOper.Instance.SelectPageByUserId(UserId, searchKey, Key, Start, PageSize);
        }

        /// <summary>
        /// 购物记录条数
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public int SelectConsuptionCount(int UserId, string SearchKey, string Key, DataTablesOrderDir? desc)
        {
            bool order = false;
            if (desc == DataTablesOrderDir.Asc)
            {
                order = false;
            }
            else if (desc == DataTablesOrderDir.Desc)
            {
                order = true;
            }
            return ConsumptionOper.Instance.SelectByUserIdCount(UserId, SearchKey, Key, order);
        }
        /// <summary>
        /// 根据电子储值卡Id查找对应用户的消费记录
        /// </summary>
        /// <param name="ElectronicId">电子储值卡Id</param>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">排序主键</param>
        /// <param name="start">开始</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public Tuple<List<ConsumptionInfo>, int, int> SelectConsumptionList(int ElectronicId, string SearchKey, string Key, int start, int PageSize, DataTablesOrderDir desc)
        {
            bool asc;
            if (desc == DataTablesOrderDir.Asc)
            {
                asc = true;
            }
            else
            {
                asc = false;
            }
            var Consumption_List = ConsumptionOper.Instance.SelectConsumptionListById1(ElectronicId, SearchKey, Key, start, PageSize, asc);
            var All_Count = ConsumptionOper.Instance.SelectConsumptionCount1(ElectronicId, null);
            var count = ConsumptionOper.Instance.SelectConsumptionCount1(ElectronicId, SearchKey);
            return new Tuple<List<ConsumptionInfo>, int, int>(Consumption_List, All_Count, count);
        }
    }
}
