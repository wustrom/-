using Common;
using DbOpertion.Models;
using DbOpertion.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.CacheExtendApi
{
    public class Cache_Store : SingleTon<Cache_Store>
    {
        /// <summary>
        /// 获取商店列表
        /// </summary>
        /// <returns></returns>
        public List<Store> GetStoreList()
        {
            return StoreOper.Instance.SelectAll(null);
        }
    }
}
