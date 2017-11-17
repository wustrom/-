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
        public List<PayRecord> SelectByUserId(int UserId, string Key, bool desc = true)
        {
            var query = new LambdaQuery<PayRecord>();
            query.Where(p => p.UserId == UserId);
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryList();
        }

        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<PayRecord> SelectPageByUserId(int UserId, string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<PayRecord>();
            query.Where(p => p.UserId == UserId);
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.ShopItem.Contains(SearchKey));
            }
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryPageList(start, PageSize);
        }
        ///<summary>
        /// 根据分页筛选数据
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">排序主键</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public int SelectByUserIdCount(int UserId, string SearchKey, string Key, bool desc = true)
        {
            var query = new LambdaQuery<PayRecord>();
            query.Where(p => p.UserId == UserId);
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.ShopItem.Contains(SearchKey));
            }
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryCount();
        }

        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="Key">排序主键</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public int SelectByUserIdCount(int UserId, string Key, bool desc = true)
        {
            var query = new LambdaQuery<PayRecord>();
            query.Where(p => p.UserId == UserId);
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryCount();
        }

        /// <summary>
        /// 根据电子储值卡Id查找对应的会员的消费记录
        /// </summary>
        /// <param name="ElectronicId">电子储值卡Id</param>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">排序主键</param>
        /// <param name="start">开始条数</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public List<PayRecordInfo> SelectConsumptionListById1(int ElectronicId, string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            List<SqlParameter> parmList = new List<SqlParameter>();
            string SqlWhereLike = null;
            if (!SearchKey.IsNullOrEmpty())
            {
                parmList.Add(new SqlParameter("@UserNickName", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@ShopItem", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@PayMoney", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@ShopTime", "%" + SearchKey + "%"));
                SqlWhereLike = @" and (UserNickName like @UserNickName or
                                         ShopItem like @ShopItem or PayMoney like @PayMoney or
                                        ShopTime like @ShopTime)";
            }
            parmList.Add(new SqlParameter("@ElectronicId", ElectronicId));
            string sql = string.Format(@"SELECT a.ConsumptionRecordId,b.UserId,b.ShopItem,a.PayMoney,a.ElectronicId,b.ShopTime,c.UserNickName
                                         FROM ConsumptionRecord a LEFT JOIN  PayRecord b on a.PayRecordId=b.PayRecordId
                                        LEFT JOIN TUser c ON b.UserId = c.UserId WHERE ElectronicId=@ElectronicId" + SqlWhereLike);
            return SqlOpertion.Instance.GetQueryPage<PayRecordInfo>(sql, parmList, Key, desc, start, PageSize);
        }

        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        /// <param name="ElectronicId">电子储值卡Id</param>
        /// <param name="SearchKey">搜索关键字</param>
        /// <returns></returns>
        public int SelectConsumptionCount1(int ElectronicId, string SearchKey)
        {
            List<SqlParameter> parmList = new List<SqlParameter>();
            string SqlWhereLike = null;
            if (!SearchKey.IsNullOrEmpty())
            {
                parmList.Add(new SqlParameter("@UserNickName", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@ShopItem", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@PayMoney", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@ShopTime", "%" + SearchKey + "%"));
                SqlWhereLike = @" and (UserNickName like @UserNickName or
                                         ShopItem like @ShopItem or PayMoney like @PayMoney or
                                        ShopTime like @ShopTime)";
            }
            parmList.Add(new SqlParameter("@ElectronicId", ElectronicId));
            string sql = string.Format(@"SELECT a.ConsumptionRecordId,b.UserId,b.ShopItem,a.PayMoney,a.ElectronicId,b.ShopTime,c.UserNickName
                                         FROM ConsumptionRecord a LEFT JOIN  PayRecord b on a.PayRecordId=b.PayRecordId
                                        LEFT JOIN TUser c ON b.UserId = c.UserId WHERE ElectronicId=@ElectronicId" + SqlWhereLike);
            return SqlOpertion.Instance.GetQueryCount(sql, parmList);
        }
    }
}
