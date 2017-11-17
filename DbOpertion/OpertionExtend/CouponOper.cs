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
    public partial class CouponOper : SingleTon<CouponOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<Coupon> SelectAll(string SearchKey, string Key, bool desc = true)
        {
            var query = new LambdaQuery<Coupon>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CouponDescribe.Contains(SearchKey) || p.CouponName.Contains(SearchKey) || p.ExpirationDay.Contains(SearchKey));
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
        public List<Coupon> SelectByPage(string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<Coupon>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CouponDescribe.Contains(SearchKey) || p.CouponName.Contains(SearchKey) || p.ExpirationDay.Contains(SearchKey));
            }
            return query.GetQueryPageList(start, PageSize);
        }


        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<Coupon> SelectByPageByMemberCard(List<int> List_Id, string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<Coupon>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            query.Where(p => p.CouponId.ContainsIn(List_Id));
            if (!SearchKey.IsNullOrEmpty())
            {

                query.Where(p => p.CouponDescribe.Contains(SearchKey) || p.CouponName.Contains(SearchKey) || p.ExpirationDay.Contains(SearchKey));
            }
            return query.GetQueryPageList(start, PageSize);
        }
        /// <summary>
        /// 查找剩余部分的全部优惠券
        /// </summary>
        /// <param name="List_Id"></param>
        /// <param name="SearchKey"></param>
        /// <param name="Key"></param>
        /// <param name="start"></param>
        /// <param name="PageSize"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public List<Coupon> SelectByPageByAllMemberCard(List<int> List_Id, string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<Coupon>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            query.Where(p => p.CouponId.ContainsNotIn(List_Id));
            if (!SearchKey.IsNullOrEmpty())
            {

                query.Where(p => p.CouponDescribe.Contains(SearchKey) || p.CouponName.Contains(SearchKey) || p.ExpirationDay.Contains(SearchKey));
            }
            return query.GetQueryPageList(start, PageSize);
        }
        /// <summary>
        /// 根据ID查找对应优惠券的数据的总条数
        /// </summary>
        /// <param name="List_Id"></param>
        /// <param name="SearchKey"></param>
        /// <returns></returns>
        public int SelectMemberCardCountByID(List<int> List_Id, string SearchKey)
        {
            var query = new LambdaQuery<Coupon>();
            query.Where(p => p.CouponId.ContainsIn(List_Id));
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CouponDescribe.Contains(SearchKey) || p.CouponName.Contains(SearchKey) || p.ExpirationDay.Contains(SearchKey));

            }
            return query.GetQueryCount();
        }

        /// <summary>
        /// 查找剩余优惠券的数据总条数
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public int SelectAllMemberCardCount(List<int> List_Id, string SearchKey)
        {
            var query = new LambdaQuery<Coupon>();
            query.Where(p => p.CouponId.ContainsNotIn(List_Id));
            if (!SearchKey.IsNullOrEmpty())
            {

                query.Where(p => p.CouponDescribe.Contains(SearchKey) || p.CouponName.Contains(SearchKey) || p.ExpirationDay.Contains(SearchKey));
            }
            return query.GetQueryCount();
        }
        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="SearchKey">搜索条件</param>
        /// <returns>对象列表</returns>
        public int SelectSearchCount(string SearchKey)
        {
            var query = new LambdaQuery<Coupon>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CouponDescribe.Contains(SearchKey) || p.CouponName.Contains(SearchKey) || p.ExpirationDay.Contains(SearchKey));
            }
            return query.GetQueryCount();
        }
        /// <summary>
        /// 根据卡片的名称查找优惠券的信息
        /// </summary>
        /// <param name="CouponName"></param>
        /// <returns></returns>
        public List<Coupon> SelectById(int CouponID)
        {
            var query = new LambdaQuery<Coupon>();
            query.Where(p => p.CouponId == CouponID);
            return query.GetQueryList();
        }
        /// <summary>
        /// 根据ID查找对应的详细信息
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <param name="Key"></param>
        /// <param name="start"></param>
        /// <param name="PageSize"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public List<Coupon> SelectByCouponID(string SearchKey, string Key, int start, int PageSize, DataTablesOrderDir desc)
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
            return CouponOper.Instance.SelectByPage(SearchKey, Key, start, PageSize, asc);
        }
        /// <summary>
        /// 筛选重复的优惠券名称
        /// </summary>
        /// <param name="CouponName">优惠券名称</param>
        /// <returns></returns>
        public List<Coupon> SelectCouponByName(string CouponName)
        {
            var query = new LambdaQuery<Coupon>();
            query.Where(p => p.CouponName == CouponName);
            return query.GetQueryList();
        }
        /// <summary>
        /// 显示已发放优惠券列表
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">主键</param>
        /// <param name="start">开始数据</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public List<CouponUserRelationInfo> Get_CouponUserRelationInfo(int CouponId, string SearchKey,string ExpirationDate1,string ExpirationDate2, string ReleaseDate1, string ReleaseDate2, string Key, int start, int PageSize, bool desc = true)
        {
            List<SqlParameter> parmList = new List<SqlParameter>();
            string SqlWhereLike = null;
            if (!SearchKey.IsNullOrEmpty()|| !ExpirationDate1.IsNullOrEmpty() || !ExpirationDate2.IsNullOrEmpty() || !ReleaseDate1.IsNullOrEmpty() || !ReleaseDate2.IsNullOrEmpty())
            {
                parmList.Add(new SqlParameter("@CouponName", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@UserNickName", "%" + SearchKey + "%"));
                SqlWhereLike = @" and (CouponName like @CouponName or UserNickName like @UserNickName)";
                if (!ExpirationDate1.IsNullOrEmpty() && !ExpirationDate2.IsNullOrEmpty())
                {
                    parmList.Add(new SqlParameter("@ExpirationDate1", "" + Convert.ToDateTime(ExpirationDate1) + ""));
                    parmList.Add(new SqlParameter("@ExpirationDate2", "" + Convert.ToDateTime(ExpirationDate2) + ""));
                    SqlWhereLike = SqlWhereLike + @"and
                                          (ExpirationDate >= @ExpirationDate1 and ExpirationDate <= @ExpirationDate2)";
                }
                if (!ReleaseDate1.IsNullOrEmpty() && !ReleaseDate2.IsNullOrEmpty())
                {
                    parmList.Add(new SqlParameter("@ReleaseDate1", "" + Convert.ToDateTime(ReleaseDate1) + ""));
                    parmList.Add(new SqlParameter("@ReleaseDate2", "" + Convert.ToDateTime(ReleaseDate2) + ""));
                    SqlWhereLike = SqlWhereLike + @"and
                                          (ReleaseDate >= @ReleaseDate1 and ReleaseDate <= @ReleaseDate2)";
                }
            }
            parmList.Add(new SqlParameter("@CouponId", CouponId));
            string sql = string.Format(@"select CouponId,CouponUserRelationId,CouponName,UserNickName,ExpirationDate,ReleaseDate from CouponUserRelation LEFT JOIN TUser
                                       on CouponUserRelation.UserId=TUser.UserId where CouponId=@CouponId" + SqlWhereLike);
            return SqlOpertion.Instance.GetQueryPage<CouponUserRelationInfo>(sql, parmList, Key, desc, start, PageSize);
            
        }
        /// <summary>
        /// 筛选已发放优惠券页面的数据条数
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <returns></returns>
        public int Select_CouponUserRelationInfoCount(int CouponId, string SearchKey,string ExpirationDate1, string ExpirationDate2, string ReleaseDate1, string ReleaseDate2)
        {
            List<SqlParameter> parmList = new List<SqlParameter>();
            string SqlWhereLike = null;
            if (!SearchKey.IsNullOrEmpty() || !ExpirationDate1.IsNullOrEmpty() || !ExpirationDate2.IsNullOrEmpty() || !ReleaseDate1.IsNullOrEmpty() || !ReleaseDate2.IsNullOrEmpty())
            {
                parmList.Add(new SqlParameter("@CouponName", "%" + SearchKey + "%"));
                parmList.Add(new SqlParameter("@UserNickName", "%" + SearchKey + "%"));
                SqlWhereLike = @" and (CouponName like @CouponName or UserNickName like @UserNickName or
                                  ExpirationDate like @ExpirationDate or ReleaseDate like @ReleaseDate)";
                if (!ExpirationDate1.IsNullOrEmpty() && !ExpirationDate2.IsNullOrEmpty())
                {
                    parmList.Add(new SqlParameter("@ExpirationDate1", "" + Convert.ToDateTime(ExpirationDate1) + ""));
                    parmList.Add(new SqlParameter("@ExpirationDate2", "" + Convert.ToDateTime(ExpirationDate2) + ""));
                    SqlWhereLike = SqlWhereLike + @"and
                                          (ExpirationDate >= @ExpirationDate1 and ExpirationDate <= @ExpirationDate2)";
                }
                if (!ReleaseDate1.IsNullOrEmpty() && !ReleaseDate2.IsNullOrEmpty())
                {
                    parmList.Add(new SqlParameter("@ReleaseDate1", "" + Convert.ToDateTime(ReleaseDate1) + ""));
                    parmList.Add(new SqlParameter("@ReleaseDate2", "" + Convert.ToDateTime(ReleaseDate2) + ""));
                    SqlWhereLike = SqlWhereLike + @"and
                                          (ReleaseDate >= @ReleaseDate1 and ReleaseDate <= @ReleaseDate2)";
                }
            }
            parmList.Add(new SqlParameter("@CouponId", CouponId));
            string sql = string.Format(@"select CouponId,CouponName,UserNickName,ExpirationDate,ReleaseDate from CouponUserRelation LEFT JOIN TUser
                                       on CouponUserRelation.UserId=TUser.UserId where CouponId=@CouponId" + SqlWhereLike);
            return SqlOpertion.Instance.GetQueryCount(sql, parmList);
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
            var delete = new LambdaDelete<Coupon>();
            delete.Where(p => p.CouponId.ContainsIn(KeyId));
            return delete.GetDeleteResult();
        }
        /// <summary>
        /// 根据优惠券Id获得优惠券信息
        /// </summary>
        /// <param name="CouponId">优惠券Id</param>
        /// <returns>对象列表</returns>
        public Coupon SelectByCouponId(int CouponId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<Coupon>();
            query.Where(p => p.CouponId == CouponId);
            return query.GetQueryList(connection, transaction).FirstOrDefault();
        }
    }

}
