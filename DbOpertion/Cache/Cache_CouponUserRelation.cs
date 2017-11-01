using Common;
using Common.Extend;
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
    /// 优惠卷用户关系缓存
    /// </summary>
    public partial class Cache_CouponUserRelation : SingleTon<Cache_CouponUserRelation>
    {
        /// <summary>
        /// 筛选全部
        /// </summary>
        /// <returns></returns>
        public List<CouponUserRelation> SelectAll(int UserId, string SearchKey, string Key, DataTablesOrderDir desc)
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
            return CouponUserRelationOper.Instance.SelectByUserId(UserId, SearchKey, Key, asc);
        }

        /// <summary>
        /// 分页筛选数据
        /// </summary>
        /// <returns></returns>
        public List<CouponUserRelation> SelectByPage(int UserId, string SearchKey, string Key, int start, int pageSize, DataTablesOrderDir desc)
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
            return CouponUserRelationOper.Instance.SelectPageByUserId(UserId, SearchKey, Key, start, pageSize, asc);
        }

        /// <summary>
        /// 筛选后的数据条数
        /// </summary>
        /// <returns></returns>
        public int SelectSearchCount(int UserId, string SearchKey, string Key, DataTablesOrderDir desc)
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
            return CouponUserRelationOper.Instance.SelectSearchCount(UserId, SearchKey, Key, asc);
        }
    }
}
