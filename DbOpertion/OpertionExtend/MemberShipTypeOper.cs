using Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.LambdaOpertion;
using Common.Extend;
using DbOpertion.Models;
using System.Data;

namespace DbOpertion.Operation
{
    public partial class MemberShipTypeOper : SingleTon<MemberShipTypeOper>
    {
        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<MemberShipType> SelectByPage(string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<MemberShipType>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CardName.Contains(SearchKey));
            }
            if (Key != null && Key != "CardImage")
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryPageList(start, PageSize);
        }
        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public int SelectCount(string SearchKey, string Key, bool desc = true)
        {
            var query = new LambdaQuery<MemberShipType>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CardName.Contains(SearchKey));

            }
            if (Key != null && Key != "CardImage")
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryCount();


        }
        /// <summary>
        /// 筛选重复的会员卡名称
        /// </summary>
        /// <param name="CardName"></param>
        /// <returns></returns>
        public List<MemberShipType> Check_MemCardName(string CardName)
        {
            var query = new LambdaQuery<MemberShipType>();
            query.Where(p => p.CardName == CardName);
            return query.GetQueryList();
        }

        /// <summary>
        /// 根据Id删除多条数据
        /// </summary>
        /// <param name="KeyId">所选对象的Id</param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool DeleteByIds(List<int> KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = new LambdaDelete<MemberShipType>();
            delete.Where(p => p.MemberShipTypeId.ContainsIn(KeyId));
            return delete.GetDeleteResult();
        }

    }
}
