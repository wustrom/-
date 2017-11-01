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
    public partial class TUserOper : SingleTon<TUserOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        /// <returns>对象列表</returns>
        public List<TUser> LoginOn(string UserName, string PassWord)
        {
            var query = new LambdaQuery<TUser>();
            query.Where(p => p.UserName == UserName || p.UserPhone == UserName);
            query.Where(p => p.UserPassword == PassWord);
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
        public List<TUser> SelectByPage(string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<TUser>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.UserName.Contains(SearchKey) || p.UserNickName.Contains(SearchKey) || p.UserPhone.Contains(SearchKey) || p.UserEmail.Contains(SearchKey) || p.ConsumptionTime.Contains(SearchKey));
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
            var query = new LambdaQuery<TUser>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.UserName.Contains(SearchKey) || p.UserNickName.Contains(SearchKey) || p.UserPhone.Contains(SearchKey) || p.UserEmail.Contains(SearchKey) || p.ConsumptionTime.Contains(SearchKey));
            }
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryCount();
        }

        /// <summary>
        /// 根据用户Id筛选用户
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns>对象列表</returns>
        public TUser SelectById(int UserId)
        {
            var query = new LambdaQuery<TUser>();
            query.Where(p => p.UserId == UserId);
            return query.GetQueryList().FirstOrDefault();
        }

        /// <summary>
        /// 根据用户手机,用户名称筛选用户
        /// </summary>
        /// <param name="UserPhone">用户手机</param>
        /// <param name="UserName">用户名称</param>
        /// <returns>对象</returns>
        public TUser SelectRepeat(string UserPhone, string UserName, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<TUser>();
            query.Where(p => p.UserPhone == UserPhone || p.UserName == UserName);
            var List_User = query.GetQueryList(connection, transaction);
            return List_User == null ? null : List_User.FirstOrDefault();
        }

    }
}
