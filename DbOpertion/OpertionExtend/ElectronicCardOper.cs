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
        /// 根据电子储值卡的类型ID查询对应的卡片信息
        /// </summary>
        /// <param name="card">电子储值卡实体类</param>
        /// <param name="user">用户实体类</param>
        /// <returns></returns>
        public List<ElectronicCardInfo> Get_ElectronicCardByTypeId(int ElectronicTypeId, string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            List<SqlParameter> parmList = new List<SqlParameter>();
            string SqlWhereLike = null;
            if (!SearchKey.IsNullOrEmpty())
            {
                parmList.Add(new SqlParameter("@CaerMoney", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@UserNickName", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@UserPhone", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@UserEmail", "%" + SearchKey + "%"));
                SqlWhereLike = @" and (CaerMoney like @CaerMoney or
                                         UserNickName like @UserNickName or UserPhone like @UserPhone or
                                         UserEmail like @UserEmail)";
            }
            parmList.Add(new SqlParameter("@ElectronicTypeId", ElectronicTypeId));
            string sql = string.Format(@"select b.UserId,a.CaerMoney,a.ElectronicTypeId,a.ElectronicId,a.IsUser,a.IsDelete,b.UserNickName from ElectronicCard a 
                                         left join AUser b on a.UserId=b.UserId
                                         where ElectronicTypeId=@ElectronicTypeId " + SqlWhereLike);
            return SqlOpertion.Instance.GetQueryPage<ElectronicCardInfo>(sql, parmList, Key, desc, start, PageSize);
        }

        /// <summary>
        /// 数据条数
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public int SelectSearchCount(int ElectronicTypeId, string SearchKey)
        {
            List<SqlParameter> parmList = new List<SqlParameter>();
            string SqlWhereLike = null;
            if (!SearchKey.IsNullOrEmpty())
            {
                parmList.Add(new SqlParameter("@CaerMoney", "'%" + SearchKey + "%'"));
                parmList.Add(new SqlParameter("@UserNickName", "'%" + SearchKey + "%'"));
                parmList.Add(new SqlParameter("@UserPhone", "'%" + SearchKey + "%'"));
                parmList.Add(new SqlParameter("@UserEmail", "'%" + SearchKey + "%'"));
                SqlWhereLike = @" and (CaerMoney like @CaerMoney or
                                         UserNickName like @UserNickName or UserPhone like @UserPhone or
                                         UserEmail like @UserEmail)";
            }
            parmList.Add(new SqlParameter("@ElectronicTypeId", ElectronicTypeId));
            string sql = string.Format(@"select a.UserId,a.CaerMoney,a.ElectronicTypeId,a.ElectronicId,a.IsUser,a.IsDelete from ElectronicCard a 
                                         left join AUser b on a.UserId=b.UserId
                                         where ElectronicTypeId=@ElectronicTypeId " + SqlWhereLike);
            return SqlOpertion.Instance.GetQueryCount(sql, parmList);
        }
    }
}
