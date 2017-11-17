using Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.LambdaOpertion;
using Common.Extend;
using DbOpertion.Models;
using Common.Result;
using System.Data.SqlClient;
using System.Data;

namespace DbOpertion.Operation
{
    public partial class MemberShipCardOper : SingleTon<MemberShipCardOper>
    {

        /// <summary>
        /// 新增会员卡
        /// </summary>
        /// <param name="CardName">会员卡名</param>
        /// <param name="TypeImage">会员卡图案</param>
        /// <param name="IsUser">是否使用</param>
        /// <param name="IsDelete">是否失效</param>
        /// <returns></returns>
        public bool AddMemberCard(string CardName, string TypeImage, bool IsUser, bool IsDelete)
        {
            var insert = new LambdaInsert<MemberShipCard>();
            insert.Insert(p => p.CardName == CardName);
            return insert.GetInsertResult();
        }

        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<MemberShipCard> SelectByPage(string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<MemberShipCard>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CardName.Contains(SearchKey));
            }
            if (Key != null)
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
            var query = new LambdaQuery<MemberShipCard>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CardName.Contains(SearchKey));

            }
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryCount();


        }
        /// <summary>
        /// 筛选会员卡信息根据会员卡类型Id
        /// </summary>
        /// <param name="MemberShipTypeId">会员卡类型Id</param>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">主键</param>
        /// <param name="start">开始数据</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public List<MemberCardByTypeInfo> SelectMemCardByTypeId(int MemberShipTypeId, string SearchKey, string ReleaseDate1, string ReleaseDate2, string CreateDate1, string CreateDate2, string Key, int start, int PageSize, bool desc = true)
        {
            List<SqlParameter> parmList = new List<SqlParameter>();
            string SqlWhereLike = null;
            if (!SearchKey.IsNullOrEmpty() || !ReleaseDate1.IsNullOrEmpty() || !ReleaseDate2.IsNullOrEmpty() || !CreateDate1.IsNullOrEmpty() || !CreateDate2.IsNullOrEmpty())
            {
                parmList.Add(new SqlParameter("@CardName", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@UserNickName", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@MemberShipCardId", "%" + SearchKey + "%"));
                SqlWhereLike = @" and (CardName like @CardName or
                                         UserNickName like @UserNickName or MemberShipCardId like @MemberShipCardId) ";
                if (!ReleaseDate1.IsNullOrEmpty() && !ReleaseDate2.IsNullOrEmpty())
                {
                    parmList.Add(new SqlParameter("@ReleaseDate1", "" + Convert.ToDateTime(ReleaseDate1) + ""));
                    parmList.Add(new SqlParameter("@ReleaseDate2", "" + Convert.ToDateTime(ReleaseDate2) + ""));
                    SqlWhereLike = SqlWhereLike + @"and
                                          (ReleaseDate >= @ReleaseDate1 and ReleaseDate <= @ReleaseDate2)";
                }
                if (!CreateDate1.IsNullOrEmpty() && !CreateDate2.IsNullOrEmpty())
                {
                    parmList.Add(new SqlParameter("@CreateDate1", "" + Convert.ToDateTime(CreateDate1) + ""));
                    parmList.Add(new SqlParameter("@CreateDate2", "" + Convert.ToDateTime(CreateDate2) + ""));
                    SqlWhereLike = SqlWhereLike + @"and
                                          (CreateDate >= @CreateDate1 and CreateDate <= @CreateDate2)";
                }

            }
            parmList.Add(new SqlParameter("@MemberShipTypeId", MemberShipTypeId));
            string sql = string.Format(@"select a.CardName,a.ReleaseDate,a.CreateDate,a.MemberShipTypeId,a.MemberShipCardId,a.IsUser,b.UserNickName,a.UserId from MemberShipCard a 
                                         left join TUser b on a.UserId=b.UserId
                                         where MemberShipTypeId=@MemberShipTypeId " + SqlWhereLike);
            return SqlOpertion.Instance.GetQueryPage<MemberCardByTypeInfo>(sql, parmList, Key, desc, start, PageSize);
        }


        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<MemberShipCard> SelectByPageByMemberCard(int MemberShipTypeId, string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<MemberShipCard>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            query.Where(p => p.MemberShipTypeId == MemberShipTypeId);
            if (!SearchKey.IsNullOrEmpty())
            {

                query.Where(p => p.CardName.Contains(SearchKey) || p.CardPassword.Contains(SearchKey));
            }
            return query.GetQueryPageList(start, PageSize);
        }

        /// <summary>
        /// 筛选根据类型ID查询的会员卡数目
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">主键</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public int SelectMemberCardCountByTypeID(int MemberShipTypeId, string SearchKey)
        {
            var query = new LambdaQuery<MemberShipCard>();
            query.Where(p => p.MemberShipTypeId == MemberShipTypeId);
            if (!SearchKey.IsNullOrEmpty())
            {

                query.Where(p => p.CardName.Contains(SearchKey) || p.CreateDate.Contains(SearchKey) || p.ReleaseDate.Contains(SearchKey));
            }
            return query.GetQueryCount();

        }
        /// <summary>
        /// 添加会员卡时筛选重复的会员卡名
        /// </summary>
        /// <param name="CardName">会员卡名</param>
        /// <returns></returns>
        public List<MemberShipCard> SelectMemCardByName(string CardName, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberShipCard>();
            query.Where(p => p.CardName == CardName);
            return query.GetQueryList(connection, transaction);
        }

        /// <summary>
        /// 筛选Excel里重复的卡号
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<string> SelectByName(IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberShipCard>();
            return query.GetQueryList(connection, transaction).Select(p => p.CardName).ToList();
        }
        /// <summary>
        /// 根据Id删除多条数据
        /// </summary>
        /// <param name="KeyId">所选对象的Id</param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool DeleteByMemberCardIds(List<int> KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = new LambdaDelete<MemberShipCard>();
            delete.Where(p => p.MemberShipCardId.ContainsIn(KeyId));
            return delete.GetDeleteResult();
        }
    }
}
