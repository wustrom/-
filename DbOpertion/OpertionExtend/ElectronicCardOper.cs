using Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.LambdaOpertion;
using Common.Extend;
using DbOpertion.Models;
using System.Data.SqlClient;
using Common.Helper;
using System.Data;

namespace DbOpertion.Operation
{
    public partial class ElectronicCardOper : SingleTon<ElectronicCardOper>
    {
        public string linq = "linq";
        /// <summary>
        /// 显示电子储值卡列表
        /// </summary>
        /// <param name="ElectronicTypeId">电子储值卡类型Id</param>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">主键</param>
        /// <param name="start">开始数据</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public List<ElectronicCard> Get_ElectronicCardByTypeId(int ElectronicTypeId, string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<ElectronicCard>();
            query.Where(p => p.ElectronicTypeId == ElectronicTypeId);
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CardName.Contains(SearchKey) || p.ElectronicTypeId.Contains(SearchKey) || p.CaerMoney.Contains(SearchKey) || p.CardCreateDate.Contains(SearchKey) || p.CardExpirationDay.Contains(SearchKey));
            }
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryPageList(start, PageSize);
        }

        /// <summary>
        ///  数据条数
        /// </summary>
        /// <param name="ElectronicTypeId">电子储值卡类型Id</param>
        /// <param name="SearchKey">搜索关键字</param>
        /// <returns></returns>
        public int SelectSearchCount(int ElectronicTypeId, string SearchKey)
        {
            var query = new LambdaQuery<ElectronicCard>();
            query.Where(p => p.ElectronicTypeId == ElectronicTypeId);
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CardName.Contains(SearchKey) || p.ElectronicTypeId.Contains(SearchKey) || p.CaerMoney.Contains(SearchKey) || p.CardCreateDate.Contains(SearchKey) || p.CardExpirationDay.Contains(SearchKey));
            }

            return query.GetQueryCount();
        }
        /// <summary>
        /// 添加电子储值卡时筛选重复的卡名
        /// </summary>
        /// <param name="ElectronicTypeId">电子储值卡类型Id</param>
        /// <param name="CardName">卡名</param>
        /// <returns></returns>
        public List<ElectronicCard> SelectElectronicCardByName(string CardName)
        {
            var query = new LambdaQuery<ElectronicCard>();
            query.Where(p => p.CardName == CardName);
            return query.GetQueryList();
        }
        /// <summary>
        /// 根据卡号类型Id和卡Id查询对应的绑定人
        /// </summary>
        /// <param name="ElectronicTypeId">卡类型Id</param>
        /// <param name="ElectronicId">卡Id</param>
        /// <returns></returns>
        public List<ElectronicCardUserInfo> SelectByUserNickName(int ElectronicTypeId, int ElectronicId)
        {
            List<SqlParameter> parmList = new List<SqlParameter>();
            parmList.Add(new SqlParameter("@ElectronicTypeId", ElectronicTypeId));
            parmList.Add(new SqlParameter("@ElectronicId", ElectronicId));
            string sql = string.Format(@"select c.UserNickName,c.UserId,d.ElectronicId,d.ElectronicTypeId from TUser c left join (	select b.UserId,a.ElectronicTypeId,a.ElectronicId
                                         from ElectronicCard a left join ElectronicCardRelation b on a.ElectronicId = b.ElectronicCardId)d 
                                         on c.UserId=d.UserId where ElectronicTypeId =@ElectronicTypeId and ElectronicId=@ElectronicId");
            return SqlOpertion.Instance.GetQueryList<ElectronicCardUserInfo>(sql, parmList);

        }

        /// <summary>
        /// 查询电子储值卡列表数据信息
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<string> SelectExcelByName(IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCard>();
            return query.GetQueryList(connection, transaction).Select(p => p.CardName).ToList();
        }
        /// <summary>
        /// 根据Id删除电子储值卡
        /// </summary>
        /// <param name="ElectronicId">电子储值卡Id</param>
        /// <returns></returns>
        public bool DeleteElectronicCardById(int ElectronicId)
        {
            var delete = new LambdaDelete<ElectronicCard>();
            delete.Where(p => p.ElectronicId == ElectronicId);
            return delete.GetDeleteResult();
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
            var delete = new LambdaDelete<ElectronicCard>();
            delete.Where(p => p.ElectronicId.ContainsIn(KeyId));
            return delete.GetDeleteResult();
        }
    }
}
