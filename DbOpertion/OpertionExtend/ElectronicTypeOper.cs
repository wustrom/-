using Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.LambdaOpertion;
using Common.Extend;
using DbOpertion.Models;

namespace DbOpertion.Operation
{
    public partial class ElectronicTypeOper : SingleTon<ElectronicTypeOper>
    {
        /// <summary>
        /// 根据分页筛选电子储值卡数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<ElectronicType> SelectByPage(string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<ElectronicType>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p =>  p.CardMoney.Contains(SearchKey));
            }
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryPageList(start, PageSize);
        }

        /// <summary>
        /// 根据分页筛选电子储值卡数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public int SelectCount(string SearchKey, string Key, bool desc = true)
        {
            var query = new LambdaQuery<ElectronicType>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CardMoney.Contains(SearchKey));

            }
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryCount();


        }
        /// <summary>
        /// 筛选电子储值卡名称重复的数据
        /// </summary>
        /// <param name="CardName"></param>
        /// <returns></returns>
        public List<ElectronicType> SelectElectronicTypeByName(string CardName)
        {
            var query = new LambdaQuery<ElectronicType>();
            return query.GetQueryList();
        }
    }
}
